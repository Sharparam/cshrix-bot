// <copyright file="LoginRequest.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Authentication
{
    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Describes a request to login to a homeserver.
    /// </summary>
    public readonly struct LoginRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginRequest" /> structure.
        /// </summary>
        /// <param name="type">The type of authentication to perform.</param>
        /// <param name="identifier">An object identifying the user.</param>
        /// <param name="password">A password to use.</param>
        /// <param name="token">A token to use.</param>
        /// <param name="deviceId">The ID of the client device.</param>
        /// <param name="initialDeviceDisplayName">
        /// Display name to set on the device, if a new device is created.
        /// </param>
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

        /// <summary>
        /// Gets the type of authentication to perform.
        /// </summary>
        [JsonProperty("type")]
        public AuthenticationType Type { get; }

        /// <summary>
        /// Gets an object identifying the user that is logging in.
        /// </summary>
        [JsonProperty("identifier")]
        public IUserIdentifier Identifier { get; }

        /// <summary>
        /// Gets a password to use for logging in.
        /// </summary>
        [JsonProperty(
            "password",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Password { get; }

        /// <summary>
        /// Gets a token to use for logging in.
        /// </summary>
        [JsonProperty(
            "token",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Token { get; }

        /// <summary>
        /// Gets the ID of the client device.
        /// </summary>
        /// <remarks>
        /// If this does not correspond to a known client device, a new device will be created. The server will
        /// auto-generate a device ID if this is not specified.
        /// </remarks>
        [JsonProperty(
            "device_id",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string DeviceId { get; }

        /// <summary>
        /// Gets a display name that should be set on the authenticated device. Ignored if <see cref="DeviceId" />
        /// corresponds to a known device.
        /// </summary>
        [JsonProperty("initial_device_display_name")]
        [CanBeNull]
        public string InitialDeviceDisplayName { get; }
    }
}
