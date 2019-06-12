// <copyright file="AvatarContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains the data of an event of type <c>m.room.avatar</c>.
    /// </summary>
    public sealed class AvatarContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvatarContent" /> class.
        /// </summary>
        /// <param name="info">Information about the avatar image file.</param>
        /// <param name="uri">URI to use for downloading the avatar file.</param>
        public AvatarContent(ImageInfo info, Uri uri)
        {
            Info = info;
            Uri = uri;
        }

        /// <summary>
        /// Gets image information for the avatar.
        /// </summary>
        [JsonProperty("info")]
        public ImageInfo Info { get; }

        /// <summary>
        /// Gets a URI to use for downloading the avatar file.
        /// </summary>
        [JsonProperty("url")]
        public Uri Uri { get; }
    }
}
