// <copyright file="EventIdContainer.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Newtonsoft.Json;

    /// <summary>
    /// An object containing an event ID.
    /// </summary>
    public readonly struct EventIdContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventIdContainer" /> structure.
        /// </summary>
        /// <param name="id">The event ID.</param>
        [JsonConstructor]
        public EventIdContainer(string id)
            : this() =>
            Id = id;

        /// <summary>
        /// Gets the event ID.
        /// </summary>
        [JsonProperty("event_id")]
        public string Id { get; }
    }
}
