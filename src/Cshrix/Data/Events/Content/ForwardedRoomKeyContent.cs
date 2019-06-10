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

    /// <summary>
    /// Describes a key-forwarding event.
    /// </summary>
    public sealed class ForwardedRoomKeyContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForwardedRoomKeyContent" /> class.
        /// </summary>
        /// <param name="algorithm">The algorithm the key in this event is to be used with.</param>
        /// <param name="roomId">The ID of the room where the key is used.</param>
        /// <param name="senderKey">The Curve25519 key of the device which originally initiated the session.</param>
        /// <param name="sessionId">The ID of the session that the key is for.</param>
        /// <param name="sessionKey">The key to be exchanged.</param>
        /// <param name="senderClaimedEd25519Key">
        /// The ED25519 key of the device which originally initiated the session.
        /// </param>
        /// <param name="forwardingCurve25519KeyChain">The chain of Curve25519 keys.</param>
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

        /// <summary>
        /// Gets the encryption algorithm the key in this event is to be used with.
        /// </summary>
        [JsonProperty("algorithm")]
        public string Algorithm { get; }

        /// <summary>
        /// Gets the ID of the room where the key is used.
        /// </summary>
        [JsonProperty("room_id")]
        public string RoomId { get; }

        /// <summary>
        /// Gets the Curve25519 key of the device which originally initiated the session.
        /// </summary>
        [JsonProperty("sender_key")]
        public string SenderKey { get; }

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

        /// <summary>
        /// Gets the ED25519 key of the device which originally initiated the session. It is 'claimed' because
        /// the receiving device has no way to tell that the original <c>room_key</c> actually came from a device
        /// which owns the private part of this key unless they have done device verification.
        /// </summary>
        [JsonProperty("sender_claimed_ed25519_key")]
        public string SenderClaimedEd25519Key { get; }

        /// <summary>
        /// Gets the chain of Curve25519 keys. It starts out empty, but each time the key is forwarded to another
        /// device, the previous sender in the chain is added to the end of the collection. For example, if the key
        /// is forwarded from <c>A</c> to <c>B</c> to <c>C</c>, this field is empty between <c>A</c> and B,
        /// and contains <c>A</c>'s Curve25519 key between <c>B</c> and <c>C</c>.
        /// </summary>
        [JsonProperty("forwarding_curve25519_key_chain")]
        public IReadOnlyCollection<string> ForwardingCurve25519KeyChain { get; }
    }
}
