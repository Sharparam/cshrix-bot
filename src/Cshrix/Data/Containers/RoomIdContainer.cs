// <copyright file="RoomIdContainer.cs">
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
    /// An object containing a room ID.
    /// </summary>
    public readonly struct RoomIdContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomIdContainer" /> structure.
        /// </summary>
        /// <param name="roomId">The room ID.</param>
        [JsonConstructor]
        public RoomIdContainer(string roomId)
            : this() =>
            RoomId = roomId;

        /// <summary>
        /// Gets the room ID.
        /// </summary>
        [JsonProperty("room_id")]
        public string RoomId { get; }
    }
}
