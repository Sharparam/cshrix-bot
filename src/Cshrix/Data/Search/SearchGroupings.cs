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

    public readonly struct SearchGroupings
    {
        [JsonConstructor]
        public SearchGroupings(IReadOnlyCollection<SearchGroup> groups)
            : this() =>
            Groups = groups;

        [JsonProperty("group_by")]
        public IReadOnlyCollection<SearchGroup> Groups { get; }
    }
}
