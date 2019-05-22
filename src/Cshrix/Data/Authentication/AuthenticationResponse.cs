// <copyright file="AuthenticationResponse.cs">
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
    /// A response returned on successful authentication.
    /// </summary>
    public readonly struct AuthenticationResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationResponse" /> structure.
        /// </summary>
        /// <param name="accessToken">An access token.</param>
        /// <param name="deviceId">A device ID.</param>
        /// <param name="userId">A user ID.</param>
        [JsonConstructor]
        public AuthenticationResponse(
            [CanBeNull] string accessToken,
            [CanBeNull] string deviceId,
            UserId userId)
            : this()
        {
            AccessToken = accessToken;
            DeviceId = deviceId;
            UserId = userId;
        }

        /// <summary>
        /// Gets an access token for the account. This access token can then be used to authorize other requests.
        /// </summary>
        [JsonProperty("access_token")]
        [CanBeNull]
        public string AccessToken { get; }

        /// <summary>
        /// Gets the ID of the authenticated device.
        /// </summary>
        [JsonProperty("device_id")]
        [CanBeNull]
        public string DeviceId { get; }

        /// <summary>
        /// Gets the authenticated user's user ID.
        /// </summary>
        [JsonProperty("user_id")]
        public UserId UserId { get; }
    }
}
