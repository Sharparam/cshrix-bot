// <copyright file="ThirdPartyIdentifiersResponse.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.ThirdParty
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains the list of a user's third party identifiers.
    /// </summary>
    public readonly struct ThirdPartyIdentifiersResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThirdPartyIdentifiersResponse" /> structure.
        /// </summary>
        /// <param name="identifiers">A collection of identifiers for the account.</param>
        public ThirdPartyIdentifiersResponse(IEnumerable<ThirdPartyIdentifier> identifiers)
            : this(identifiers.ToList().AsReadOnly())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThirdPartyIdentifiersResponse" /> structure.
        /// </summary>
        /// <param name="identifiers">A collection of identifiers for the account.</param>
        [JsonConstructor]
        public ThirdPartyIdentifiersResponse(IReadOnlyCollection<ThirdPartyIdentifier> identifiers)
            : this() =>
            Identifiers = identifiers;

        /// <summary>
        /// Gets a collection of third party identifiers registered to the account.
        /// </summary>
        [JsonProperty("threepids")]
        public IReadOnlyCollection<ThirdPartyIdentifier> Identifiers { get; }
    }
}
