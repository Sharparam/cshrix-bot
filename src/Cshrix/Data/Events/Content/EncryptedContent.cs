// <copyright file="EncryptedContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System.Collections.Generic;

    using Cryptography;

    using JetBrains.Annotations;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Contains the data of an encrypted event (<c>m.room.encrypted</c>).
    /// </summary>
    public sealed class EncryptedContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EncryptedContent" /> class.
        /// </summary>
        /// <param name="algorithm">The algorithm used to encrypt the data.</param>
        /// <param name="rawCiphertext">Raw ciphertext object as returned from the API.</param>
        /// <param name="senderKey">The Curve25519 key of the sender.</param>
        /// <param name="deviceId">The ID of the sending device.</param>
        /// <param name="sessionId">The ID of the session used to encrypt the message.</param>
        public EncryptedContent(
            Algorithm algorithm,
            JToken rawCiphertext,
            string senderKey,
            [CanBeNull] string deviceId,
            [CanBeNull] string sessionId)
        {
            Algorithm = algorithm;
            RawCiphertext = rawCiphertext;
            SenderKey = senderKey;
            DeviceId = deviceId;
            SessionId = sessionId;

            if (RawCiphertext is JObject jObject)
            {
                CiphertextInfos = jObject.ToObject<IReadOnlyDictionary<string, CiphertextInfo>>();
            }
            else if (RawCiphertext is JValue jValue && jValue.Type == JTokenType.String)
            {
                Ciphertext = (string)jValue;
            }
        }

        /// <summary>
        /// Gets the encryption algorithm used to encrypt the data.
        /// </summary>
        /// <remarks>
        /// The value of this property determines which other properties will have values.
        /// </remarks>
        [JsonProperty("algorithm")]
        public Algorithm Algorithm { get; }

        /// <summary>
        /// Gets the raw ciphertext for the encrypted content of the event data.
        /// </summary>
        /// <remarks>
        /// This is either an encrypted payload (a string), if <see cref="Algorithm" /> is
        /// <see cref="Cshrix.Cryptography.Algorithm.Megolm" />, or a dictionary mapping recipient Curve25519
        /// identity keys to ciphertext information, if <see cref="Algorithm" /> is
        /// <see cref="Cshrix.Cryptography.Algorithm.Olm" />.
        /// For more details, see
        /// <a href="https://matrix.org/docs/spec/client_server/r0.4.0.html#messaging-algorithms">
        /// Messaging Algorithms
        /// </a>.
        /// </remarks>
        [JsonProperty("ciphertext")]
        public JToken RawCiphertext { get; }

        /// <summary>
        /// Gets the ciphertext of the payload, if <see cref="Algorithm" /> is
        /// <see cref="Cshrix.Cryptography.Algorithm.Megolm" />.
        /// </summary>
        [JsonIgnore]
        [CanBeNull]
        public string Ciphertext { get; }

        /// <summary>
        /// Gets a dictionary mapping recipient Curve25519 identity keys to ciphertext information, if
        /// <see cref="Algorithm" /> is <see cref="Cshrix.Cryptography.Algorithm.Olm" />.
        /// </summary>
        [JsonIgnore]
        [CanBeNull]
        public IReadOnlyDictionary<string, CiphertextInfo> CiphertextInfos { get; }

        /// <summary>
        /// Gets the Curve25519 key of the sender.
        /// </summary>
        [JsonProperty("sender_key")]
        public string SenderKey { get; }

        /// <summary>
        /// Gets the ID of the sending device.
        /// </summary>
        /// <remarks>
        /// Required when <see cref="Algorithm" /> is <see cref="Cshrix.Cryptography.Algorithm.Megolm" />.
        /// </remarks>
        [JsonProperty("device_id")]
        [CanBeNull]
        public string DeviceId { get; }

        /// <summary>
        /// Gets the ID of the session used to encrypt the message.
        /// </summary>
        /// <remarks>
        /// Required when <see cref="Algorithm" /> is <see cref="Cshrix.Cryptography.Algorithm.Megolm" />.
        /// </remarks>
        [JsonProperty("session_id")]
        [CanBeNull]
        public string SessionId { get; }
    }
}
