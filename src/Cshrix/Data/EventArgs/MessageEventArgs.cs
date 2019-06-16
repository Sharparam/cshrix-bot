// <copyright file="MessageEventArgs.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;

    /// <summary>
    /// Contains data for a message event.
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventArgs" /> class.
        /// </summary>
        /// <param name="message">The message that was sent.</param>
        public MessageEventArgs(Message message) => Message = message;

        /// <summary>
        /// Gets the message that was sent.
        /// </summary>
        public Message Message { get; }
    }
}
