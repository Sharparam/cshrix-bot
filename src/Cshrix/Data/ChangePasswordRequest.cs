// <copyright file="ChangePasswordRequest.cs">
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
    /// Describes a password change request.
    /// </summary>
    public readonly struct ChangePasswordRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordRequest" /> structure.
        /// </summary>
        /// <param name="newPassword">The new password to set.</param>
        /// <param name="authenticationData">Data authenticating the user.</param>
        [JsonConstructor]
        public ChangePasswordRequest(string newPassword, SessionContainer authenticationData = null)
            : this()
        {
            NewPassword = newPassword;
            AuthenticationData = authenticationData;
        }

        /// <summary>
        /// Gets the new password to set.
        /// </summary>
        [JsonProperty("new_password")]
        public string NewPassword { get; }

        /// <summary>
        /// Gets an object containing data authenticating the user making the request.
        /// </summary>
        [JsonProperty(
            "auth",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SessionContainer AuthenticationData { get; }
    }
}
