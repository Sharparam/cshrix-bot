// <copyright file="ThirdPartyUserIdentifier.cs">
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
    /// Identifies a user by one of their third party identifier.
    /// </summary>
    public readonly struct ThirdPartyUserIdentifier : IUserIdentifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThirdPartyUserIdentifier" /> structure.
        /// </summary>
        /// <param name="address">The address of the identifier.</param>
        /// <param name="medium">The medium of the identifier.</param>
        [JsonConstructor]
        public ThirdPartyUserIdentifier(string address, string medium)
            : this()
        {
            Medium = medium;
            Address = address;
        }

        /// <summary>
        /// Gets the type of this identifier.
        /// </summary>
        /// <remarks>Returns <see cref="UserIdentifierType.ThirdParty" />.</remarks>
        public UserIdentifierType Type => UserIdentifierType.ThirdParty;

        /// <summary>
        /// Gets the third party identifier address.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; }

        /// <summary>
        /// Gets the medium of the third party identifier.
        /// </summary>
        [JsonProperty("medium")]
        public string Medium { get; }
    }
}
