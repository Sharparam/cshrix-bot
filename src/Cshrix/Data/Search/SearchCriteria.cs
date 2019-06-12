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

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains search criteria.
    /// </summary>
    public readonly struct SearchCriteria
    {
        /// <summary>
        /// The default search ordering.
        /// </summary>
        private const SearchOrdering DefaultOrdering = SearchOrdering.Rank;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCriteria" /> structure.
        /// </summary>
        /// <param name="searchTerm">The term to search for.</param>
        /// <param name="keys">Event keys to search in.</param>
        /// <param name="filter">A filter to apply to the results.</param>
        /// <param name="ordering">A value specifying how to order the search results.</param>
        /// <param name="contextConfiguration">Configuration specifying what context should be included.</param>
        /// <param name="includeState">A value indicating whether current room state should be included.</param>
        /// <param name="groupings">Configures how search results should be grouped.</param>
        [JsonConstructor]
        public SearchCriteria(
            string searchTerm,
            SearchKeys? keys = null,
            [CanBeNull] RoomEventFilter filter = null,
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

        /// <summary>
        /// Gets the term to search for in events.
        /// </summary>
        [JsonProperty("search_term")]
        public string SearchTerm { get; }

        /// <summary>
        /// Gets which keys to search in on events.
        /// </summary>
        [JsonProperty("keys", NullValueHandling = NullValueHandling.Ignore)]
        public SearchKeys? Keys { get; }

        /// <summary>
        /// Gets a filter to apply to the search results.
        /// </summary>
        [JsonProperty(
            "filter",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public RoomEventFilter Filter { get; }

        /// <summary>
        /// Gets the ordering to apply to search results.
        /// </summary>
        [DefaultValue(DefaultOrdering)]
        [JsonProperty("order_by")]
        public SearchOrdering Ordering { get; }

        /// <summary>
        /// Gets a configuration deciding what, if any, context should be included.
        /// </summary>
        [JsonProperty("event_context", NullValueHandling = NullValueHandling.Ignore)]
        public SearchContextConfiguration? ContextConfiguration { get; }

        /// <summary>
        /// Gets a value indicating whether the current state for each room should be returned in the results.
        /// </summary>
        [DefaultValue(false)]
        [JsonProperty("include_state")]
        public bool IncludeState { get; }

        /// <summary>
        /// Gets a configuration telling the server how to group the search results.
        /// </summary>
        [JsonProperty(
            "groupings",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SearchGroupings? Groupings { get; }
    }
}
