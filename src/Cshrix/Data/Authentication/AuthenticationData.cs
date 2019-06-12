// <copyright file="AuthenticationData.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Authentication
{
    /// <summary>
    /// Contains authentication data.
    /// </summary>
    public class AuthenticationData : SessionContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationData" /> class.
        /// </summary>
        /// <param name="type">The type of authentication this is.</param>
        /// <param name="session">The value of the session key given by the homeserver.</param>
        public AuthenticationData(AuthenticationType type, string session)
            : base(session) =>
            Type = type;

        /// <summary>
        /// Gets the type of authentication data contained in this object.
        /// </summary>
        public AuthenticationType Type { get; }
    }
}
