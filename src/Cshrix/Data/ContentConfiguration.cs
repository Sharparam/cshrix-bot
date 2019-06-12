// <copyright file="ContentConfiguration.cs">
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
    /// Contains homeserver content configuration.
    /// </summary>
    public readonly struct ContentConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentConfiguration" /> structure.
        /// </summary>
        /// <param name="maxSize">Maximum upload size, in bytes.</param>
        [JsonConstructor]
        public ContentConfiguration(long? maxSize)
            : this() =>
            MaxSize = maxSize;

        /// <summary>
        /// Gets the maximum upload size, in bytes.
        /// </summary>
        [JsonProperty("m.upload.size")]
        public long? MaxSize { get; }
    }
}
