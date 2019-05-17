// <copyright file="SearchResult.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Search
{
    using Events;

    using Newtonsoft.Json;

    /// <summary>
    /// Describes a result event from a search.
    /// </summary>
    public readonly struct SearchEventResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchEventResult" /> structure.
        /// </summary>
        /// <param name="rank">A number describing how closely the result matches the search.</param>
        /// <param name="result">The event that matched.</param>
        /// <param name="context">Context for the result.</param>
        [JsonConstructor]
        public SearchEventResult(int rank, Event result, SearchEventContext? context)
            : this()
        {
            Rank = rank;
            Result = result;
            Context = context;
        }

        /// <summary>
        /// Gets a number that describes how closely this result matches the search. Higher is closer.
        /// </summary>
        [JsonProperty("rank")]
        public int Rank { get; }

        /// <summary>
        /// Gets the event that matched.
        /// </summary>
        [JsonProperty("result")]
        public Event Result { get; }

        /// <summary>
        /// Gets the context for the result, if requested.
        /// </summary>
        [JsonProperty("context")]
        public SearchEventContext? Context { get; }
    }
}
