// <copyright file="BaseUriContainer.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// Gets an object containing a base URI.
    /// </summary>
    public readonly struct BaseUriContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseUriContainer" /> structure.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        [JsonConstructor]
        public BaseUriContainer(Uri baseUri)
            : this() =>
            BaseUri = baseUri;

        /// <summary>
        /// Gets the base URI.
        /// </summary>
        [JsonProperty("base_url")]
        public Uri BaseUri { get; }
    }
}
