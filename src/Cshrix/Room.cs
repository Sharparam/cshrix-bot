// <copyright file="Room.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using Data.Events;
    using Data.Events.Content;

    using Extensions;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Describes a room joined by the client, and defines possible actions.
    /// </summary>
    public sealed class Room : IRoom
    {
        /// <summary>
        /// The default room version if none is specified.
        /// </summary>
        private const string DefaultRoomVersion = "1";

        /// <summary>
        /// The logger instance for the object.
        /// </summary>
        private readonly ILogger _log;

        /// <summary>
        /// A set containing all known aliases for the room.
        /// </summary>
        private readonly HashSet<RoomAlias> _aliases;

        /// <summary>
        /// Contains event IDs that have been replaced by new events.
        /// </summary>
        private readonly HashSet<string> _replacedEventIds;

        /// <summary>
        /// Initializes a new instance of the <see cref="Room" /> class.
        /// </summary>
        /// <param name="log">Logger instance for the room.</param>
        /// <param name="id">The ID of the room.</param>
        /// <param name="membership">The current membership of the user in the room.</param>
        private Room(ILogger log, string id, Membership membership)
        {
            _log = log;
            Id = id;
            Membership = membership;
            _aliases = new HashSet<RoomAlias>();
            _replacedEventIds = new HashSet<string>();
            PowerLevels = new PowerLevels();
        }

        /// <inheritdoc />
        public event EventHandler<TombstonedEventArgs> Tombstoned;

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
        public Membership Membership { get; private set; }

        /// <inheritdoc />
        public RoomAlias CanonicalAlias { get; private set; }

        /// <inheritdoc />
        public IReadOnlyCollection<RoomAlias> Aliases => _aliases;

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <inheritdoc />
        public string Topic { get; private set; }

        /// <inheritdoc />
        public UserId Creator { get; private set; }

        /// <inheritdoc />
        public string Version { get; private set; } = DefaultRoomVersion;

        /// <inheritdoc />
        public PowerLevels PowerLevels { get; }

        /// <inheritdoc />
        public bool IsTombstoned { get; private set; }

        /// <inheritdoc />
        public TombstoneContent TombstoneContent { get; private set; }

        /// <summary>
        /// Creates a <see cref="Room" /> from the contents of an <see cref="InvitedRoom" />.
        /// </summary>
        /// <param name="loggerFactory">Logger factory.</param>
        /// <param name="id">The ID of the room the user was invited to.</param>
        /// <param name="invitedRoom">The data for the invited room.</param>
        /// <returns>An instance of <see cref="Room" />.</returns>
        internal static Room FromInvitedRoom(ILoggerFactory loggerFactory, string id, InvitedRoom invitedRoom)
        {
            var logger = loggerFactory.CreateLogger(GenerateLoggerCategory(id));
            var room = new Room(logger, id, Membership.Invited);

            var state = invitedRoom.InviteState;
            room.UpdateFromEvents(state.Events);

            return room;
        }

        /// <summary>
        /// Creates a <see cref="Room" /> from the contents of a <see cref="JoinedRoom" />.
        /// </summary>
        /// <param name="loggerFactory">Logger factory.</param>
        /// <param name="id">The ID of the room.</param>
        /// <param name="joinedRoom">Data about the joined room.</param>
        /// <returns>An instance of <see cref="Room" />.</returns>
        internal static Room FromJoinedRoom(ILoggerFactory loggerFactory, string id, JoinedRoom joinedRoom)
        {
            var logger = loggerFactory.CreateLogger(GenerateLoggerCategory(id));
            var room = new Room(logger, id, Membership.Joined);

            room.Update(joinedRoom);

            return room;
        }

        /// <summary>
        /// Generates a logger category for the specified room ID.
        /// </summary>
        /// <param name="id">The room ID.</param>
        /// <returns>The logger category.</returns>
        private static string GenerateLoggerCategory(string id)
        {
            var typeName = typeof(Room).FullName;
            return $"{typeName}.{id}";
        }

        /// <summary>
        /// Updates a room from a synced <see cref="JoinedRoom" /> object.
        /// </summary>
        /// <param name="joinedRoom">The sync data to update from.</param>
        internal void Update(JoinedRoom joinedRoom)
        {
            _log.LogTrace("Updating from a JoinedRoom object");
            UpdateFromEvents(joinedRoom.State.Events);
            UpdateFromEvents(joinedRoom.Timeline.Events);
        }

        /// <summary>
        /// Updates room details from a collection of events.
        /// </summary>
        /// <param name="events">The events to update from.</param>
        private void UpdateFromEvents(IReadOnlyCollection<Event> events)
        {
            var filtered = events.Where(e => !_replacedEventIds.Contains(e.Id)).ToList().AsReadOnly();

            _log.LogTrace("Updating from collection of events");
            UpdateAliasesFromEvents(filtered);

            if (filtered.GetStateEventContentOrDefault("m.room.name") is RoomNameContent nameContent)
            {
                _log.LogTrace("Updating room name to {Name}", nameContent.Name);
                Name = nameContent.Name;
            }

            if (filtered.GetStateEventContentOrDefault("m.room.topic") is RoomTopicContent topicContent)
            {
                _log.LogTrace("Updating room topic to {Topic}", topicContent.Topic);
                Topic = topicContent.Topic;
            }

            if (filtered.GetStateEventContentOrDefault("m.room.create") is CreationContent creationContent)
            {
                _log.LogTrace(
                    "Updating room creator to {Creator} and version to {Version}",
                    creationContent.Creator,
                    creationContent.RoomVersion);

                Creator = creationContent.Creator;
                Version = creationContent.RoomVersion ?? DefaultRoomVersion;
            }

            if (filtered.GetStateEventContentOrDefault("m.room.power_levels") is PowerLevelsContent powerLevelsContent)
            {
                _log.LogTrace("Updating power levels");
                PowerLevels.Content = powerLevelsContent;
            }

            if (filtered.GetStateEventContentOrDefault("m.room.tombstone") is TombstoneContent tombstoneContent)
            {
                _log.LogTrace("Room has been tombstoned");
                OnTombstone(tombstoneContent);
            }
        }

        /// <summary>
        /// Updates this room's aliases from a collection of events.
        /// </summary>
        /// <param name="events">The events to update from.</param>
        private void UpdateAliasesFromEvents(IReadOnlyCollection<Event> events)
        {
            _log.LogTrace("Updating aliases from events");
            var aliasContent = events.GetStateEventContentOrDefault<CanonicalAliasContent>("m.room.canonical_alias");

            if (aliasContent?.Alias != null)
            {
                _log.LogTrace("New canonical alias: {CanonicalAlias}", aliasContent.Alias);
                CanonicalAlias = aliasContent.Alias;
                _aliases.Add(aliasContent.Alias);
            }

            var aliasesStates = events.OfEventType("m.room.aliases");
            var aliases = aliasesStates.SelectMany(e => (e.Content as AliasesContent)?.Aliases).Where(a => a != null);
            _aliases.UnionWith(aliases);
        }

        /// <summary>
        /// Sets tombstone properties and invokes the <see cref="Tombstoned" /> event.
        /// </summary>
        /// <param name="content">Information about the tombstone event.</param>
        private void OnTombstone(TombstoneContent content)
        {
            _log.LogTrace("Room tombstoned. Setting properties and broadcasting event");
            IsTombstoned = true;
            TombstoneContent = content;
            Tombstoned?.Invoke(this, new TombstonedEventArgs(content));
        }
    }
}
