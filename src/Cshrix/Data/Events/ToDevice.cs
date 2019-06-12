// <copyright file="ToDevice.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about send-to-device messages for the client device.
    /// </summary>
    public readonly struct ToDevice
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToDevice" /> structure.
        /// </summary>
        /// <param name="events">A collection of send-to-device messages.</param>
        [JsonConstructor]
        public ToDevice(IReadOnlyCollection<Event> events)
            : this() =>
            Events = events;

        /// <summary>
        /// Gets a collection of send-to-device messages.
        /// </summary>
        [JsonProperty("events")]
        public IReadOnlyCollection<Event> Events { get; }
    }
}
