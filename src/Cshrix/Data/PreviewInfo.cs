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

    /// <summary>
    /// Preview information about a URI. Any of the properties may be null or otherwise not present.
    /// </summary>
    public struct PreviewInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewInfo" /> structure.
        /// </summary>
        /// <param name="type">The type of page.</param>
        /// <param name="title">The title of the page.</param>
        /// <param name="description">The description of the page.</param>
        /// <param name="uri">The canonical URI of the page.</param>
        /// <param name="imageUri">A URI to an image representing the page.</param>
        /// <param name="alternateImageUri">A URI to an alternate image representing the page.</param>
        /// <param name="secureImageUri">A secure URI to an image representing the page.</param>
        /// <param name="imageType">Type of image.</param>
        /// <param name="imageHeight">Height of image.</param>
        /// <param name="imageWidth">Width of image.</param>
        /// <param name="imageSize">Size of image, in bytes.</param>
        /// <param name="imageAltText">Alternate text for the image.</param>
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

        /// <summary>
        /// Gets the type of the page.
        /// </summary>
        [JsonProperty("og:type")]
        [CanBeNull]
        public string Type { get; }

        /// <summary>
        /// Gets the title of the page.
        /// </summary>
        [JsonProperty("og:title")]
        [CanBeNull]
        public string Title { get; }

        /// <summary>
        /// Gets the description of the page.
        /// </summary>
        [JsonProperty("og:description")]
        [CanBeNull]
        public string Description { get; }

        /// <summary>
        /// Gets the canonical URI of the page.
        /// </summary>
        [JsonProperty("og:url")]
        [CanBeNull]
        public Uri Uri { get; }

        /// <summary>
        /// Gets an image URI that represents the page.
        /// </summary>
        [JsonProperty("og:image")]
        [CanBeNull]
        public Uri ImageUri { get; }

        /// <summary>
        /// Gets a URI for an alternate image representing the page.
        /// </summary>
        [JsonProperty("og:image:url")]
        [CanBeNull]
        public Uri AlternateImageUri { get; }

        /// <summary>
        /// Gets a secure URI for the image representing the page.
        /// </summary>
        [JsonProperty("og:image:secure_url")]
        [CanBeNull]
        public Uri SecureImageUri { get; }

        /// <summary>
        /// Gets the type of the image contained in <see cref="ImageUri" />.
        /// </summary>
        [JsonProperty("og:image:type")]
        [CanBeNull]
        public ContentType ImageType { get; }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        [JsonProperty("og:image:height")]
        public int? ImageHeight { get; }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        [JsonProperty("og:image:width")]
        public int? ImageWidth { get; }

        /// <summary>
        /// Gets the size of the image, in bytes.
        /// </summary>
        [JsonProperty("matrix:image:size")]
        public long? ImageSize { get; }

        /// <summary>
        /// Gets the alternative text of the image.
        /// </summary>
        [JsonProperty("og:image:alt")]
        [CanBeNull]
        public string ImageAltText { get; }
    }
}
