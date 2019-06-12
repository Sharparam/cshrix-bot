// <copyright file="Timeline.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// Describes the timeline of a room.
    /// </summary>
    public readonly struct Timeline
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Timeline" /> structure.
        /// </summary>
        /// <param name="events">Timeline events.</param>
        /// <param name="limited">Whether the number of events was limited by a filter.</param>
        /// <param name="previousBatchToken">A token to request previous events from the API.</param>
        public Timeline(IEnumerable<Event> events, bool limited, string previousBatchToken)
            : this(events.ToList().AsReadOnly(), limited, previousBatchToken)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Timeline" /> structure.
        /// </summary>
        /// <param name="events">Timeline events.</param>
        /// <param name="limited">Whether the number of events was limited by a filter.</param>
        /// <param name="previousBatchToken">A token to request previous events from the API.</param>
        [JsonConstructor]
        public Timeline(IReadOnlyCollection<Event> events, bool limited, string previousBatchToken)
            : this()
        {
            Events = events;
            Limited = limited;
            PreviousBatchToken = previousBatchToken;
        }

        /// <summary>
        /// Gets the events that are part of the timeline.
        /// </summary>
        [JsonProperty("events")]
        public IReadOnlyCollection<Event> Events { get; }

        /// <summary>
        /// Gets a value indicating whether the number of events was limited by a filter.
        /// </summary>
        [JsonProperty("limited")]
        public bool Limited { get; }

        /// <summary>
        /// Gets a token that can be supplied to the <c>from</c> parameter on the <c>rooms/{roomId}/messages</c>
        /// endpoint.
        /// </summary>
        [JsonProperty("prev_batch")]
        public string PreviousBatchToken { get; }
    }
}
