// <copyright file="EventFilter.cs">
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

    public readonly struct EventFilter
    {
        public EventFilter(
            int? limit = null,
            [CanBeNull] IEnumerable<Identifier> notSenders = null,
            [CanBeNull] IEnumerable<string> notTypes = null,
            [CanBeNull] IEnumerable<Identifier> senders = null,
            [CanBeNull] IEnumerable<string> types = null)
            : this(
                limit,
                notSenders?.ToList().AsReadOnly(),
                notTypes?.ToList().AsReadOnly(),
                senders?.ToList().AsReadOnly(),
                types?.ToList().AsReadOnly())
        {
        }

        [JsonConstructor]
        public EventFilter(
            int? limit = null,
            [CanBeNull] IReadOnlyCollection<Identifier> notSenders = null,
            [CanBeNull] IReadOnlyCollection<string> notTypes = null,
            [CanBeNull] IReadOnlyCollection<Identifier> senders = null,
            [CanBeNull] IReadOnlyCollection<string> types = null)
            : this()
        {
            Limit = limit;
            NotSenders = notSenders;
            NotTypes = notTypes;
            Senders = senders;
            Types = types;
        }

        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        [CanBeNull]
        public int? Limit { get; }

        [JsonProperty(
            "not_senders",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<Identifier> NotSenders { get; }

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
        public IReadOnlyCollection<Identifier> Senders { get; }

        [JsonProperty(
            "types",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<string> Types { get; }
    }
}
