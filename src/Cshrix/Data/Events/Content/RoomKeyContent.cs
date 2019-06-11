// <copyright file="RoomKeyContent.cs">
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
    /// Contains data for the <c>m.room_key</c> event.
    /// </summary>
    public sealed class RoomKeyContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomKeyContent" /> class.
        /// </summary>
        /// <param name="algorithm">The encryption algorithm the key is to be used with.</param>
        /// <param name="roomId">The ID of the room where the key is used.</param>
        /// <param name="sessionId">The ID of the session that the key is for.</param>
        /// <param name="sessionKey">The key to be exchanged.</param>
        public RoomKeyContent(string algorithm, string roomId, string sessionId, string sessionKey)
        {
            Algorithm = algorithm;
            RoomId = roomId;
            SessionId = sessionId;
            SessionKey = sessionKey;
        }

        /// <summary>
        /// Gets the encryption algorithm the key in the event is to be used with.
        /// </summary>
        /// <remarks>Must be <c>m.megolm.v1.aes-sha2</c>.</remarks>
        [JsonProperty("algorithm")]
        public string Algorithm { get; }

        /// <summary>
        /// Gets the ID of the room where the key is used.
        /// </summary>
        [JsonProperty("room_id")]
        public string RoomId { get; }

        /// <summary>
        /// Gets the ID of the session that the key is for.
        /// </summary>
        [JsonProperty("session_id")]
        public string SessionId { get; }

        /// <summary>
        /// Gets the key to be exchanged.
        /// </summary>
        [JsonProperty("session_key")]
        public string SessionKey { get; }
    }
}
