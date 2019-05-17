// <copyright file="UserSearchQuery.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Search
{
    using Newtonsoft.Json;

    /// <summary>
    /// Describes a search query for users.
    /// </summary>
    public readonly struct UserSearchQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSearchQuery" /> structure.
        /// </summary>
        /// <param name="searchTerm">The term to search for.</param>
        /// <param name="limit">The maximum number of results to return.</param>
        [JsonConstructor]
        public UserSearchQuery(string searchTerm, int limit = 10)
            : this()
        {
            SearchTerm = searchTerm;
            Limit = limit;
        }

        /// <summary>
        /// Gets the term to search for.
        /// </summary>
        [JsonProperty("search_term")]
        public string SearchTerm { get; }

        /// <summary>
        /// Gets the maximum number of results to return.
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; }
    }
}
