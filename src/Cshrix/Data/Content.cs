// <copyright file="Content.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;
    using System.Net.Mime;

    using JetBrains.Annotations;

    /// <summary>
    /// Contains content downloaded from a Matrix media repository.
    /// </summary>
    [PublicAPI]
    public readonly struct Content
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Content" /> structure.
        /// </summary>
        /// <param name="filename">The filename of the content.</param>
        /// <param name="contentType">The content type of the content.</param>
        /// <param name="bytes">The downloaded bytes of the content.</param>
        public Content([CanBeNull] string filename, ContentType contentType, IReadOnlyCollection<byte> bytes)
            : this()
        {
            Filename = filename;
            ContentType = contentType;
            Bytes = bytes;
        }

        /// <summary>
        /// Gets the filename of the downloaded content.
        /// </summary>
        [CanBeNull]
        public string Filename { get; }

        /// <summary>
        /// Gets the content type of the downloaded content.
        /// </summary>
        public ContentType ContentType { get; }

        /// <summary>
        /// Gets the downloaded content data.
        /// </summary>
        public IReadOnlyCollection<byte> Bytes { get; }
    }
}
