// <copyright file="Event.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using Content;

    using Newtonsoft.Json;

    using Serialization;

    [JsonConverter(typeof(EventConverter))]
    public class Event
    {
        public Event(EventContent content, string type, string redacts)
        {
            Content = content;
            Type = type;
            Redacts = redacts;
        }

        [JsonProperty("content")]
        public EventContent Content { get; }

        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("redacts")]
        public string Redacts { get; }

        public bool IsRedaction => Redacts != null;
    }
}
