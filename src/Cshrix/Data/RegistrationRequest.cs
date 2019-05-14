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

    /// <summary>
    /// Contains information required to request user registration.
    /// </summary>
    public readonly struct RegistrationRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationRequest" /> structure.
        /// </summary>
        /// <param name="username">Localpart to use in generating the Matrix user ID.</param>
        /// <param name="password">Password to set.</param>
        /// <param name="bindEmail">Whether to bind the email used for authentication to the new account.</param>
        /// <param name="deviceId">ID to set on the current device.</param>
        /// <param name="initialDeviceDisplayName">Initial display name to set on the current device.</param>
        /// <param name="inhibitLogin">Whether to prevent automatic login.</param>
        /// <param name="authenticationData">Data used to authenticate the request.</param>
        [JsonConstructor]
        public RegistrationRequest(
            string username = null,
            string password = null,
            bool bindEmail = true,
            string deviceId = null,
            string initialDeviceDisplayName = null,
            bool inhibitLogin = false,
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

        /// <summary>
        /// Gets the data used to authenticate the registration request.
        /// </summary>
        [JsonProperty("auth")]
        public SessionContainer AuthenticationData { get; }

        /// <summary>
        /// Gets the localpart that should be used when creating the account.
        /// </summary>
        [JsonProperty(
            "username",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Username { get; }

        /// <summary>
        /// Gets the password to set on the account.
        /// </summary>
        [JsonProperty(
            "password",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Password { get; }

        /// <summary>
        /// Gets a value indicating whether the server should associate the email used
        /// for authentication with the new user account.
        /// </summary>
        [JsonProperty("bind_email")]
        public bool BindEmail { get; }

        /// <summary>
        /// Gets a device ID to apply to the current device on successful registration.
        /// </summary>
        [JsonProperty(
            "device_id",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string DeviceId { get; }

        /// <summary>
        /// Gets an initial display name to set on the user's device.
        /// </summary>
        [JsonProperty(
            "initial_device_display_name",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string InitialDeviceDisplayName { get; }

        /// <summary>
        /// Gets a value indicating whether access tokens and device IDs should <em>not</em> be returned on
        /// successful registration. This prevents automatic login.
        /// </summary>
        [JsonProperty("inhibit_login")]
        public bool InhibitLogin { get; }
    }
}
