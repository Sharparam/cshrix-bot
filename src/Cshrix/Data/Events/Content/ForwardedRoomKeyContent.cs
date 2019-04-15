// <copyright file="ForwardedRoomKeyContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System.Collections.Generic;
    using System.Linq;

    using JetBrains.Annotations;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ForwardedRoomKeyContent : EventContent
    {
        public ForwardedRoomKeyContent([NotNull] IDictionary<string, object> dictionary)
            : base(dictionary)
        {
            Algorithm = GetValueOrDefault<string>("algorithm");
            var roomId = GetValueOrDefault<string>("room_id");
            RoomId = (Identifier)roomId;
            SenderKey = GetValueOrDefault<string>("sender_key");
            SessionId = GetValueOrDefault<string>("session_id");
            SessionKey = GetValueOrDefault<string>("session_key");
            SenderClaimedEd25519Key = GetValueOrDefault<string>("sender_claimed_ed25519_key");

            var keyChain = GetValueOrDefault<JArray>("forwarding_curve25519_key_chain");
            ForwardingCurve25519KeyChain = keyChain?.ToObject<IReadOnlyCollection<string>>() ??
                                           Enumerable.Empty<string>().ToList().AsReadOnly();
        }

        [JsonProperty("algorithm")]
        public string Algorithm { get; }

        [JsonProperty("room_id")]
        public Identifier RoomId { get; }

        [JsonProperty("sender_key")]
        public string SenderKey { get; }

        [JsonProperty("session_id")]
        public string SessionId { get; }

        [JsonProperty("session_key")]
        public string SessionKey { get; }

        [JsonProperty("sender_claimed_ed25519_key")]
        public string SenderClaimedEd25519Key { get; }

        [JsonProperty("forwarding_curve25519_key_chain")]
        public IReadOnlyCollection<string> ForwardingCurve25519KeyChain { get; }
    }
}
