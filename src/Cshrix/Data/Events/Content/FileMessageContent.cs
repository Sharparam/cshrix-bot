// <copyright file="FileMessageContent.cs">
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
    /// Contains information about a message event that has a file.
    /// </summary>
    public sealed class FileMessageContent : UriMessageContent<FileInfo>
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="FileMessageContent" /> class.
        /// </summary>
        /// <param name="body">The body of the message (used as alt text).</param>
        /// <param name="messageType">The message type.</param>
        /// <param name="info">Information about the file.</param>
        /// <param name="uri">URI of the file.</param>
        /// <param name="encryptedFile">Information about the encrypted file.</param>
        public FileMessageContent(string body, string messageType, FileInfo info, Uri uri, EncryptedFile? encryptedFile)
            : base(body, messageType, info, uri, encryptedFile)
        {
        }
    }
}
