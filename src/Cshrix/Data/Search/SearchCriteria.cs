// <copyright file="RoomEventsCriteria.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Search
{
    using System.ComponentModel;

    using Newtonsoft.Json;

    public readonly struct SearchCriteria
    {
        public const SearchOrdering DefaultOrdering = SearchOrdering.Rank;

        [JsonConstructor]
        public SearchCriteria(
            string searchTerm,
            SearchKeys? keys = null,
            RoomEventFilter? filter = null,
            SearchOrdering ordering = DefaultOrdering,
            SearchContextConfiguration? contextConfiguration = null,
            bool includeState = false,
            SearchGroupings? groupings = null)
            : this()
        {
            SearchTerm = searchTerm;
            Keys = keys;
            Filter = filter;
            Ordering = ordering;
            ContextConfiguration = contextConfiguration;
            IncludeState = includeState;
            Groupings = groupings;
        }

        [JsonProperty("search_term")]
        public string SearchTerm { get; }

        [JsonProperty("keys", NullValueHandling = NullValueHandling.Ignore)]
        public SearchKeys? Keys { get; }

        [JsonProperty(
            "filter",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public RoomEventFilter? Filter { get; }

        [DefaultValue(DefaultOrdering)]
        [JsonProperty("order_by")]
        public SearchOrdering Ordering { get; }

        [JsonProperty("event_context", NullValueHandling = NullValueHandling.Ignore)]
        public SearchContextConfiguration? ContextConfiguration { get; }

        [DefaultValue(false)]
        [JsonProperty("include_state")]
        public bool IncludeState { get; }

        [JsonProperty(
            "groupings",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SearchGroupings? Groupings { get; }
    }
}
