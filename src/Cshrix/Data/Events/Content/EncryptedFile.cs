// <copyright file="EncryptedFile.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public readonly struct EncryptedFile
    {
        [JsonConstructor]
        public EncryptedFile(
            Uri uri,
            JsonWebKey key,
            string iv,
            IReadOnlyDictionary<string, string> hashes,
            string version)
            : this()
        {
            Uri = uri;
            Key = key;
            IV = iv;
            Hashes = hashes;
            Version = version;
        }

        [JsonProperty("url")]
        public Uri Uri { get; }

        [JsonProperty("key")]
        public JsonWebKey Key { get; }

        [JsonProperty("iv")]
        public string IV { get; }

        [JsonProperty("hashes")]
        public IReadOnlyDictionary<string, string> Hashes { get; }

        [JsonProperty("v")]
        public string Version { get; }
    }
}
