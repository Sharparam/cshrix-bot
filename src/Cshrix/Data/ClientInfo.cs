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

    public readonly struct ClientInfo
    {
        [JsonConstructor]
        public ClientInfo(BaseUriContainer homeserver, BaseUriContainer identityServer)
            : this()
        {
            Homeserver = homeserver;
            IdentityServer = identityServer;
        }

        [JsonProperty("m.homeserver")]
        public BaseUriContainer Homeserver { get; }

        [JsonProperty("m.identity_server")]
        public BaseUriContainer IdentityServer { get; }
    }
}
