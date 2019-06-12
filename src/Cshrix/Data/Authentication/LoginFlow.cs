// <copyright file="LoginFlow.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Authentication
{
    using Newtonsoft.Json;

    /// <summary>
    /// Describes a login flow.
    /// </summary>
    public readonly struct LoginFlow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFlow" /> structure.
        /// </summary>
        /// <param name="type">The authentication type.</param>
        [JsonConstructor]
        public LoginFlow(AuthenticationType type)
            : this() =>
            Type = type;

        /// <summary>
        /// Gets the authentication type of this login flow. This is supplied as the <c>type</c> when logging in.
        /// </summary>
        [JsonProperty("type")]
        public AuthenticationType Type { get; }
    }
}
