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

    public readonly struct SearchResultCategories
    {
        [JsonConstructor]
        public SearchResultCategories(SearchResultRoomEvents roomEvents)
            : this() =>
            RoomEvents = roomEvents;

        [JsonProperty("room_events")]
        public SearchResultRoomEvents RoomEvents { get; }
    }
}
