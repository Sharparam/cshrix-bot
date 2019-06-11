// <copyright file="ImageMessageContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;

    /// <summary>
    /// Contains data for an image message event.
    /// </summary>
    public sealed class ImageMessageContent : UriMessageContent<ImageInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMessageContent" /> class.
        /// </summary>
        /// <param name="body">Body of the message (image alt text).</param>
        /// <param name="messageType">The type of the message.</param>
        /// <param name="info">Information about the image file.</param>
        /// <param name="uri">URI to the image.</param>
        /// <param name="encryptedFile">Information about the encrypted image file.</param>
        public ImageMessageContent(
            string body,
            string messageType,
            ImageInfo info,
            Uri uri,
            EncryptedFile? encryptedFile)
            : base(body, messageType, info, uri, encryptedFile)
        {
        }
    }
}
