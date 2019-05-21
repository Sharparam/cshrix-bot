// <copyright file="ThirdPartyIdentifierDeletionRequest.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.ThirdParty
{
    using Newtonsoft.Json;

    /// <summary>
    /// Details about a third party identifier to be removed.
    /// </summary>
    public readonly struct ThirdPartyIdentifierDeletionRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThirdPartyIdentifierDeletionRequest" /> structure.
        /// </summary>
        /// <param name="address">The third party address being removed.</param>
        /// <param name="medium">The medium of the third party identifier being removed.</param>
        [JsonConstructor]
        public ThirdPartyIdentifierDeletionRequest(string address, string medium)
            : this()
        {
            Address = address;
            Medium = medium;
        }

        /// <summary>
        /// Gets the third party address being removed.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; }

        /// <summary>
        /// Gets the medium of the third party identifier being removed.
        /// </summary>
        [JsonProperty("medium")]
        public string Medium { get; }
    }
}
