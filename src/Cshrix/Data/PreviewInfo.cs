// <copyright file="PreviewInfo.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;
    using System.Net.Mime;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public struct PreviewInfo
    {
        [JsonConstructor]
        public PreviewInfo(
            [CanBeNull] string type,
            [CanBeNull] string title,
            [CanBeNull] string description,
            [CanBeNull] Uri uri,
            [CanBeNull] Uri imageUri,
            [CanBeNull] Uri alternateImageUri,
            [CanBeNull] Uri secureImageUri,
            [CanBeNull] ContentType imageType,
            int? imageHeight,
            int? imageWidth,
            long? imageSize,
            [CanBeNull] string imageAltText)
            : this()
        {
            Type = type;
            Title = title;
            Description = description;
            Uri = uri;
            ImageUri = imageUri;
            AlternateImageUri = alternateImageUri;
            SecureImageUri = secureImageUri;
            ImageType = imageType;
            ImageHeight = imageHeight;
            ImageWidth = imageWidth;
            ImageSize = imageSize;
            ImageAltText = imageAltText;
        }

        [JsonProperty("og:type")]
        [CanBeNull]
        public string Type { get; }

        [JsonProperty("og:title")]
        [CanBeNull]
        public string Title { get; }

        [JsonProperty("og:description")]
        [CanBeNull]
        public string Description { get; }

        [JsonProperty("og:url")]
        [CanBeNull]
        public Uri Uri { get; }

        [JsonProperty("og:image")]
        [CanBeNull]
        public Uri ImageUri { get; }

        [JsonProperty("og:image:url")]
        [CanBeNull]
        public Uri AlternateImageUri { get; }

        [JsonProperty("og:image:secure_url")]
        [CanBeNull]
        public Uri SecureImageUri { get; }

        [JsonProperty("og:image:type")]
        [CanBeNull]
        public ContentType ImageType { get; }

        [JsonProperty("og:image:height")]
        public int? ImageHeight { get; }

        [JsonProperty("og:image:width")]
        public int? ImageWidth { get; }

        [JsonProperty("matrix:image:size")]
        public long? ImageSize { get; }

        [JsonProperty("og:image:alt")]
        [CanBeNull]
        public string ImageAltText { get; }
    }
}
