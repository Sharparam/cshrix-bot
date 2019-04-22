// <copyright file="LoginRequest.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Authentication;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct LoginRequest
    {
        [JsonConstructor]
        public LoginRequest(
            AuthenticationType type,
            IUserIdentifier identifier,
            [CanBeNull] string password = null,
            [CanBeNull] string token = null,
            [CanBeNull] string deviceId = null,
            [CanBeNull] string initialDeviceDisplayName = null)
            : this()
        {
            Type = type;
            Identifier = identifier;
            Password = password;
            Token = token;
            DeviceId = deviceId;
            InitialDeviceDisplayName = initialDeviceDisplayName;
        }

        [JsonProperty("type")]
        public AuthenticationType Type { get; }

        [JsonProperty("identifier")]
        public IUserIdentifier Identifier { get; }

        [JsonProperty(
            "password",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Password { get; }

        [JsonProperty(
            "token",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Token { get; }

        [JsonProperty(
            "device_id",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string DeviceId { get; }

        [JsonProperty("initial_device_display_name")]
        [CanBeNull]
        public string InitialDeviceDisplayName { get; }
    }
}
