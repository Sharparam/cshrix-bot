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

    public readonly struct EventContext
    {
        [JsonConstructor]
        public EventContext(
            string end,
            RoomEvent @event,
            IReadOnlyCollection<RoomEvent> after,
            IReadOnlyCollection<RoomEvent> before,
            string start,
            IReadOnlyCollection<StateEvent> state)
            : this()
        {
            End = end;
            Event = @event;
            After = after;
            Before = before;
            Start = start;
            State = state;
        }

        [JsonProperty("end")]
        public string End { get; }

        [JsonProperty("event")]
        public RoomEvent Event { get; }

        [JsonProperty("events_after")]
        public IReadOnlyCollection<RoomEvent> After { get; }

        [JsonProperty("events_before")]
        public IReadOnlyCollection<RoomEvent> Before { get; }

        [JsonProperty("start")]
        public string Start { get; }

        [JsonProperty("state")]
        public IReadOnlyCollection<StateEvent> State { get; }
    }
}
