// <copyright file="ThirdPartyIdentifierCredentials.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.ThirdParty
{
    using Newtonsoft.Json;

    public readonly struct ThirdPartyIdentifierCredentials
    {
        [JsonConstructor]
        public ThirdPartyIdentifierCredentials(string clientSecret, string identityServer, string sessionId)
            : this()
        {
            ClientSecret = clientSecret;
            IdentityServer = identityServer;
            SessionId = sessionId;
        }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; }

        [JsonProperty("id_server")]
        public string IdentityServer { get; }

        [JsonProperty("sid")]
        public string SessionId { get; }
    }
}
