// <copyright file="FileInfo.cs">
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
    /// Contains information about a file.
    /// </summary>
    public class FileInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfo" /> class.
        /// </summary>
        /// <param name="mimeType">The MIME type of the file.</param>
        /// <param name="size">The size of the file, in bytes.</param>
        /// <param name="thumbnailUri">A URI to a thumbnail for the file.</param>
        /// <param name="thumbnailFile">Information about an encrypted thumbnail.</param>
        /// <param name="thumbnailInfo">Information about the thumbnail.</param>
        public FileInfo(
            string mimeType,
            int size,
            [CanBeNull] Uri thumbnailUri,
            EncryptedFile? thumbnailFile,
            ThumbnailInfo thumbnailInfo)
        {
            MimeType = mimeType;
            Size = size;
            ThumbnailUri = thumbnailUri;
            ThumbnailFile = thumbnailFile;
            ThumbnailInfo = thumbnailInfo;
        }

        /// <summary>
        /// Gets the MIME type of the file.
        /// </summary>
        [JsonProperty("mimetype")]
        public string MimeType { get; }

        /// <summary>
        /// Gets the size of the file, in bytes.
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; }

        /// <summary>
        /// Gets a URI to a thumbnail for this file.
        /// </summary>
        [JsonProperty("thumbnail_url")]
        [CanBeNull]
        public Uri ThumbnailUri { get; }

        /// <summary>
        /// Gets information about an encrypted thumbnail for the file.
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
