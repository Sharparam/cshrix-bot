// <copyright file="PasswordAuthentication.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Authentication
{
    using Newtonsoft.Json;

    /// <summary>
    /// An authentication method where the client submits an identifier and secret password, both sent in plain-text.
    /// </summary>
    public sealed class PasswordAuthentication : AuthenticationData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordAuthentication" /> class.
        /// </summary>
        /// <param name="identifier">A user identifier.</param>
        /// <param name="password">The user password.</param>
        /// <param name="session">The value of the session key given by the homeserver.</param>
        public PasswordAuthentication(IUserIdentifier identifier, string password, string session)
            : base(AuthenticationType.Password, session)
        {
            Identifier = identifier;
            Password = password;
        }

        /// <summary>
        /// Gets a user identifier.
        /// </summary>
        [JsonProperty("identifier")]
        public IUserIdentifier Identifier { get; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; }
    }
}
