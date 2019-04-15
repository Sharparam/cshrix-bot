// <copyright file="UnsignedData.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using System;
    using System.Collections.Generic;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Serialization;

    public readonly struct UnsignedData
    {
        [JsonConstructor]
        public UnsignedData(
            TimeSpan age,
            Event @event,
            string transactionId,
            IReadOnlyCollection<StrippedState> inviteRoomState)
            : this()
        {
            Age = age;
            Event = @event;
            TransactionId = transactionId;
            InviteRoomState = inviteRoomState;
        }

        [JsonProperty("age")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan Age { get; }

        [JsonProperty("redacted_because")]
        [CanBeNull]
        public Event Event { get; }

        [JsonProperty("transaction_id")]
        [CanBeNull]
        public string TransactionId { get; }

        [JsonProperty("invite_room_state")]
        [CanBeNull]
        public IReadOnlyCollection<StrippedState> InviteRoomState { get; }
    }
}
