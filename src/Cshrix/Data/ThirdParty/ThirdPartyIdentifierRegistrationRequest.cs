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

    /// <summary>
    /// Contains data on the third party identifier being added to an account.
    /// </summary>
    public readonly struct ThirdPartyIdentifierRegistrationRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThirdPartyIdentifierRegistrationRequest" /> structure.
        /// </summary>
        /// <param name="credentials">The credentials to register.</param>
        /// <param name="bind">Whether the homeserver should also bind this 3PID to the account's Matrix ID.</param>
        [JsonConstructor]
        public ThirdPartyIdentifierRegistrationRequest(ThirdPartyIdentifierCredentials credentials, bool bind = false)
            : this()
        {
            Credentials = credentials;
            Bind = bind;
        }

        /// <summary>
        /// Gets the third party credentials to associate with the account.
        /// </summary>
        [JsonProperty("three_pid_creds")]
        public ThirdPartyIdentifierCredentials Credentials { get; }

        /// <summary>
        /// Gets a value indicating whether the homeserver should also bind this third party identifier to the
        /// account's Matrix ID with the passed identity server.
        /// </summary>
        [JsonProperty("bind")]
        public bool Bind { get; }
    }
}
