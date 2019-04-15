// <copyright file="StickerContent.cs">
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

    public class StickerContent : EventContent
    {
        public StickerContent(string body, ImageInfo info, Uri uri)
        {
            Body = body;
            Info = info;
            Uri = uri;
        }

        [JsonProperty("body")]
        public string Body { get; }

        [JsonProperty("info")]
        public ImageInfo Info { get; }

        [JsonProperty("url")]
        public Uri Uri { get; }
    }
}
