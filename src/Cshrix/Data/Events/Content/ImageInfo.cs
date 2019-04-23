// <copyright file="ImageInfo.cs">
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

    public class ImageInfo : FileInfo
    {
        public ImageInfo(
            string mimeType,
            int size,
            [CanBeNull] Uri thumbnailUri,
            EncryptedFile? thumbnailFile,
            ThumbnailInfo thumbnailInfo,
            int height,
            int width)
            : base(mimeType, size, thumbnailUri, thumbnailFile, thumbnailInfo)
        {
            Height = height;
            Width = width;
        }

        [JsonProperty("h")]
        public int Height { get; }

        [JsonProperty("w")]
        public int Width { get; }
    }
}
