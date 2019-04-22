// <copyright file="ThirdPartyIdentifierRegistrationRequest.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.ThirdParty
{
    using Newtonsoft.Json;

    public readonly struct ThirdPartyIdentifierRegistrationRequest
    {
        [JsonConstructor]
        public ThirdPartyIdentifierRegistrationRequest(ThirdPartyIdentifierCredentials credentials, bool bind = false)
            : this()
        {
            Credentials = credentials;
            Bind = bind;
        }

        [JsonProperty("three_pid_creds")]
        public ThirdPartyIdentifierCredentials Credentials { get; }

        [JsonProperty("bind")]
        public bool Bind { get; }
    }
}
