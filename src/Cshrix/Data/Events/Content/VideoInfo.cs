// <copyright file="VideoInfo.cs">
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

    using Serialization;

    /// <summary>
    /// Describes a video file.
    /// </summary>
    public sealed class VideoInfo : FileInfo
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoInfo" /> class.
        /// </summary>
        /// <param name="mimeType">The MIME type of the file.</param>
        /// <param name="size">The size of the file, in bytes.</param>
        /// <param name="thumbnailUri">URI to a thumbnail for the video.</param>
        /// <param name="thumbnailFile">Information about an encrypted thumbnail file.</param>
        /// <param name="thumbnailInfo">Information about the thumbnail.</param>
        /// <param name="duration">The duration of the video.</param>
        /// <param name="height">The height of the video, in pixels.</param>
        /// <param name="width">The width of the video, in pixels.</param>
        public VideoInfo(
            string mimeType,
            int size,
            [CanBeNull] Uri thumbnailUri,
            EncryptedFile? thumbnailFile,
            ThumbnailInfo thumbnailInfo,
            TimeSpan? duration,
            int height,
            int width)
            : base(mimeType, size, thumbnailUri, thumbnailFile, thumbnailInfo)
        {
            Duration = duration;
            Height = height;
            Width = width;
        }

        /// <summary>
        /// Gets the duration of the video.
        /// </summary>
        [JsonProperty("duration")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan? Duration { get; }

        /// <summary>
        /// Gets the height of the video, in pixels.
        /// </summary>
        [JsonProperty("h")]
        public int Height { get; }

        /// <summary>
        /// Gets the width of the video, in pixels.
        /// </summary>
        [JsonProperty("w")]
        public int Width { get; }
    }
}
