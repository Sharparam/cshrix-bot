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

    /// <summary>
    /// Contains a collection of events.
    /// </summary>
    public readonly struct EventsContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventsContainer" /> structure.
        /// </summary>
        /// <param name="events">A collection of events.</param>
        public EventsContainer(IEnumerable<Event> events)
            : this(events.ToList().AsReadOnly())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsContainer" /> structure.
        /// </summary>
        /// <param name="events">A collection of events.</param>
        [JsonConstructor]
        public EventsContainer(IReadOnlyCollection<Event> events)
            : this() =>
            Events = events;

        /// <summary>
        /// Gets a collection of events.
        /// </summary>
        [JsonProperty("events")]
        public IReadOnlyCollection<Event> Events { get; }
    }
}
