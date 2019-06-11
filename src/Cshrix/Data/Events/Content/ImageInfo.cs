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

    /// <summary>
    /// Contains information about an image file.
    /// </summary>
    public sealed class ImageInfo : FileInfo
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageInfo" /> class.
        /// </summary>
        /// <param name="mimeType">The MIME type of the image.</param>
        /// <param name="size">The size of the image, in bytes.</param>
        /// <param name="thumbnailUri">A URL to the thumbnail for the image.</param>
        /// <param name="thumbnailFile">An object describing an encrypted thumbnail for the image.</param>
        /// <param name="thumbnailInfo">Information about the image thumbnail.</param>
        /// <param name="height">Height of the image, in pixels.</param>
        /// <param name="width">Width of the image, in pixels.</param>
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

        /// <summary>
        /// Gets the height of the image, in pixels.
        /// </summary>
        [JsonProperty("h")]
        public int Height { get; }

        /// <summary>
        /// Gets the width of the image, in pixels.
        /// </summary>
        [JsonProperty("w")]
        public int Width { get; }
    }
}
