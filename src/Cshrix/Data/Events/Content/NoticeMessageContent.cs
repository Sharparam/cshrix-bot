// <copyright file="NoticeMessageContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    /// <summary>
    /// Contains the contents of a notice message event.
    /// </summary>
    public sealed class NoticeMessageContent : FormattedMessageContent
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="NoticeMessageContent" /> class.
        /// </summary>
        /// <param name="body">The body of the notice.</param>
        /// <param name="messageType">The type of the message.</param>
        /// <param name="format">The type of formatting used.</param>
        /// <param name="formattedBody">The formatted body of the notice.</param>
        public NoticeMessageContent(string body, string messageType, string format, string formattedBody)
            : base(body, messageType, format, formattedBody)
        {
        }
    }
}
