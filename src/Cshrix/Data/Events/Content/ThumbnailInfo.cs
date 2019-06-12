// <copyright file="ThumbnailInfo.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a thumbnail.
    /// </summary>
    public readonly struct ThumbnailInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThumbnailInfo" /> structure.
        /// </summary>
        /// <param name="height">Height of the thumbnail, in pixels.</param>
        /// <param name="width">Width of the thumbnail, in pixels.</param>
        /// <param name="mimeType">MIME type of the thumbnail.</param>
        /// <param name="size">Size of the thumbnail, in bytes.</param>
        [JsonConstructor]
        public ThumbnailInfo(int height, int width, string mimeType, int size)
            : this()
        {
            Height = height;
            Width = width;
            MimeType = mimeType;
            Size = size;
        }

        /// <summary>
        /// Gets the height of the thumbnail, in pixels.
        /// </summary>
        [JsonProperty("h")]
        public int Height { get; }

        /// <summary>
        /// Gets the width of the thumbnail, in pixels.
        /// </summary>
        [JsonProperty("w")]
        public int Width { get; }

        /// <summary>
        /// Gets the MIME type of the thumbnail.
        /// </summary>
        [JsonProperty("mimetype")]
        public string MimeType { get; }

        /// <summary>
        /// Gets the size of the thumbnail, in bytes.
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; }
    }
}
