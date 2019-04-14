// <copyright file="EventsContainer.cs">
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

    public readonly struct EventsContainer
    {
        public EventsContainer(IEnumerable<Event> events)
            : this(events.ToList().AsReadOnly())
        {
        }

        [JsonConstructor]
        public EventsContainer(IReadOnlyCollection<Event> events)
            : this() =>
            Events = events;

        [JsonProperty("events")]
        public IReadOnlyCollection<Event> Events { get; }
    }
}
