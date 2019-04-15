// <copyright file="EmailAuthentication.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    public sealed class EmailAuthentication : AuthenticationData
    {
        public EmailAuthentication(string session, IEnumerable<ThirdPartyIdentifierCredentials> credentials)
            : this(session, credentials.ToArray())
        {
        }

        public EmailAuthentication(string session, IReadOnlyCollection<ThirdPartyIdentifierCredentials> credentials)
            : base(AuthenticationType.Email, session) =>
            Credentials = credentials;

        [JsonProperty("threepidCreds")]
        public IReadOnlyCollection<ThirdPartyIdentifierCredentials> Credentials { get; }
    }
}
