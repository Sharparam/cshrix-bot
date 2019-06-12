// <copyright file="AvatarUriContainer.cs">
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
    /// An object containing an avatar URI.
    /// </summary>
    public readonly struct AvatarUriContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvatarUriContainer" /> structure.
        /// </summary>
        /// <param name="uri">The avatar URI.</param>
        [JsonConstructor]
        public AvatarUriContainer(Uri uri)
            : this() =>
            Uri = uri;

        /// <summary>
        /// Gets the avatar URI.
        /// </summary>
        [JsonProperty("avatar_url")]
        public Uri Uri { get; }
    }
}
