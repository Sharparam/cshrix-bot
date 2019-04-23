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

    public readonly struct LocationInfo
    {
        [JsonConstructor]
        public LocationInfo([CanBeNull] Uri thumbnailUri, EncryptedFile? thumbnailFile, ThumbnailInfo thumbnailInfo)
            : this()
        {
            ThumbnailUri = thumbnailUri;
            ThumbnailFile = thumbnailFile;
            ThumbnailInfo = thumbnailInfo;
        }

        [JsonProperty("thumbnail_url")]
        [CanBeNull]
        public Uri ThumbnailUri { get; }

        [JsonProperty("thumbnail_file")]
        public EncryptedFile? ThumbnailFile { get; }

        [JsonProperty("thumbnail_info")]
        public ThumbnailInfo ThumbnailInfo { get; }
    }
}
