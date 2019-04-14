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

    using Serialization;

    public class RoomEvent : SenderEvent
    {
        public RoomEvent(
            EventContent content,
            string type,
            Identifier id,
            Identifier sender,
            Identifier? roomId,
            DateTimeOffset sentAt,
            UnsignedData? unsigned)
            : base(content, type, sender)
        {
            Id = id;
            RoomId = roomId;
            SentAt = sentAt;
            Unsigned = unsigned;
        }

        [JsonProperty("event_id")]
        public Identifier Id { get; }

        /// <summary>
        /// Gets the ID of the room this event is associated with.
        /// May be <c>null</c> if this event was retrieved from the sync API.
        /// </summary>
        /// <remarks>
        /// Cshrix will attempt to set this ID after retrieving events via the sync API,
        /// but it cannot guarantee that the ID will be set at all times.
        /// </remarks>
        [JsonProperty("room_id")]
        public Identifier? RoomId { get; internal set; }

        [JsonProperty("origin_server_ts")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTimeOffset SentAt { get; }

        [JsonProperty("unsigned")]
        public UnsignedData? Unsigned { get; }
    }
}
