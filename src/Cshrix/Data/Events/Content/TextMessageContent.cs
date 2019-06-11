// <copyright file="TextMessageContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    /// <summary>
    /// A regular text message (<c>m.text</c>).
    /// </summary>
    public sealed class TextMessageContent : FormattedMessageContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextMessageContent" /> class.
        /// </summary>
        /// <param name="body">The text message.</param>
        /// <param name="messageType">The type of the message.</param>
        /// <param name="format">The type of formatting used to format the message.</param>
        /// <param name="formattedBody">The formatted message body.</param>
        public TextMessageContent(string body, string messageType, string format, string formattedBody)
            : base(body, messageType, format, formattedBody)
        {
        }
    }
}
