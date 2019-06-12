// <copyright file="FilterIdContainer.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Newtonsoft.Json;

    /// <summary>
    /// An object containing a filter ID.
    /// </summary>
    public readonly struct FilterIdContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterIdContainer" /> structure.
        /// </summary>
        /// <param name="id">The filter ID.</param>
        [JsonConstructor]
        public FilterIdContainer(string id)
            : this() =>
            Id = id;

        /// <summary>
        /// Gets the filter ID.
        /// </summary>
        [JsonProperty("filter_id")]
        public string Id { get; }
    }
}
