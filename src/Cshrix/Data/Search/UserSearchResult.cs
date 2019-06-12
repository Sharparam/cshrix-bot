// <copyright file="UserSearchResult.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Search
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains the results of a user search.
    /// </summary>
    public readonly struct UserSearchResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSearchResult" /> structure.
        /// </summary>
        /// <param name="limited"><c>true</c> if the results are limited; otherwise <c>false</c>.</param>
        /// <param name="results">The results of the search.</param>
        public UserSearchResult(bool limited, IEnumerable<SearchUser> results)
            : this(limited, results.ToList().AsReadOnly())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSearchResult" /> structure.
        /// </summary>
        /// <param name="limited"><c>true</c> if the results are limited; otherwise <c>false</c>.</param>
        /// <param name="results">The results of the search.</param>
        [JsonConstructor]
        public UserSearchResult(bool limited, IReadOnlyCollection<SearchUser> results)
            : this()
        {
            Limited = limited;
            Results = results;
        }

        /// <summary>
        /// Gets a value indicating whether the search result is limited.
        /// </summary>
        [JsonProperty("limited")]
        public bool Limited { get; }

        /// <summary>
        /// Gets a collection of users that matched the search.
        /// </summary>
        [JsonProperty("results")]
        public IReadOnlyCollection<SearchUser> Results { get; }
    }
}
