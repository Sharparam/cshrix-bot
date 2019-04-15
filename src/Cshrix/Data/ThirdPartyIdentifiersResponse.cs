// <copyright file="ThirdPartyIdentifiersResponse.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    public readonly struct ThirdPartyIdentifiersResponse
    {
        public ThirdPartyIdentifiersResponse(IEnumerable<ThirdPartyIdentifier> identifiers)
            : this(identifiers.ToList().AsReadOnly())
        {
        }

        [JsonConstructor]
        public ThirdPartyIdentifiersResponse(IReadOnlyCollection<ThirdPartyIdentifier> identifiers)
            : this() =>
            Identifiers = identifiers;

        [JsonProperty("threepids")]
        public IReadOnlyCollection<ThirdPartyIdentifier> Identifiers { get; }
    }
}
