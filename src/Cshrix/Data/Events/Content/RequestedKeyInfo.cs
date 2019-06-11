// <copyright file="RequestedKeyInfo.cs">
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
    /// Contains information about a requested key.
    /// </summary>
    public readonly struct RequestedKeyInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestedKeyInfo" /> structure.
        /// </summary>
        /// <param name="algorithm">The encryption algorithm the key is to be used with.</param>
        /// <param name="roomId">The ID of the room where the key is used.</param>
        /// <param name="senderKey">The Curve25519 key of the device which initiated the session.</param>
        /// <param name="sessionId">The ID of the session that the key is for.</param>
        [JsonConstructor]
        public RequestedKeyInfo(string algorithm, string roomId, string senderKey, string sessionId)
            : this()
        {
            Algorithm = algorithm;
            RoomId = roomId;
            SenderKey = senderKey;
            SessionId = sessionId;
        }

        /// <summary>
        /// Gets the encryption algorithm the requested key is to be used with.
        /// </summary>
        [JsonProperty("algorithm")]
        public string Algorithm { get; }

        /// <summary>
        /// Gets the ID of the room where the key is used.
        /// </summary>
        [JsonProperty("room_id")]
        public string RoomId { get; }

        /// <summary>
        /// Gets the Curve25519 key of the device which initiated the session originally.
        /// </summary>
        [JsonProperty("sender_key")]
        public string SenderKey { get; }

        /// <summary>
        /// Gets the ID of the session that the key is for.
        /// </summary>
        [JsonProperty("session_id")]
        public string SessionId { get; }
    }
}
