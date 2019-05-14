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

    using Newtonsoft.Json;

    public sealed class ForwardedRoomKeyContent : EventContent
    {
        public ForwardedRoomKeyContent(
            string algorithm,
            string roomId,
            string senderKey,
            string sessionId,
            string sessionKey,
            string senderClaimedEd25519Key,
            IReadOnlyCollection<string> forwardingCurve25519KeyChain)
        {
            Algorithm = algorithm;
            RoomId = roomId;
            SenderKey = senderKey;
            SessionId = sessionId;
            SessionKey = sessionKey;
            SenderClaimedEd25519Key = senderClaimedEd25519Key;
            ForwardingCurve25519KeyChain = forwardingCurve25519KeyChain;
        }

        [JsonProperty("algorithm")]
        public string Algorithm { get; }

        [JsonProperty("room_id")]
        public string RoomId { get; }

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
