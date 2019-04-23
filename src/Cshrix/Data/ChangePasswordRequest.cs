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

    public readonly struct ChangePasswordRequest
    {
        [JsonConstructor]
        public ChangePasswordRequest(string newPassword, SessionContainer authenticationData = null)
            : this()
        {
            AuthenticationData = authenticationData;
            NewPassword = newPassword;
        }

        [JsonProperty(
            "auth",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SessionContainer AuthenticationData { get; }

        [JsonProperty("new_password")]
        public string NewPassword { get; }
    }
}
