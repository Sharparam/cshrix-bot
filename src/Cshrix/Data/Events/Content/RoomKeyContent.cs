// <copyright file="RoomKeyContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System.Collections.Generic;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public class RoomKeyContent : EventContent
    {
        public RoomKeyContent([NotNull] IDictionary<string, object> dictionary)
            : base(dictionary)
        {
            Algorithm = GetValueOrDefault<string>("algorithm");
            var roomId = GetValueOrDefault<string>("room_id");
            RoomId = (Identifier)roomId;
            SessionId = GetValueOrDefault<string>("session_id");
            SessionKey = GetValueOrDefault<string>("session_key");
        }

        [JsonProperty("algorithm")]
        public string Algorithm { get; }

        [JsonProperty("room_id")]
        public Identifier RoomId { get; }

        [JsonProperty("session_id")]
        public string SessionId { get; }

        [JsonProperty("session_key")]
        public string SessionKey { get; }
    }
}
