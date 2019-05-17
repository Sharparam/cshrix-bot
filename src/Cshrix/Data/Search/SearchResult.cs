// <copyright file="SearchResult.cs">
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
    /// Contains a search result.
    /// </summary>
    public readonly struct SearchResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResult" /> structure.
        /// </summary>
        /// <param name="categories">The search results for each requested category.</param>
        [JsonConstructor]
        public SearchResult(SearchResultCategories categories)
            : this() =>
            Categories = categories;

        /// <summary>
        /// An object containing the search results for each category that was requested.
        /// </summary>
        [JsonProperty("search_categories")]
        public SearchResultCategories Categories { get; }
    }
}
