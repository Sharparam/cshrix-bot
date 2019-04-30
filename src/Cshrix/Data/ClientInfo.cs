// <copyright file="ClientInfo.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about what servers a client should use for communicating with a homeserver.
    /// </summary>
    public readonly struct ClientInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientInfo" /> structure.
        /// </summary>
        /// <param name="homeserver">URI to the homeserver.</param>
        /// <param name="identityServer">URI to the identity server.</param>
        [JsonConstructor]
        public ClientInfo(BaseUriContainer homeserver, BaseUriContainer identityServer)
            : this()
        {
            Homeserver = homeserver;
            IdentityServer = identityServer;
        }

        /// <summary>
        /// Gets the URI to the homeserver.
        /// </summary>
        [JsonProperty("m.homeserver")]
        public BaseUriContainer Homeserver { get; }

        /// <summary>
        /// Gets the URI to the identity server.
        /// </summary>
        [JsonProperty("m.identity_server")]
        public BaseUriContainer IdentityServer { get; }
    }
}
