// <copyright file="EventContext.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;

    using Events;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains details about an event and its context.
    /// </summary>
    public readonly struct EventContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventContext" /> structure.
        /// </summary>
        /// <param name="start">A pagination token for the start of the chunk.</param>
        /// <param name="end">A pagination token for the end of the chunk.</param>
        /// <param name="event">The requested event.</param>
        /// <param name="before">Events before the requested event.</param>
        /// <param name="after">Events after the requested event.</param>
        /// <param name="state">State events for the room at the last event.</param>
        [JsonConstructor]
        public EventContext(
            string start,
            string end,
            RoomEvent @event,
            IReadOnlyCollection<RoomEvent> before,
            IReadOnlyCollection<RoomEvent> after,
            IReadOnlyCollection<StateEvent> state)
            : this()
        {
            Start = start;
            End = end;
            Event = @event;
            Before = before;
            After = after;
            State = state;
        }

        /// <summary>
        /// Gets a pagination token for the start of this chunk.
        /// </summary>
        [JsonProperty("start")]
        public string Start { get; }

        /// <summary>
        /// Gets a pagination token for the end of this chunk.
        /// </summary>
        [JsonProperty("end")]
        public string End { get; }

        /// <summary>
        /// Gets a <see cref="RoomEvent" /> for the requested event.
        /// </summary>
        [JsonProperty("event")]
        public RoomEvent Event { get; }

        /// <summary>
        /// Gets a collection of events that happened just before the requested event,
        /// in reverse-chronological order.
        /// </summary>
        [JsonProperty("events_before")]
        public IReadOnlyCollection<RoomEvent> Before { get; }

        /// <summary>
        /// Gets a collection of events that happened just after the requested event,
        /// in chronological order.
        /// </summary>
        [JsonProperty("events_after")]
        public IReadOnlyCollection<RoomEvent> After { get; }

        /// <summary>
        /// Gets a collection of state events in the room at the last event returned.
        /// </summary>
        [JsonProperty("state")]
        public IReadOnlyCollection<StateEvent> State { get; }
    }
}
