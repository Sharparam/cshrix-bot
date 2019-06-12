// <copyright file="DummyAuthentication.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Authentication
{
    /// <summary>
    /// Dummy authentication data.
    /// </summary>
    public sealed class DummyAuthentication : AuthenticationData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DummyAuthentication" /> class.
        /// </summary>
        /// <param name="session">Session identifier.</param>
        public DummyAuthentication(string session)
            : base(AuthenticationType.Dummy, session)
        {
        }
    }
}
