// <copyright file="RoomIdEvent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using Content;

    using Newtonsoft.Json;

    public class RoomIdEvent : Event
    {
        public RoomIdEvent(EventContent content, string type, Identifier roomId)
            : base(content, type) =>
            RoomId = roomId;

        [JsonProperty("room_id")]
        public Identifier RoomId { get; }
    }
}
