// <copyright file="PublicRoomsFilter.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct PublicRoomsFilter
    {
        [JsonConstructor]
        public PublicRoomsFilter([CanBeNull] string genericSearchTerm)
            : this() =>
            GenericSearchTerm = genericSearchTerm;

        [JsonProperty(
            "generic_search_term",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string GenericSearchTerm { get; }
    }
}
