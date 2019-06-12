// <copyright file="State.cs">
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
    /// Contains state (events) for a room.
    /// </summary>
    public readonly struct State
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="State" /> structure.
        /// </summary>
        /// <param name="events">State events.</param>
        public State(IEnumerable<Event> events)
            : this(events.ToList().AsReadOnly())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="State" /> structure.
        /// </summary>
        /// <param name="events">State events.</param>
        [JsonConstructor]
        public State(IReadOnlyCollection<Event> events)
            : this() =>
            Events = events;

        /// <summary>
        /// Gets a collection of state events.
        /// </summary>
        public IReadOnlyCollection<Event> Events { get; }
    }
}
