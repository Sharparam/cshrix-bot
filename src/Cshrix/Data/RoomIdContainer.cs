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

    public readonly struct RoomIdContainer
    {
        [JsonConstructor]
        public RoomIdContainer(string roomId)
            : this() =>
            RoomId = roomId;

        [JsonProperty("room_id")]
        public string RoomId { get; }
    }
}
