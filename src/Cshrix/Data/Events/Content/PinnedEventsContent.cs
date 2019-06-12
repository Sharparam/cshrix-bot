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

    using Newtonsoft.Json;

    /// <summary>
    /// Contains information on which events in a room have been pinned.
    /// </summary>
    public sealed class PinnedEventsContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PinnedEventsContent" /> class.
        /// </summary>
        /// <param name="pinned">A collection of event IDs that have been pinned.</param>
        public PinnedEventsContent(IReadOnlyCollection<string> pinned) => Pinned = pinned;

        /// <summary>
        /// Gets a collection of event IDs that have been pinned.
        /// </summary>
        [JsonProperty("pinned")]
        public IReadOnlyCollection<string> Pinned { get; }
    }
}
