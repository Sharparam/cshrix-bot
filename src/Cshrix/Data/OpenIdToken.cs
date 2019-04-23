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

    public readonly struct OpenIdToken
    {
        [JsonConstructor]
        public OpenIdToken(string accessToken, TimeSpan expiresIn, string matrixServerName, string tokenType)
            : this()
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
            MatrixServerName = matrixServerName;
            TokenType = tokenType;
        }

        [JsonProperty("access_token")]
        public string AccessToken { get; }

        [JsonProperty("expires_in")]
        [JsonConverter(typeof(SecondTimeSpanConverter))]
        public TimeSpan ExpiresIn { get; }

        [JsonProperty("matrix_server_name")]
        public string MatrixServerName { get; }

        [JsonProperty("token_type")]
        public string TokenType { get; }
    }
}
