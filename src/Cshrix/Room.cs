// <copyright file="Room.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Data;
    using Data.Events;
    using Data.Events.Content;

    using Events;

    using Extensions;

    using Microsoft.Extensions.Logging;

    using Utilities;

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

        // ReSharper disable once NotAccessedField.Local
        /// <summary>
        /// The Matrix client instance.
        /// </summary>
        private readonly IMatrixClient _client;

        /// <summary>
        /// The client-server API instance.
        /// </summary>
        private readonly IMatrixClientServerApi _api;

        /// <summary>
        /// A set containing all known aliases for the room.
        /// </summary>
        private readonly HashSet<RoomAlias> _aliases;

        /// <summary>
        /// Contains event IDs that have been replaced by new events.
        /// </summary>
        private readonly HashSet<string> _replacedEventIds;

        /// <summary>
        /// Contains power levels for this room.
        /// </summary>
        private readonly PowerLevels _powerLevels;

        /// <summary>
        /// Initializes a new instance of the <see cref="Room" /> class.
        /// </summary>
        /// <param name="log">Logger instance for the room.</param>
        /// <param name="client">The Matrix client instance.</param>
        /// <param name="api">The Matrix client-server API instance.</param>
        /// <param name="id">The ID of the room.</param>
        /// <param name="membership">The current membership of the user in the room.</param>
        public Room(ILogger log, IMatrixClient client, IMatrixClientServerApi api, string id, Membership membership)
        {
            _log = log;
            _client = client;
            _api = api;
            Id = id;
            Membership = membership;
            _aliases = new HashSet<RoomAlias>();
            _replacedEventIds = new HashSet<string>();
            _powerLevels = new PowerLevels();
        }

        /// <inheritdoc />
        public event AsyncEventHandler<MessageEventArgs> Message;

        /// <inheritdoc />
        public event AsyncEventHandler<TombstonedEventArgs> Tombstoned;

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
        public IPowerLevels PowerLevels => _powerLevels;

        /// <inheritdoc />
        public bool IsTombstoned { get; private set; }

        /// <inheritdoc />
        public TombstoneContent TombstoneContent { get; private set; }

        /// <inheritdoc />
        public async Task<string> SendAsync(string message)
        {
            _log.LogTrace("Sending message to room");
            var content = new MessageContent(message, "m.text");
            var transactionId = TransactionUtils.GenerateId();
            _log.LogTrace("Sending message with transaction ID {TransactionId}", transactionId);
            var result = await _api.SendEventAsync(Id, "m.room.message", transactionId, content);
            _log.LogTrace(
                "Message with TXNID {TransactionId} sent to room with event ID {EventId}",
                transactionId,
                result.Id);

            return result.Id;
        }

        /// <summary>
        /// Generates a logger category for the specified room ID.
        /// </summary>
        /// <param name="id">The room ID.</param>
        /// <returns>The logger category.</returns>
        internal static string GenerateLoggerCategory(string id)
        {
            var typeName = typeof(Room).FullName;
            return $"{typeName}.{id}";
        }

        /// <summary>
        /// Updates a room from a synced <see cref="InvitedRoom" /> object.
        /// </summary>
        /// <param name="invitedRoom">The sync data to update from.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        internal async Task UpdateAsync(InvitedRoom invitedRoom)
        {
            _log.LogTrace("Updating from an InvitedRoom object");
            Membership = Membership.Invited;
            await UpdateFromEventsAsync(invitedRoom.InviteState.Events);
        }

        /// <summary>
        /// Updates a room from a synced <see cref="JoinedRoom" /> object.
        /// </summary>
        /// <param name="joinedRoom">The sync data to update from.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        internal async Task UpdateAsync(JoinedRoom joinedRoom)
        {
            _log.LogTrace("Updating from a JoinedRoom object");
            Membership = Membership.Joined;
            await UpdateFromEventsAsync(joinedRoom.State.Events);
            await UpdateFromEventsAsync(joinedRoom.Timeline.Events);
        }

        /// <summary>
        /// Updates room details from a collection of events.
        /// </summary>
        /// <param name="events">The events to update from.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        private async Task UpdateFromEventsAsync(IReadOnlyCollection<Event> events)
        {
            var replacedIds = events.Where(e => e.Unsigned?.ReplacesStateEventId != null)
                .Select(e => e.Unsigned.Value.ReplacesStateEventId);

            _replacedEventIds.UnionWith(replacedIds);

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
                _powerLevels.Content = powerLevelsContent;
            }

            if (filtered.GetStateEventContentOrDefault("m.room.tombstone") is TombstoneContent tombstoneContent)
            {
                _log.LogTrace("Room has been tombstoned");
                await OnTombstoneAsync(tombstoneContent);
            }

            await HandleMessageEventsAsync(filtered);
        }

        /// <summary>
        /// Updates this room's aliases from a collection of events.
        /// </summary>
        /// <param name="events">The events to update from.</param>
        private void UpdateAliasesFromEvents(IReadOnlyCollection<Event> events)
        {
            _log.LogTrace("Updating aliases from events");
            var aliasContent = events.GetStateEventContentOrDefault<CanonicalAliasContent>("m.room.canonical_alias");

            if (aliasContent != null)
            {
                var oldCanonAlias = CanonicalAlias;
                _log.LogTrace("New canonical alias: {CanonicalAlias}", aliasContent.Alias);
                CanonicalAlias = aliasContent.Alias;

                if (oldCanonAlias != null)
                {
                    _log.LogTrace("Removing old canonical alias: {OldCanonicalAlias}", oldCanonAlias);
                    _aliases.Remove(oldCanonAlias);
                }

                _aliases.Add(aliasContent.Alias);
            }

            var aliasesStates = events.OfEventType("m.room.aliases");
            var aliases = aliasesStates.SelectMany(e => (e.Content as AliasesContent)?.Aliases).Where(a => a != null);
            _aliases.UnionWith(aliases);
        }

        /// <summary>
        /// Extracts message events from the given collection of events and handles them.
        /// </summary>
        /// <param name="events">The collection of events to process.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        private async Task HandleMessageEventsAsync(IReadOnlyCollection<Event> events)
        {
            _log.LogTrace("Handling message events");
            var messageEvents = events.OfEventType("m.room.message");
            foreach (var messageEvent in messageEvents)
            {
                await HandleMessageEventAsync(messageEvent);
            }
        }

        /// <summary>
        /// Handles a message event.
        /// </summary>
        /// <param name="messageEvent">The message event to handle.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        private async Task HandleMessageEventAsync(Event messageEvent)
        {
            if (messageEvent.Type != "m.room.message" || messageEvent.Sender == null || !messageEvent.SentAt.HasValue ||
                !(messageEvent.Content is MessageContent content))
            {
                return;
            }

            var sender = messageEvent.Sender;
            var timestamp = messageEvent.SentAt.Value;

            var message = new Message(sender, this, timestamp, content.MessageType, content);

            _log.LogTrace("Broadcasting message from {Sender} at {Timestamp}", sender, timestamp);
            await Message.InvokeAsync(this, new MessageEventArgs(message));
        }

        /// <summary>
        /// Sets tombstone properties and invokes the <see cref="Tombstoned" /> event.
        /// </summary>
        /// <param name="content">Information about the tombstone event.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        private async Task OnTombstoneAsync(TombstoneContent content)
        {
            _log.LogTrace("Room tombstoned. Setting properties and broadcasting event");
            IsTombstoned = true;
            TombstoneContent = content;
            await Tombstoned.InvokeAsync(this, new TombstonedEventArgs(content));
        }
    }
}
