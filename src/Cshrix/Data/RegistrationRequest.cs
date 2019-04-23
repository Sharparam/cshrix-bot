// <copyright file="RegistrationRequest.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Newtonsoft.Json;

    public readonly struct RegistrationRequest
    {
        [JsonConstructor]
        public RegistrationRequest(
            string username = null,
            string password = null,
            bool bindEmail = true,
            string deviceId = null,
            bool inhibitLogin = false,
            string initialDeviceDisplayName = null,
            SessionContainer authenticationData = null)
            : this()
        {
            AuthenticationData = authenticationData ?? new SessionContainer(null);
            BindEmail = bindEmail;
            DeviceId = deviceId;
            InhibitLogin = inhibitLogin;
            InitialDeviceDisplayName = initialDeviceDisplayName;
            Password = password;
            Username = username;
        }

        [JsonProperty("auth")]
        public SessionContainer AuthenticationData { get; }

        [JsonProperty("bind_email")]
        public bool BindEmail { get; }

        [JsonProperty(
            "device_id",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string DeviceId { get; }

        [JsonProperty("inhibit_login")]
        public bool InhibitLogin { get; }

        [JsonProperty(
            "initial_device_display_name",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string InitialDeviceDisplayName { get; }

        [JsonProperty(
            "password",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Password { get; }

        [JsonProperty(
            "username",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Username { get; }
    }
}
