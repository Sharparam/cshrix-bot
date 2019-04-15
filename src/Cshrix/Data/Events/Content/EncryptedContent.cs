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
        public EncryptedContent([NotNull] IDictionary<string, object> dictionary)
            : base(dictionary)
        {
            Algorithm = GetValueOrDefault<Algorithm>("algorithm");
            SenderKey = GetValueOrDefault<string>("sender_key");
            DeviceId = GetValueOrDefault<string>("device_id");
            SessionId = GetValueOrDefault<string>("session_id");

            var ciphertext = dictionary["ciphertext"];

            if (ciphertext is JObject jObject)
            {
                CiphertextInfos = jObject.ToObject<IReadOnlyDictionary<string, CiphertextInfo>>();
            }
            else
            {
                Ciphertext = (string)ciphertext;
            }
        }

        [JsonProperty("algorithm")]
        public Algorithm Algorithm { get; }

        [JsonProperty("ciphertext")]
        [CanBeNull]
        public string Ciphertext { get; }

        [JsonProperty("ciphertext")]
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
