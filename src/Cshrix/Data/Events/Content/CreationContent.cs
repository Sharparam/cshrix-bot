// <copyright file="CreationContent.cs">
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
    /// Contains the content of an <c>m.room.create</c> event.
    /// </summary>
    public sealed class CreationContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreationContent" /> class.
        /// </summary>
        /// <param name="creator">The ID of the user who created the room.</param>
        /// <param name="federate">Whether the room federates to remote servers.</param>
        /// <param name="roomVersion">The room version of the room.</param>
        public CreationContent(UserId creator, bool federate, string roomVersion)
        {
            Creator = creator;
            Federate = federate;
            RoomVersion = roomVersion;
        }

        /// <summary>
        /// Gets the ID of the user who created the room.
        /// </summary>
        [JsonProperty("creator")]
        public UserId Creator { get; }

        /// <summary>
        /// Gets a value indicating whether this room federates to other servers.
        /// </summary>
        [JsonProperty("m.federate")]
        public bool Federate { get; }

        /// <summary>
        /// Gets the room version of the room.
        /// </summary>
        [JsonProperty("room_version")]
        public string RoomVersion { get; }
    }
}
