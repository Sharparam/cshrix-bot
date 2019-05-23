// <copyright file="InviteState.cs">
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
    /// Contains state data for a room to which the user has been invited.
    /// </summary>
    public readonly struct InviteState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InviteState" /> structure.
        /// </summary>
        /// <param name="events">Stripped-down state data.</param>
        public InviteState(IEnumerable<Event> events)
            : this(events.ToList().AsReadOnly())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InviteState" /> structure.
        /// </summary>
        /// <param name="events">Stripped-down state data.</param>
        [JsonConstructor]
        public InviteState(IReadOnlyCollection<Event> events)
            : this() =>
            Events = events;

        /// <summary>
        /// Gets a collection of stripped-down state events for the room.
        /// </summary>
        [JsonProperty("events")]
        public IReadOnlyCollection<Event> Events { get; }
    }
}
