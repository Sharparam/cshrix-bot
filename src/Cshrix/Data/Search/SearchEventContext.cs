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

    /// <summary>
    /// Describes the context around an event returned in a search result.
    /// </summary>
    public readonly struct SearchEventContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchEventContext" /> structure.
        /// </summary>
        /// <param name="start">Pagination token for the start of the chunk.</param>
        /// <param name="end">Pagination token for the end of the chunk.</param>
        /// <param name="profiles">Mapping between user IDs and their profiles.</param>
        /// <param name="eventsBefore">Events that happened before the result.</param>
        /// <param name="eventsAfter">Events that happened after the result.</param>
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

        /// <summary>
        /// Gets a pagination token for the start of the chunk.
        /// </summary>
        [JsonProperty("start")]
        public string Start { get; }

        /// <summary>
        /// Gets a pagination token for the end of the chunk.
        /// </summary>
        [JsonProperty("end")]
        public string End { get; }

        /// <summary>
        /// Gets a mapping between user IDs and their profile info.
        /// </summary>
        [JsonProperty("profile_info")]
        public IReadOnlyDictionary<UserId, Profile> Profiles { get; }

        /// <summary>
        /// Gets a collection of events that happened just before the result.
        /// </summary>
        [JsonProperty("events_before")]
        public IReadOnlyCollection<Event> EventsBefore { get; }

        /// <summary>
        /// Gets a collection of events that happened just after the result.
        /// </summary>
        [JsonProperty("events_after")]
        public IReadOnlyCollection<Event> EventsAfter { get; }
    }
}
