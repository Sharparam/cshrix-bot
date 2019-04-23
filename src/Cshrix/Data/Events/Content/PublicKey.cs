// <copyright file="PublicKey.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct PublicKey
    {
        [JsonConstructor]
        public PublicKey([CanBeNull] Uri keyValidityUri, string key)
            : this()
        {
            KeyValidityUri = keyValidityUri;
            Key = key;
        }

        [JsonProperty("key_validity_url")]
        [CanBeNull]
        public Uri KeyValidityUri { get; }

        [JsonProperty("public_key")]
        public string Key { get; }
    }
}
