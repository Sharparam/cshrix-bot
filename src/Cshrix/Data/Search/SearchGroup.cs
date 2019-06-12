// <copyright file="SearchGroup.cs">
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
    /// Configuration for a search grouping.
    /// </summary>
    public readonly struct SearchGroup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchGroup" /> structure.
        /// </summary>
        /// <param name="key">The key to group by.</param>
        [JsonConstructor]
        public SearchGroup(SearchGroupKey key)
            : this() =>
            Key = key;

        /// <summary>
        /// Gets the key to group by.
        /// </summary>
        [JsonProperty("key")]
        public SearchGroupKey Key { get; }
    }
}
