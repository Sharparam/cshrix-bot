// <copyright file="SearchGroupings.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Search
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Specifies how to group search results.
    /// </summary>
    public readonly struct SearchGroupings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchGroupings" /> structure.
        /// </summary>
        /// <param name="groups">Grouping configurations.</param>
        [JsonConstructor]
        public SearchGroupings(IReadOnlyCollection<SearchGroup> groups)
            : this() =>
            Groups = groups;

        /// <summary>
        /// Gets a collection of grouping configurations.
        /// </summary>
        [JsonProperty("group_by")]
        public IReadOnlyCollection<SearchGroup> Groups { get; }
    }
}
