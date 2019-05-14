// <copyright file="SendToDeviceMessages.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Events.Content;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains messages to send to specified client devices.
    /// </summary>
    public readonly struct SendToDeviceMessages
    {
        /// <inheritdoc />
        public SendToDeviceMessages(IDictionary<UserId, IDictionary<string, EventContent>> messages)
            : this(
                new ReadOnlyDictionary<UserId, IReadOnlyDictionary<string, EventContent>>(
                    messages.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IReadOnlyDictionary<string, EventContent>)new ReadOnlyDictionary<string, EventContent>(
                            kvp.Value))))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendToDeviceMessages" /> structure.
        /// </summary>
        /// <param name="messages">Dictionary of users to a dictionary of device ID to message.</param>
        [JsonConstructor]
        public SendToDeviceMessages(IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, EventContent>> messages)
            : this() =>
            Messages = messages;

        /// <summary>
        /// Gets a dictionary mapping users to a dictionary that maps device IDs to a message to send.
        /// </summary>
        [JsonProperty("messages")]
        public IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, EventContent>> Messages { get; }
    }
}
