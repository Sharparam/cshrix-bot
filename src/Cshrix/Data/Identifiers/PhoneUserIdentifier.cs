// <copyright file="PhoneUserIdentifier.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Newtonsoft.Json;

    using ThirdParty;

    /// <summary>
    /// Identifies a user by their phone number.
    /// </summary>
    /// <remarks>
    /// A client can identify a user using a phone number associated with the user's account, where the phone number
    /// was previously associated using the <c>/account/3pid</c> API. The phone number can be passed in as entered by
    /// the user, the homeserver will be responsible for canonicalizing it. If the client wishes to canonicalize the
    /// phone number, then it can use the <see cref="ThirdPartyIdentifier" /> identifier instead with
    /// <see cref="ThirdPartyIdentifier.Medium" /> set to <c>msisdn</c>.
    /// </remarks>
    public readonly struct PhoneUserIdentifier : IUserIdentifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneUserIdentifier" /> structure.
        /// </summary>
        /// <param name="country">The country that the number is from.</param>
        /// <param name="number">The phone number.</param>
        [JsonConstructor]
        public PhoneUserIdentifier(string country, string number)
            : this()
        {
            Country = country;
            Number = number;
        }

        /// <summary>
        /// Gets the type of this identifier.
        /// </summary>
        /// <value>Returns <see cref="UserIdentifierType.Phone" />.</value>
        public UserIdentifierType Type => UserIdentifierType.Phone;

        /// <summary>
        /// Gets the country that the phone number is from.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; }

        /// <summary>
        /// Gets the phone number.
        /// </summary>
        [JsonProperty("phone")]
        public string Number { get; }
    }
}
