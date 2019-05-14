// <copyright file="TurnServerCredentials.cs">
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
    /// Credentials for a TURN server(s).
    /// </summary>
    public readonly struct TurnServerCredentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TurnServerCredentials" /> structure.
        /// </summary>
        /// <param name="uris">A list of TURN URIs.</param>
        /// <param name="username">The username to use.</param>
        /// <param name="password">The password to use.</param>
        /// <param name="ttl">The time-to-live.</param>
        [JsonConstructor]
        public TurnServerCredentials(Uri[] uris, string username, string password, TimeSpan ttl)
            : this()
        {
            Uris = uris;
            Username = username;
            Password = password;
            Ttl = ttl;
        }

        /// <summary>
        /// Gets an array of URIs for TURN servers.
        /// </summary>
        [JsonProperty("uris")]
        public Uri[] Uris { get; }

        /// <summary>
        /// Gets the username.
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; }

        /// <summary>
        /// Gets the time-to-live (TTL).
        /// </summary>
        [JsonProperty("ttl")]
        [JsonConverter(typeof(SecondTimeSpanConverter))]
        public TimeSpan Ttl { get; }
    }
}
