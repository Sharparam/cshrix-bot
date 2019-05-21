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

    /// <summary>
    /// Contains signed third party data. Used to prove that a user owns a third party identity which has been invited
    /// to a room.
    /// </summary>
    public readonly struct SignedThirdPartyData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignedThirdPartyData" /> structure.
        /// </summary>
        /// <param name="signed">Signed third party data.</param>
        [JsonConstructor]
        public SignedThirdPartyData([CanBeNull] SignedSenderData signed)
            : this() =>
            Signed = signed;

        /// <summary>
        /// Gets signed third party data.
        /// </summary>
        [CanBeNull]
        [JsonProperty("third_party_signed")]
        public SignedSenderData Signed { get; }
    }
}
