// <copyright file="VideoMessageContent.cs">
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
    /// Contains information about a video message.
    /// </summary>
    public sealed class VideoMessageContent : UriMessageContent<VideoInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoMessageContent" /> class.
        /// </summary>
        /// <param name="body">Body text of the message (alt text).</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="info">Information about the video.</param>
        /// <param name="uri">URI to the video file.</param>
        /// <param name="encryptedFile">Information about an encrypted video file.</param>
        public VideoMessageContent(
            string body,
            string messageType,
            VideoInfo info,
            Uri uri,
            EncryptedFile? encryptedFile)
            : base(body, messageType, info, uri, encryptedFile)
        {
        }
    }
}
