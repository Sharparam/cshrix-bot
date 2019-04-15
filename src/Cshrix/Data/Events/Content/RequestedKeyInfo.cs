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

    public readonly struct RequestedKeyInfo
    {
        [JsonConstructor]
        public RequestedKeyInfo(string algorithm, Identifier roomId, string senderKey, string sessionId)
            : this()
        {
            Algorithm = algorithm;
            RoomId = roomId;
            SenderKey = senderKey;
            SessionId = sessionId;
        }

        [JsonProperty("algorithm")]
        public string Algorithm { get; }

        [JsonProperty("room_id")]
        public Identifier RoomId { get; }

        [JsonProperty("sender_key")]
        public string SenderKey { get; }

        [JsonProperty("session_id")]
        public string SessionId { get; }
    }
}
