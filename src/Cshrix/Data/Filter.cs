// <copyright file="Filter.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct Filter
    {
        public Filter(
            EventFilter? accountData = null,
            [CanBeNull] IEnumerable<string> eventFields = null,
            EventFormat? eventFormat = null,
            EventFilter? presence = null,
            RoomFilter? room = null)
            : this(accountData, eventFields?.ToList().AsReadOnly(), eventFormat, presence, room)
        {
        }

        [JsonConstructor]
        public Filter(
            EventFilter? accountData = null,
            [CanBeNull] IReadOnlyCollection<string> eventFields = null,
            EventFormat? eventFormat = null,
            EventFilter? presence = null,
            RoomFilter? room = null)
            : this()
        {
            AccountData = accountData;
            EventFields = eventFields;
            EventFormat = eventFormat;
            Presence = presence;
            Room = room;
        }

        [JsonProperty(
            "account_data",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EventFilter? AccountData { get; }

        [JsonProperty(
            "event_fields",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<string> EventFields { get; }

        [JsonProperty("event_format", NullValueHandling = NullValueHandling.Ignore)]
        public EventFormat? EventFormat { get; }

        [JsonProperty(
            "presence",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EventFilter? Presence { get; }

        [JsonProperty(
            "room",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public RoomFilter? Room { get; }
    }
}
