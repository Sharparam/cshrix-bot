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

    public class VideoInfo : FileInfo
    {
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

        [JsonProperty("duration")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan? Duration { get; }

        [JsonProperty("h")]
        public int Height { get; }

        [JsonProperty("w")]
        public int Width { get; }
    }
}
