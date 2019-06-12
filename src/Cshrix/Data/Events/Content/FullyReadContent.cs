// <copyright file="FullyReadContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using Newtonsoft.Json;

    /// <summary>
    /// Contains data for <c>m.fully_read</c> events.
    /// </summary>
    public sealed class FullyReadContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FullyReadContent" /> class.
        /// </summary>
        /// <param name="eventId">The ID of the event up to which the user is fully read.</param>
        public FullyReadContent(string eventId) => EventId = eventId;

        /// <summary>
        /// Gets the ID of the event which the user has fully read up to.
        /// </summary>
        [JsonProperty("event_id")]
        public string EventId { get; }
    }
}
