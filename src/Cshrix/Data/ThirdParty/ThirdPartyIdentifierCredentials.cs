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

    /// <summary>
    /// Contains third party credentials.
    /// </summary>
    public readonly struct ThirdPartyIdentifierCredentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThirdPartyIdentifierCredentials" /> structure.
        /// </summary>
        /// <param name="clientSecret">A client secret used in the session with the identity server.</param>
        /// <param name="identityServer">The identity server to use.</param>
        /// <param name="sessionId">A session identifier given by the identity server.</param>
        [JsonConstructor]
        public ThirdPartyIdentifierCredentials(string clientSecret, string identityServer, string sessionId)
            : this()
        {
            ClientSecret = clientSecret;
            IdentityServer = identityServer;
            SessionId = sessionId;
        }

        /// <summary>
        /// Gets the client secret used in the session with the identity server.
        /// </summary>
        [JsonProperty("client_secret")]
        public string ClientSecret { get; }

        /// <summary>
        /// Gets the identity server to use.
        /// </summary>
        [JsonProperty("id_server")]
        public string IdentityServer { get; }

        /// <summary>
        /// Gets the session identifier given by the identity server.
        /// </summary>
        [JsonProperty("sid")]
        public string SessionId { get; }
    }
}
