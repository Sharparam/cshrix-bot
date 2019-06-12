// <copyright file="RoomNameContent.cs">
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
    /// Contains the name of a room (from the <c>m.room.name</c> event).
    /// </summary>
    public sealed class RoomNameContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomNameContent" /> class.
        /// </summary>
        /// <param name="name">The name of the room.</param>
        public RoomNameContent(string name) => Name = name;

        /// <summary>
        /// Gets the name of the room.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; }
    }
}
