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

    public readonly struct ThumbnailInfo
    {
        [JsonConstructor]
        public ThumbnailInfo(int height, int width, string mimeType, int size)
            : this()
        {
            Height = height;
            Width = width;
            MimeType = mimeType;
            Size = size;
        }

        [JsonProperty("h")]
        public int Height { get; }

        [JsonProperty("w")]
        public int Width { get; }

        [JsonProperty("mimetype")]
        public string MimeType { get; }

        [JsonProperty("size")]
        public int Size { get; }
    }
}
