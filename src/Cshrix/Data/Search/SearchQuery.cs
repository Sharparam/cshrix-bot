// <copyright file="SearchQuery.cs">
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
    /// A search query for events.
    /// </summary>
    public readonly struct SearchQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchQuery" /> structure.
        /// </summary>
        /// <param name="categories">The search categories.</param>
        [JsonConstructor]
        public SearchQuery(SearchCategories categories)
            : this() =>
            Categories = categories;

        /// <summary>
        /// Gets an object containing the search categories to search in, and their criteria.
        /// </summary>
        [JsonProperty("search_categories")]
        public SearchCategories Categories { get; }
    }
}
