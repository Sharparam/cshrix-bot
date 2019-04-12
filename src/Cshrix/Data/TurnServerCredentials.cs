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

    public readonly struct TurnServerCredentials
    {
        [JsonConstructor]
        public TurnServerCredentials(string password, TimeSpan ttl, Uri[] uris, string username)
            : this()
        {
            Password = password;
            Ttl = ttl;
            Uris = uris;
            Username = username;
        }

        [JsonProperty("password")]
        public string Password { get; }

        [JsonProperty("ttl")]
        [JsonConverter(typeof(SecondTimeSpanConverter))]
        public TimeSpan Ttl { get; }

        [JsonProperty("uris")]
        public Uri[] Uris { get; }

        [JsonProperty("username")]
        public string Username { get; }
    }
}
