// <copyright file="SearchResult.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Search
{
    using Events;

    using Newtonsoft.Json;

    public readonly struct SearchEventResult
    {
        [JsonConstructor]
        public SearchEventResult(int rank, Event result, SearchEventContext? context)
            : this()
        {
            Rank = rank;
            Result = result;
            Context = context;
        }

        [JsonProperty("rank")]
        public int Rank { get; }

        [JsonProperty("result")]
        public Event Result { get; }

        [JsonProperty("context")]
        public SearchEventContext? Context { get; }
    }
}
