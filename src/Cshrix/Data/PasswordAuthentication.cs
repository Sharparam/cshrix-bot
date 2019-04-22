// <copyright file="PasswordAuthentication.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Authentication;

    using Newtonsoft.Json;

    public sealed class PasswordAuthentication : AuthenticationData
    {
        public PasswordAuthentication(IUserIdentifier identifier, string password, string session)
            : base(AuthenticationType.Password, session)
        {
            Identifier = identifier;
            Password = password;
        }

        [JsonProperty("identifier")]
        public IUserIdentifier Identifier { get; }

        [JsonProperty("password")]
        public string Password { get; }
    }
}
