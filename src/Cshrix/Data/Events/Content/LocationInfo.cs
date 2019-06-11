// <copyright file="LocationInfo.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains thumbnail information about a location from a location message.
    /// </summary>
    public readonly struct LocationInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationInfo" /> structure.
        /// </summary>
        /// <param name="thumbnailUri">URI to the thumbnail.</param>
        /// <param name="thumbnailFile">Encrypted thumbnail file.</param>
        /// <param name="thumbnailInfo">Information about the thumbnail.</param>
        [JsonConstructor]
        public LocationInfo([CanBeNull] Uri thumbnailUri, EncryptedFile? thumbnailFile, ThumbnailInfo thumbnailInfo)
            : this()
        {
            ThumbnailUri = thumbnailUri;
            ThumbnailFile = thumbnailFile;
            ThumbnailInfo = thumbnailInfo;
        }

        /// <summary>
        /// Gets the URI to the thumbnail.
        /// </summary>
        [JsonProperty("thumbnail_url")]
        [CanBeNull]
        public Uri ThumbnailUri { get; }

        /// <summary>
        /// Gets information about an encrypted thumbnail file.
        /// </summary>
        [JsonProperty("thumbnail_file")]
        public EncryptedFile? ThumbnailFile { get; }

        /// <summary>
        /// Gets information about the thumbnail.
        /// </summary>
        [JsonProperty("thumbnail_info")]
        public ThumbnailInfo ThumbnailInfo { get; }
    }
}
