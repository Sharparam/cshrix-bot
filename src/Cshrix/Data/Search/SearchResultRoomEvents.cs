// <copyright file="SearchResultRoomEvents.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Search
{
    using System.Collections.Generic;

    using Events;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains information on room events that matched a search.
    /// </summary>
    public readonly struct SearchResultRoomEvents
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultRoomEvents" /> structure.
        /// </summary>
        /// <param name="count">An approximate count of the total number of results.</param>
        /// <param name="highlights">A collection of words that should be highlighted.</param>
        /// <param name="results">The search results in requested order.</param>
        /// <param name="stateEvents">A dictionary of state events for each included room.</param>
        /// <param name="groups">The requested dictionary of groups.</param>
        /// <param name="nextBatchToken">A token that can be used to fetch further search results.</param>
        [JsonConstructor]
        public SearchResultRoomEvents(
            int count,
            IReadOnlyCollection<string> highlights,
            IReadOnlyCollection<SearchEventResult> results,
            [CanBeNull] IReadOnlyDictionary<string, IReadOnlyCollection<StateEvent>> stateEvents,
            IReadOnlyDictionary<string, IReadOnlyDictionary<string, SearchGroupValue>> groups,
            [CanBeNull] string nextBatchToken)
            : this()
        {
            Count = count;
            Highlights = highlights;
            Results = results;
            StateEvents = stateEvents;
            Groups = groups;
            NextBatchToken = nextBatchToken;
        }

        /// <summary>
        /// Gets an approximate count of the total number of results found.
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; }

        /// <summary>
        /// Gets a collection of words that should be highlighted.
        /// </summary>
        [JsonProperty("highlights")]
        public IReadOnlyCollection<string> Highlights { get; }

        /// <summary>
        /// Gets a collection of search results in the requested order.
        /// </summary>
        [JsonProperty("results")]
        public IReadOnlyCollection<SearchEventResult> Results { get; }

        /// <summary>
        /// Gets a mapping from room ID to a collection of state events for that room.
        /// </summary>
        /// <remarks>
        /// If <see cref="SearchCriteria.IncludeState" /> was <c>false</c>, this will be <c>null</c>.
        /// </remarks>
        [JsonProperty("state")]
        [CanBeNull]
        public IReadOnlyDictionary<string, IReadOnlyCollection<StateEvent>> StateEvents { get; }

        /// <summary>
        /// Gets a mapping of groups that were requested. The key is the group key requested, the key of the inner
        /// dictionary is the grouped value (e.g. a room's ID or a user's ID).
        /// </summary>
        [JsonProperty("groups")]
        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, SearchGroupValue>> Groups { get; }

        /// <summary>
        /// Gets a token that can be used to get the next batch of results, by passing as the <c>next_batch</c>
        /// parameter to the next search call.
        /// </summary>
        /// <remarks>If this property is <c>null</c>, there are no more results.</remarks>
        [JsonProperty("next_batch")]
        [CanBeNull]
        public string NextBatchToken { get; }
    }
}
