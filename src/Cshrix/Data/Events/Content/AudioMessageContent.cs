// <copyright file="AudioMessageContent.cs">
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

    /// <summary>
    /// Contains the content for a message event of type <c>m.audio</c>.
    /// </summary>
    public sealed class AudioMessageContent : UriMessageContent<AudioInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioMessageContent" /> class.
        /// </summary>
        /// <param name="body">The body text of the message.</param>
        /// <param name="messageType">The type of the message.</param>
        /// <param name="info">Information about the audio clip.</param>
        /// <param name="uri">The URI of the audio clip.</param>
        /// <param name="encryptedFile">Information about the encrypted audio file, if any.</param>
        public AudioMessageContent(
            string body,
            string messageType,
            AudioInfo info,
            [CanBeNull] Uri uri,
            EncryptedFile? encryptedFile)
            : base(body, messageType, info, uri, encryptedFile)
        {
        }
    }
}
