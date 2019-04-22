// <copyright file="ThirdPartyUserIdentifier.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.ThirdParty
{
    using Newtonsoft.Json;

    public readonly struct ThirdPartyUserIdentifier : IUserIdentifier
    {
        [JsonConstructor]
        public ThirdPartyUserIdentifier(string medium, string address)
            : this()
        {
            Medium = medium;
            Address = address;
        }

        public UserIdentifierType Type => UserIdentifierType.ThirdParty;

        [JsonProperty("medium")]
        public string Medium { get; }

        [JsonProperty("address")]
        public string Address { get; }
    }
}
