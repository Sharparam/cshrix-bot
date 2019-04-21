// <copyright file="AuthenticationResponse.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct AuthenticationResponse
    {
        [JsonConstructor]
        public AuthenticationResponse(
            [CanBeNull] string accessToken,
            [CanBeNull] string deviceId,
            [CanBeNull] string homeserver,
            UserId userId)
            : this()
        {
            AccessToken = accessToken;
            DeviceId = deviceId;

            #pragma warning disable 618
            Homeserver = homeserver;
            #pragma warning restore 618

            UserId = userId;
        }

        [JsonProperty("access_token")]
        [CanBeNull]
        public string AccessToken { get; }

        [JsonProperty("device_id")]
        [CanBeNull]
        public string DeviceId { get; }

        [JsonProperty("home_server")]
        [CanBeNull]
        [Obsolete("Extract server name from the UserId instead")]
        public string Homeserver { get; }

        [JsonProperty("user_id")]
        public UserId UserId { get; }
    }
}
