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

    /// <summary>
    /// Contains information on how to filter a request to list public rooms.
    /// </summary>
    public readonly struct PublicRoomsFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicRoomsFilter" /> structure.
        /// </summary>
        /// <param name="genericSearchTerm">A string to search for in the room metadata.</param>
        [JsonConstructor]
        public PublicRoomsFilter([CanBeNull] string genericSearchTerm)
            : this() =>
            GenericSearchTerm = genericSearchTerm;

        /// <summary>
        /// Gets a string to search for in the room metadata, e.g. name, topic, canonical alias et.c.
        /// </summary>
        [JsonProperty(
            "generic_search_term",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string GenericSearchTerm { get; }
    }
}
