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

    /// <summary>
    /// Describes a sticker message (<c>m.sticker</c>).
    /// </summary>
    public sealed class StickerContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StickerContent" /> class.
        /// </summary>
        /// <param name="body">A textual representation of the sticker.</param>
        /// <param name="info">Sticker image information.</param>
        /// <param name="uri">A URI to the sticker image.</param>
        public StickerContent(string body, ImageInfo info, Uri uri)
        {
            Body = body;
            Info = info;
            Uri = uri;
        }

        /// <summary>
        /// Gets a textual representation of the sticker image.
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; }

        /// <summary>
        /// Gets information about the sticker image.
        /// </summary>
        [JsonProperty("info")]
        public ImageInfo Info { get; }

        /// <summary>
        /// Gets a URI to the sticker image.
        /// </summary>
        [JsonProperty("url")]
        public Uri Uri { get; }
    }
}
