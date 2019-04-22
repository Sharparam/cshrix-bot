// <copyright file="SearchResultRoomEvents.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Search
{
    using System.Collections.Generic;

    using Events;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct SearchResultRoomEvents
    {
        [JsonConstructor]
        public SearchResultRoomEvents(
            int count,
            IReadOnlyCollection<string> highlights,
            IReadOnlyCollection<SearchEventResult> results,
            [CanBeNull] IReadOnlyDictionary<string, IReadOnlyCollection<StateEvent>> stateEvents,
            IReadOnlyDictionary<string, IReadOnlyDictionary<string, SearchGroupValue>> groups,
            [CanBeNull] string nextBatchToken)
            : this()
        {
            Count = count;
            Highlights = highlights;
            Results = results;
            StateEvents = stateEvents;
            Groups = groups;
            NextBatchToken = nextBatchToken;
        }

        [JsonProperty("count")]
        public int Count { get; }

        [JsonProperty("highlights")]
        public IReadOnlyCollection<string> Highlights { get; }

        [JsonProperty("results")]
        public IReadOnlyCollection<SearchEventResult> Results { get; }

        [JsonProperty("state")]
        [CanBeNull]
        public IReadOnlyDictionary<string, IReadOnlyCollection<StateEvent>> StateEvents { get; }

        [JsonProperty("groups")]
        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, SearchGroupValue>> Groups { get; }

        [JsonProperty("next_batch")]
        [CanBeNull]
        public string NextBatchToken { get; }
    }
}
