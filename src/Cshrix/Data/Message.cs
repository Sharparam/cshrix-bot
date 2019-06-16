// <copyright file="Message.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;

    using Events.Content;

    using Newtonsoft.Json;

    /// <summary>
    /// An instant message.
    /// </summary>
    public readonly struct Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message" /> structure.
        /// </summary>
        /// <param name="senderId">The ID of the sender.</param>
        /// <param name="room">The room in which the message was sent.</param>
        /// <param name="sentAt">The date and time the message was sent.</param>
        /// <param name="type">The type of the message content.</param>
        /// <param name="content">The message content.</param>
        [JsonConstructor]
        public Message(UserId senderId, IRoom room, DateTimeOffset sentAt, string type, MessageContent content)
            : this()
        {
            SenderId = senderId;
            Room = room;
            SentAt = sentAt;
            Type = type;
            Content = content;
        }

        /// <summary>
        /// Gets the ID of the user that sent the message.
        /// </summary>
        public UserId SenderId { get; }

        /// <summary>
        /// Gets the room in which the message was sent.
        /// </summary>
        public IRoom Room { get; }

        /// <summary>
        /// Gets the date and time at which the message was sent.
        /// </summary>
        public DateTimeOffset SentAt { get; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets the contents of the message.
        /// </summary>
        public MessageContent Content { get; }
    }
}
