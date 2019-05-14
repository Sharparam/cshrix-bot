// <copyright file="OneTimeKeyCounts.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Cryptography
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains a dictionary of one-time key (OTK) types and associated counters.
    /// </summary>
    public readonly struct OneTimeKeyCounts
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneTimeKeyCounts" /> structure.
        /// </summary>
        /// <param name="counts">A dictionary with one-time key types and their associated counters.</param>
        [JsonConstructor]
        public OneTimeKeyCounts(IReadOnlyDictionary<string, int> counts)
            : this() =>
            Counts = counts;

        /// <summary>
        /// Gets a dictionary mapping one-time key (OTK) types to a counter.
        /// </summary>
        [JsonProperty("one_time_key_counts")]
        public IReadOnlyDictionary<string, int> Counts { get; }
    }
}
