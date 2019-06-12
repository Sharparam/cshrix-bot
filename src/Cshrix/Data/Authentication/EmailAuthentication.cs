// <copyright file="EmailAuthentication.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Authentication
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    using ThirdParty;

    /// <summary>
    /// Authentication is supported by authorising an email address with an identity server.
    /// </summary>
    /// <remarks>
    /// Prior to submitting this, the client should authenticate with an identity server. After authenticating,
    /// the session information should be submitted to the homeserver.
    /// </remarks>
    public sealed class EmailAuthentication : AuthenticationData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAuthentication" /> class.
        /// </summary>
        /// <param name="session">The value of the session key given by the homeserver.</param>
        /// <param name="credentials">Third party identifier credentials to use for authentication.</param>
        public EmailAuthentication(string session, IEnumerable<ThirdPartyIdentifierCredentials> credentials)
            : this(session, credentials.ToArray())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAuthentication" /> class.
        /// </summary>
        /// <param name="session">The value of the session key given by the homeserver.</param>
        /// <param name="credentials">Third party identifier credentials to use for authentication.</param>
        public EmailAuthentication(string session, IReadOnlyCollection<ThirdPartyIdentifierCredentials> credentials)
            : base(AuthenticationType.Email, session) =>
            Credentials = credentials;

        /// <summary>
        /// Gets a collection of third party identifier credentials to use for authentication.
        /// </summary>
        [JsonProperty("threepidCreds")]
        public IReadOnlyCollection<ThirdPartyIdentifierCredentials> Credentials { get; }
    }
}
