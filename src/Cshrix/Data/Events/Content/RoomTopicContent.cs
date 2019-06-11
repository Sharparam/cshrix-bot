// <copyright file="RoomTopicContent.cs">
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
    /// Contains the topic of a room (from the <c>m.room.topic</c> event).
    /// </summary>
    public sealed class RoomTopicContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomTopicContent" /> class.
        /// </summary>
        /// <param name="topic">The topic of the room.</param>
        public RoomTopicContent(string topic) => Topic = topic;

        /// <summary>
        /// Gets the topic of the room.
        /// </summary>
        [JsonProperty("topic")]
        public string Topic { get; }
    }
}
