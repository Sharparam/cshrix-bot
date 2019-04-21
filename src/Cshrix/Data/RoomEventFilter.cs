// <copyright file="RoomEventFilter.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct RoomEventFilter
    {
        public RoomEventFilter(
            int? limit = null,
            [CanBeNull] IEnumerable<UserId> notSenders = null,
            [CanBeNull] IEnumerable<string> notTypes = null,
            [CanBeNull] IEnumerable<UserId> senders = null,
            [CanBeNull] IEnumerable<string> types = null,
            [CanBeNull] IEnumerable<string> notRooms = null,
            [CanBeNull] IEnumerable<string> rooms = null,
            bool containsUrl = false)
            : this(
                limit,
                notSenders?.ToList().AsReadOnly(),
                notTypes?.ToList().AsReadOnly(),
                senders?.ToList().AsReadOnly(),
                types?.ToList().AsReadOnly(),
                notRooms?.ToList().AsReadOnly(),
                rooms?.ToList().AsReadOnly(),
                containsUrl)
        {
        }

        [JsonConstructor]
        public RoomEventFilter(
            int? limit = null,
            [CanBeNull] IReadOnlyCollection<UserId> notSenders = null,
            [CanBeNull] IReadOnlyCollection<string> notTypes = null,
            [CanBeNull] IReadOnlyCollection<UserId> senders = null,
            [CanBeNull] IReadOnlyCollection<string> types = null,
            [CanBeNull] IReadOnlyCollection<string> notRooms = null,
            [CanBeNull] IReadOnlyCollection<string> rooms = null,
            bool containsUrl = false)
            : this()
        {
            Limit = limit;
            NotSenders = notSenders;
            NotTypes = notTypes;
            Senders = senders;
            Types = types;
            NotRooms = notRooms;
            Rooms = rooms;
            ContainsUrl = containsUrl;
        }

        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int? Limit { get; }

        [JsonProperty(
            "not_senders",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<UserId> NotSenders { get; }

        [JsonProperty(
            "not_types",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<string> NotTypes { get; }

        [JsonProperty(
            "senders",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<UserId> Senders { get; }

        [JsonProperty(
            "types",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<string> Types { get; }

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

        [DefaultValue(false)]
        [JsonProperty("contains_url")]
        public bool ContainsUrl { get; }
    }
}
