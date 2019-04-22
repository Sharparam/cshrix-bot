// <copyright file="SearchGroupValue.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Search
{
    using System.Collections.Generic;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct SearchGroupValue
    {
        [JsonConstructor]
        public SearchGroupValue(
            [CanBeNull] string nextBatchToken,
            int order,
            IReadOnlyCollection<string> results)
            : this()
        {
            NextBatchToken = nextBatchToken;
            Order = order;
            Results = results;
        }

        [JsonProperty("next_batch")]
        [CanBeNull]
        public string NextBatchToken { get; }

        [JsonProperty("order")]
        public int Order { get; }

        [JsonProperty("results")]
        public IReadOnlyCollection<string> Results { get; }
    }
}
