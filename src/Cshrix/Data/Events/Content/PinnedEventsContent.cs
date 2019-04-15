// <copyright file="PinnedEventsContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System.Collections.Generic;
    using System.Linq;

    using JetBrains.Annotations;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class PinnedEventsContent : EventContent
    {
        public PinnedEventsContent([NotNull] IDictionary<string, object> dictionary)
            : base(dictionary)
        {
            var hasEvents = TryGetValue<JArray>("pinned", out var events);
            Pinned = hasEvents
                ? events.ToObject<IReadOnlyCollection<Identifier>>()
                : Enumerable.Empty<Identifier>().ToList().AsReadOnly();
        }

        [JsonProperty("pinned")]
        public IReadOnlyCollection<Identifier> Pinned { get; }
    }
}
