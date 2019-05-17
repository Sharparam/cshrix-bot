// <copyright file="JoinedRooms.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains a collection of room IDs that a user has joined.
    /// </summary>
    public readonly struct JoinedRooms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinedRooms" /> structure.
        /// </summary>
        /// <param name="roomIds">A collection of room IDs that the user is joined to.</param>
        [JsonConstructor]
        public JoinedRooms(IReadOnlyCollection<string> roomIds)
            : this() =>
            RoomIds = roomIds;

        /// <summary>
        /// Gets the collection of room IDs that the user is joined to.
        /// </summary>
        [JsonProperty("joined_rooms")]
        public IReadOnlyCollection<string> RoomIds { get; }
    }
}
