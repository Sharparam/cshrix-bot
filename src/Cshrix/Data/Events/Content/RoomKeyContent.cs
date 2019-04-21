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

    public class RoomKeyContent : EventContent
    {
        public RoomKeyContent(string algorithm, string roomId, string sessionId, string sessionKey)
        {
            Algorithm = algorithm;
            RoomId = roomId;
            SessionId = sessionId;
            SessionKey = sessionKey;
        }

        [JsonProperty("algorithm")]
        public string Algorithm { get; }

        [JsonProperty("room_id")]
        public string RoomId { get; }

        [JsonProperty("session_id")]
        public string SessionId { get; }

        [JsonProperty("session_key")]
        public string SessionKey { get; }
    }
}
