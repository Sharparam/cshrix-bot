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

    /// <summary>
    /// Describes a search result group.
    /// </summary>
    public readonly struct SearchGroupValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchGroupValue" /> structure.
        /// </summary>
        /// <param name="nextBatchToken">A token that can be used to get the next batch of results in the group.</param>
        /// <param name="order">A number that can be used to order different groups.</param>
        /// <param name="results">A collection of event IDs identifying the results in the group.</param>
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

        /// <summary>
        /// Gets a token that can be used to get the next batch of results in the group, by passing it as the
        /// <c>next_batch</c> parameter to the next call.
        /// </summary>
        /// <remarks>
        /// If this property is <c>null</c>, there are no more results in this group.
        /// </remarks>
        [JsonProperty("next_batch")]
        [CanBeNull]
        public string NextBatchToken { get; }

        /// <summary>
        /// Gets a number that can be used to order different groups.
        /// </summary>
        [JsonProperty("order")]
        public int Order { get; }

        /// <summary>
        /// Gets a collection of event IDs identifying which results are in this group.
        /// </summary>
        [JsonProperty("results")]
        public IReadOnlyCollection<string> Results { get; }
    }
}
