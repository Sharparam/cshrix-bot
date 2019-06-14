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
        /// A set containing all known aliases for the room.
        /// </summary>
        private readonly HashSet<RoomAlias> _aliases;

        /// <summary>
        /// Initializes a new instance of the <see cref="Room" /> class.
        /// </summary>
        /// <param name="id">The ID of the room.</param>
        /// <param name="membership">The current membership of the user in the room.</param>
        public Room(string id, Membership membership)
        {
            Id = id;
            Membership = membership;
            _aliases = new HashSet<RoomAlias>();
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
        public bool IsTombstoned { get; private set; }

        /// <inheritdoc />
        public TombstoneContent TombstoneContent { get; private set; }

        /// <summary>
        /// Creates a <see cref="Room" /> from the contents of an <see cref="InvitedRoom" />.
        /// </summary>
        /// <param name="id">The ID of the room the user was invited to.</param>
        /// <param name="invitedRoom">The data for the invited room.</param>
        /// <returns>An instance of <see cref="Room" />.</returns>
        internal static Room FromInvitedRoom(string id, InvitedRoom invitedRoom)
        {
            var room = new Room(id, Membership.Invited);

            var state = invitedRoom.InviteState;
            room.UpdateFromEvents(state.Events);

            return room;
        }

        /// <summary>
        /// Creates a <see cref="Room" /> from the contents of a <see cref="JoinedRoom" />.
        /// </summary>
        /// <param name="id">The ID of the room.</param>
        /// <param name="joinedRoom">Data about the joined room.</param>
        /// <returns>An instance of <see cref="Room" />.</returns>
        internal static Room FromJoinedRoom(string id, JoinedRoom joinedRoom)
        {
            var room = new Room(id, Membership.Joined);

            room.Update(joinedRoom);

            return room;
        }

        /// <summary>
        /// Updates a room from a synced <see cref="JoinedRoom" /> object.
        /// </summary>
        /// <param name="joinedRoom">The sync data to update from.</param>
        internal void Update(JoinedRoom joinedRoom)
        {
            UpdateFromEvents(joinedRoom.State.Events);
            UpdateFromEvents(joinedRoom.Timeline.Events);
        }

        /// <summary>
        /// Updates room details from a collection of events.
        /// </summary>
        /// <param name="events">The events to update from.</param>
        private void UpdateFromEvents(IReadOnlyCollection<Event> events)
        {
            UpdateAliasesFromEvents(events);

            if (events.GetStateEventContentOrDefault("m.room.name") is RoomNameContent nameContent)
            {
                Name = nameContent.Name;
            }

            if (events.GetStateEventContentOrDefault("m.room.topic") is RoomTopicContent topicContent)
            {
                Topic = topicContent.Topic;
            }

            if (events.GetStateEventContentOrDefault("m.room.create") is CreationContent creationContent)
            {
                Creator = creationContent.Creator;
                Version = creationContent.RoomVersion ?? DefaultRoomVersion;
            }

            if (events.GetStateEventContentOrDefault("m.room.tombstone") is TombstoneContent tombstoneContent)
            {
                OnTombstone(tombstoneContent);
            }
        }

        /// <summary>
        /// Updates this room's aliases from a collection of events.
        /// </summary>
        /// <param name="events">The events to update from.</param>
        private void UpdateAliasesFromEvents(IReadOnlyCollection<Event> events)
        {
            var aliasContent = events.GetStateEventContentOrDefault<CanonicalAliasContent>("m.room.canonical_alias");

            if (aliasContent?.Alias != null)
            {
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
            IsTombstoned = true;
            TombstoneContent = content;
            Tombstoned?.Invoke(this, new TombstonedEventArgs(content));
        }
    }
}
