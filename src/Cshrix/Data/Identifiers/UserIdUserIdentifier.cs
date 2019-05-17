// <copyright file="UserIdUserIdentifier.cs">
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
    /// Identifies a user by their full Matrix user ID.
    /// </summary>
    public readonly struct UserIdUserIdentifier : IUserIdentifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserIdUserIdentifier" /> structure.
        /// </summary>
        /// <param name="userId">The user ID of the user.</param>
        [JsonConstructor]
        public UserIdUserIdentifier(UserId userId)
            : this() =>
            UserId = userId;

        /// <summary>
        /// Gets the type of this identifier.
        /// </summary>
        /// <value>Returns <see cref="UserIdentifierType.User" />.</value>
        public UserIdentifierType Type => UserIdentifierType.User;

        /// <summary>
        /// Gets the Matrix user ID of the user.
        /// </summary>
        [JsonProperty("user")]
        public UserId UserId { get; }
    }
}
