// <copyright file="SearchEventContext.cs">
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

    using Newtonsoft.Json;

    public readonly struct SearchEventContext
    {
        [JsonConstructor]
        public SearchEventContext(
            string start,
            string end,
            IReadOnlyDictionary<UserId, Profile> profiles,
            IReadOnlyCollection<Event> eventsBefore,
            IReadOnlyCollection<Event> eventsAfter)
            : this()
        {
            Start = start;
            End = end;
            Profiles = profiles;
            EventsBefore = eventsBefore;
            EventsAfter = eventsAfter;
        }

        [JsonProperty("start")]
        public string Start { get; }

        [JsonProperty("end")]
        public string End { get; }

        [JsonProperty("profile_info")]
        public IReadOnlyDictionary<UserId, Profile> Profiles { get; }

        [JsonProperty("events_before")]
        public IReadOnlyCollection<Event> EventsBefore { get; }

        [JsonProperty("events_after")]
        public IReadOnlyCollection<Event> EventsAfter { get; }
    }
}
