// <copyright file="AudioInfo.cs">
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

    using Serialization;

    /// <summary>
    /// Describes an audio message.
    /// </summary>
    public readonly struct AudioInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioInfo" /> structure.
        /// </summary>
        /// <param name="mimeType">The mimetype of the audio.</param>
        /// <param name="size">The size of the audio clip, in bytes.</param>
        /// <param name="duration">The duration of the audio.</param>
        [JsonConstructor]
        public AudioInfo(
            string mimeType,
            int size,
            TimeSpan? duration)
            : this()
        {
            MimeType = mimeType;
            Size = size;
            Duration = duration;
        }

        /// <summary>
        /// Gets the mimetype of the audio, e.g. <c>audio/aac</c>.
        /// </summary>
        [JsonProperty("mimetype")]
        public string MimeType { get; }

        /// <summary>
        /// Gets the size of the audio clip, in bytes.
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; }

        /// <summary>
        /// Gets the duration of the audio.
        /// </summary>
        [JsonProperty("duration")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan? Duration { get; }
    }
}
