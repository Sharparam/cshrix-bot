// <copyright file="JsonWebKey.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using Newtonsoft.Json;

    public readonly struct JsonWebKey
    {
        [JsonConstructor]
        public JsonWebKey(string keyType, string[] keyOperations, string algorithm, string key, bool extractable)
            : this()
        {
            KeyType = keyType;
            KeyOperations = keyOperations;
            Algorithm = algorithm;
            Key = key;
            Extractable = extractable;
        }

        [JsonProperty("kty")]
        public string KeyType { get; }

        [JsonProperty("key_opts")]
        public string[] KeyOperations { get; }

        [JsonProperty("alg")]
        public string Algorithm { get; }

        [JsonProperty("k")]
        public string Key { get; }

        [JsonProperty("ext")]
        public bool Extractable { get; }
    }
}
