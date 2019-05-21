// <copyright file="AuthenticationContainer.cs">
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
    /// Contains authentication data for the
    /// <a href="https://matrix.org/docs/spec/client_server/r0.4.0.html#user-interactive-authentication-api">
    /// User-Interactive Authentication API
    /// </a>.
    /// </summary>
    public class AuthenticationContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationContainer" /> class.
        /// </summary>
        /// <param name="authentication">Additional authentication information.</param>
        public AuthenticationContainer(SessionContainer authentication = null) => Authentication = authentication;

        /// <summary>
        /// Gets additional authentication information for the user-interactive authentication API.
        /// </summary>
        [JsonProperty(
            "auth",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SessionContainer Authentication { get; }
    }
}
