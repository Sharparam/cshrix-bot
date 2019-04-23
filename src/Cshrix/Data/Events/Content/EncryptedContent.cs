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

    public class EncryptedContent : EventContent
    {
        public EncryptedContent(
            Algorithm algorithm,
            JToken rawCiphertext,
            string senderKey,
            string deviceId,
            string sessionId)
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

        [JsonProperty("algorithm")]
        public Algorithm Algorithm { get; }

        [JsonProperty("ciphertext")]
        public JToken RawCiphertext { get; }

        [JsonIgnore]
        [CanBeNull]
        public string Ciphertext { get; }

        [JsonIgnore]
        [CanBeNull]
        public IReadOnlyDictionary<string, CiphertextInfo> CiphertextInfos { get; }

        [JsonProperty("sender_key")]
        public string SenderKey { get; }

        [JsonProperty("device_id")]
        public string DeviceId { get; }

        [JsonProperty("session_id")]
        public string SessionId { get; }
    }
}
