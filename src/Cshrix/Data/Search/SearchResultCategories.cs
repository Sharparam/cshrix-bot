// <copyright file="SearchResultCategories.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Search
{
    using Newtonsoft.Json;

    /// <summary>
    /// Contains categorized search results.
    /// </summary>
    public readonly struct SearchResultCategories
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultCategories" /> structure.
        /// </summary>
        /// <param name="roomEvents">Room events that matched the search.</param>
        [JsonConstructor]
        public SearchResultCategories(SearchResultRoomEvents roomEvents)
            : this() =>
            RoomEvents = roomEvents;

        /// <summary>
        /// Contains room events that matched the search.
        /// </summary>
        [JsonProperty("room_events")]
        public SearchResultRoomEvents RoomEvents { get; }
    }
}
