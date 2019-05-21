// <copyright file="ThirdPartyIdentifier.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.ThirdParty
{
    using System;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// Represents a third party identifier associated with a user account.
    /// </summary>
    public readonly struct ThirdPartyIdentifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThirdPartyIdentifier" /> structure.
        /// </summary>
        /// <param name="address">The 3PID address.</param>
        /// <param name="medium">The medium of the 3PID.</param>
        /// <param name="addedAt">The date and time at which the identifier was associated with the user.</param>
        /// <param name="validatedAt">
        /// The date and time at which the identifier was validated by the identity server.
        /// </param>
        [JsonConstructor]
        public ThirdPartyIdentifier(string address, string medium, DateTimeOffset addedAt, DateTimeOffset validatedAt)
            : this()
        {
            Address = address;
            Medium = medium;
            AddedAt = addedAt;
            ValidatedAt = validatedAt;
        }

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

        /// <summary>
        /// Gets the date and time at which this identifier was associated with the user.
        /// </summary>
        [JsonProperty("added_at")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTimeOffset AddedAt { get; }

        /// <summary>
        /// Gets the date and time at which this identifier was validated by the identity server.
        /// </summary>
        [JsonProperty("validated_at")]
        public DateTimeOffset ValidatedAt { get; }
    }
}
