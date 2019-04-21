// <copyright file="RoomFilter.cs">
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

    public readonly struct RoomFilter
    {
        public RoomFilter(
            EventFilter? accountData = null,
            EventFilter? ephemeral = null,
            bool includeLeave = false,
            [CanBeNull] IEnumerable<string> notRooms = null,
            [CanBeNull] IEnumerable<string> rooms = null,
            EventFilter? state = null,
            EventFilter? timeline = null)
            : this(
                accountData,
                ephemeral,
                includeLeave,
                notRooms?.ToList().AsReadOnly(),
                rooms?.ToList().AsReadOnly(),
                state,
                timeline)
        {
        }

        [JsonConstructor]
        public RoomFilter(
            EventFilter? accountData = null,
            EventFilter? ephemeral = null,
            bool includeLeave = false,
            [CanBeNull] IReadOnlyCollection<string> notRooms = null,
            [CanBeNull] IReadOnlyCollection<string> rooms = null,
            EventFilter? state = null,
            EventFilter? timeline = null)
            : this()
        {
            AccountData = accountData;
            Ephemeral = ephemeral;
            IncludeLeave = includeLeave;
            NotRooms = notRooms;
            Rooms = rooms;
            State = state;
            Timeline = timeline;
        }

        [JsonProperty(
            "account_data",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EventFilter? AccountData { get; }

        [JsonProperty(
            "ephemeral",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EventFilter? Ephemeral { get; }

        [JsonProperty("include_leave")]
        public bool IncludeLeave { get; }

        [JsonProperty(
            "not_rooms",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<string> NotRooms { get; }

        [JsonProperty(
            "rooms",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<string> Rooms { get; }

        [JsonProperty(
            "state",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EventFilter? State { get; }

        [JsonProperty(
            "timeline",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EventFilter? Timeline { get; }
    }
}
