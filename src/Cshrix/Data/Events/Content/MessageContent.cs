// <copyright file="MessageContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using Newtonsoft.Json;

    /// <summary>
    /// Contains the contents of a message event.
    /// </summary>
    public class MessageContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageContent" /> class.
        /// </summary>
        /// <param name="body">The body of the message.</param>
        /// <param name="messageType">The type of the message.</param>
        public MessageContent(string body, string messageType)
        {
            Body = body;
            MessageType = messageType;
        }

        /// <summary>
        /// Gets the body of the message.
        /// </summary>
        [JsonProperty("body")]
        public virtual string Body { get; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        [JsonProperty("msgtype")]
        public string MessageType { get; }
    }
}
