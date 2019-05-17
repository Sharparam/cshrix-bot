// <copyright file="ContentUriContainer.cs">
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
    /// A wrapper object that contains a content URI.
    /// </summary>
    public readonly struct ContentUriContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentUriContainer" /> structure.
        /// </summary>
        /// <param name="contentUri">The content URI.</param>
        [JsonConstructor]
        public ContentUriContainer(Uri contentUri)
            : this() =>
            ContentUri = contentUri;

        /// <summary>
        /// Gets the content URI.
        /// </summary>
        [JsonProperty("content_uri")]
        public Uri ContentUri { get; }
    }
}
