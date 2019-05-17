// <copyright file="LocalpartUserIdentifier.cs">
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
    /// Identifies a user by their localpart.
    /// </summary>
    public readonly struct LocalpartUserIdentifier : IUserIdentifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalpartUserIdentifier" /> structure.
        /// </summary>
        /// <param name="localpart">The local part of the user's Matrix user ID.</param>
        [JsonConstructor]
        public LocalpartUserIdentifier(string localpart)
            : this() =>
            Localpart = localpart;

        /// <summary>
        /// Gets the type of this identifier.
        /// </summary>
        /// <value>This will return <see cref="UserIdentifierType.User" />.</value>
        public UserIdentifierType Type => UserIdentifierType.User;

        /// <summary>
        /// Gets the localpart of the user's Matrix user ID.
        /// </summary>
        [JsonProperty("user")]
        public string Localpart { get; }
    }
}
