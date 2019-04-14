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

    using Newtonsoft.Json;

    using Serialization;

    public readonly struct UnsignedData
    {
        [JsonConstructor]
        public UnsignedData(TimeSpan age, Event @event, string transactionId)
            : this()
        {
            Age = age;
            Event = @event;
            TransactionId = transactionId;
        }

        [JsonProperty("age")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan Age { get; }

        [JsonProperty("redacted_because")]
        public Event Event { get; }

        [JsonProperty("transaction_id")]
        public string TransactionId { get; }
    }
}
