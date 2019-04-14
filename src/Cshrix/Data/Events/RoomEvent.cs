// <copyright file="RoomEvent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using System;

    using Newtonsoft.Json;

    public class RoomEvent : SenderEvent
    {
        public RoomEvent(EventContent content, string type, Identifier id, Identifier sender)
            : base(content, type, sender) =>
            Id = id;

        [JsonProperty("event_id")]
        public Identifier Id { get; }

        [JsonProperty("origin_server_ts")]
        public DateTimeOffset SentAt { get; }

        [JsonProperty("unsigned")]
        public UnsignedData Unsigned { get; }
    }
}
