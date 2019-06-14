// <copyright file="TombstoneContent.cs">
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
    /// Contains the data for a <c>m.room.tombstone</c> event.
    /// </summary>
    public sealed class TombstoneContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TombstoneContent" /> class.
        /// </summary>
        /// <param name="message">A server-defined message.</param>
        /// <param name="replacementRoomId">The ID of the new room the client should be visiting.</param>
        public TombstoneContent(string message, string replacementRoomId)
        {
            Message = message;
            ReplacementRoomId = replacementRoomId;
        }

        /// <summary>
        /// Gets a server-defined message.
        /// </summary>
        [JsonProperty("body")]
        public string Message { get; }

        /// <summary>
        /// Gets the ID of the new room the client should be visiting.
        /// </summary>
        [JsonProperty("replacement_room")]
        public string ReplacementRoomId { get; }
    }
}
