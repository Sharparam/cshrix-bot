// <copyright file="JoinedRoom.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using Newtonsoft.Json;

    /// <summary>
    /// Contains events in a sync response for a joined room.
    /// </summary>
    public readonly struct JoinedRoom
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinedRoom" /> structure.
        /// </summary>
        /// <param name="summary">A summary of the room.ĳ</param>
        /// <param name="state">The current state of the room.</param>
        /// <param name="timeline">The timeline of the room.</param>
        /// <param name="ephemeral">Ephemeral events for the room.</param>
        /// <param name="accountData">Room-specific account data.</param>
        /// <param name="unreadCounts">Unread counts for the room.</param>
        [JsonConstructor]
        public JoinedRoom(
            RoomSummary summary,
            EventsContainer state,
            Timeline timeline,
            EventsContainer ephemeral,
            EventsContainer accountData,
            UnreadCounts unreadCounts)
            : this()
        {
            Summary = summary;
            State = state;
            Timeline = timeline;
            Ephemeral = ephemeral;
            AccountData = accountData;
            UnreadCounts = unreadCounts;
        }

        /// <summary>
        /// Gets a summary of the room.
        /// </summary>
        [JsonProperty("summary")]
        public RoomSummary Summary { get; }

        /// <summary>
        /// Gets the current state of the room.
        /// </summary>
        [JsonProperty("state")]
        public EventsContainer State { get; }

        /// <summary>
        /// Gets the timeline for the room.
        /// </summary>
        [JsonProperty("timeline")]
        public Timeline Timeline { get; }

        /// <summary>
        /// Gets ephemeral events for the room.
        /// </summary>
        [JsonProperty("ephemeral")]
        public EventsContainer Ephemeral { get; }

        /// <summary>
        /// Gets account data specific for this room.
        /// </summary>
        [JsonProperty("account_data")]
        public EventsContainer AccountData { get; }

        /// <summary>
        /// Gets an object describing unread counts for messages.
        /// </summary>
        [JsonProperty("unread_notifications")]
        public UnreadCounts UnreadCounts { get; }
    }
}
