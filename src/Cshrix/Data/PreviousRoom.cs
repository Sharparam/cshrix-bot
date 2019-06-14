// <copyright file="PreviousRoom.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Newtonsoft.Json;

    /// <summary>
    /// Contains data on a previous room.
    /// </summary>
    public readonly struct PreviousRoom
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreviousRoom" /> structure.
        /// </summary>
        /// <param name="roomId">The ID of the previous room.</param>
        /// <param name="eventId">The ID of the last known event in the previous room.</param>
        [JsonConstructor]
        public PreviousRoom(string roomId, string eventId)
            : this()
        {
            RoomId = roomId;
            EventId = eventId;
        }

        /// <summary>
        /// Gets the ID of the previous room.
        /// </summary>
        [JsonProperty("room_id")]
        public string RoomId { get; }

        /// <summary>
        /// Gets the ID of the last known event in the previous room.
        /// </summary>
        [JsonProperty("event_id")]
        public string EventId { get; }
    }
}
