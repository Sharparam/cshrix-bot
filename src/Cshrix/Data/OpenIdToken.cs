// <copyright file="OpenIdToken.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// An OpenID token.
    /// </summary>
    public readonly struct OpenIdToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenIdToken" /> structure.
        /// </summary>
        /// <param name="accessToken">An OpenID access token.</param>
        /// <param name="expiresIn">The amount of time before the token expires.</param>
        /// <param name="matrixServerName">The domain name of the verifying homeserver.</param>
        /// <param name="tokenType">The type of the token.</param>
        [JsonConstructor]
        public OpenIdToken(string accessToken, TimeSpan expiresIn, string matrixServerName, string tokenType)
            : this()
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
            MatrixServerName = matrixServerName;
            TokenType = tokenType;
        }

        /// <summary>
        /// Gets an access token the consumer may use to verify the identity of the person who generated the token.
        /// This is passed to the federation API <c>GET /openid/userinfo</c>.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; }

        /// <summary>
        /// Gets the amount of time before the token expires.
        /// </summary>
        [JsonProperty("expires_in")]
        [JsonConverter(typeof(SecondTimeSpanConverter))]
        public TimeSpan ExpiresIn { get; }

        /// <summary>
        /// Gets the homeserver domain the consumer should use when attempting to verify the user's identity.
        /// </summary>
        [JsonProperty("matrix_server_name")]
        public string MatrixServerName { get; }

        /// <summary>
        /// Gets the type of the token. This will always be <c>Bearer</c>.
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; }
    }
}
