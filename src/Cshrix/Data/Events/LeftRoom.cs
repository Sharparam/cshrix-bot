// <copyright file="LeftRoom.cs">
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
    /// Contains events and data for a room the user has left.
    /// </summary>
    public readonly struct LeftRoom
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LeftRoom" /> structure.
        /// </summary>
        /// <param name="state">The last known state of the room.</param>
        /// <param name="timeline">Timeline of the room.</param>
        /// <param name="accountData">Room-specific account data.</param>
        [JsonConstructor]
        public LeftRoom(State state, Timeline timeline, EventsContainer accountData)
            : this()
        {
            State = state;
            Timeline = timeline;
            AccountData = accountData;
        }

        /// <summary>
        /// Gets the last known state of the room.
        /// </summary>
        [JsonProperty("state")]
        public State State { get; }

        /// <summary>
        /// Gets the timeline for the room, as it looked when the room was left.
        /// </summary>
        [JsonProperty("timeline")]
        public Timeline Timeline { get; }

        /// <summary>
        /// Gets room-specific account data for the room.
        /// </summary>
        [JsonProperty("account_data")]
        public EventsContainer AccountData { get; }
    }
}
