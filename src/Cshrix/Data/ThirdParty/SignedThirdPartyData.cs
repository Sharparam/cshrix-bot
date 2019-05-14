// <copyright file="SignedThirdPartyData.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.ThirdParty
{
    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct SignedThirdPartyData
    {
        [JsonConstructor]
        public SignedThirdPartyData([CanBeNull] SignedSenderData signed)
            : this() =>
            Signed = signed;

        [CanBeNull]
        [JsonProperty("third_party_signed")]
        public SignedSenderData Signed { get; }
    }
}
