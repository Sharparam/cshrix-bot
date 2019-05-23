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

    public readonly struct Timeline
    {
        public Timeline(IEnumerable<Event> events, bool limited, string previousBatchToken)
            : this(events.ToList().AsReadOnly(), limited, previousBatchToken)
        {
        }

        public Timeline(IReadOnlyCollection<Event> events, bool limited, string previousBatchToken)
            : this()
        {
            Events = events;
            Limited = limited;
            PreviousBatchToken = previousBatchToken;
        }

        [JsonProperty("events")]
        public IReadOnlyCollection<Event> Events { get; }

        [JsonProperty("limited")]
        public bool Limited { get; }

        [JsonProperty("prev_batch")]
        public string PreviousBatchToken { get; }
    }
}
