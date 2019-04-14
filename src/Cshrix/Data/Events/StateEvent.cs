// <copyright file="StateEvent.cs">
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

    public class StateEvent : StrippedState
    {
        public StateEvent(
            EventContent content,
            string type,
            Identifier id,
            Identifier sender,
            DateTimeOffset sentAt,
            UnsignedData unsigned,
            EventContent previousContent,
            string stateKey)
            : base(content, stateKey, type, sender)
        {
            Id = id;
            SentAt = sentAt;
            Unsigned = unsigned;
            PreviousContent = previousContent;
        }

        [JsonProperty("event_id")]
        public string Id { get; }

        [JsonProperty("origin_server_ts")]
        public DateTimeOffset SentAt { get; }

        [JsonProperty("unsigned")]
        public UnsignedData Unsigned { get; }

        [JsonProperty("prev_content")]
        public EventContent PreviousContent { get; }
    }
}
