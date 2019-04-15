// <copyright file="ThirdPartyInviteContent.cs">
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

    public class ThirdPartyInviteContent : EventContent
    {
        public ThirdPartyInviteContent(
            string displayName,
            Uri keyValidityUri,
            string publicKey,
            IReadOnlyCollection<PublicKey> publicKeys)
        {
            DisplayName = displayName;
            KeyValidityUri = keyValidityUri;
            PublicKey = publicKey;
            PublicKeys = publicKeys;
        }

        [JsonProperty("display_name")]
        public string DisplayName { get; }

        [JsonProperty("key_validity_url")]
        public Uri KeyValidityUri { get; }

        [JsonProperty("public_key")]
        public string PublicKey { get; }

        [JsonProperty("public_keys")]
        public IReadOnlyCollection<PublicKey> PublicKeys { get; }
    }
}
