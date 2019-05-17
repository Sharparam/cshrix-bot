// <copyright file="RoomAliasInformation.cs">
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
    /// Contains information about a room alias.
    /// </summary>
    public readonly struct RoomAliasInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomAliasInformation" /> structure.
        /// </summary>
        /// <param name="roomId">The room ID for the alias.</param>
        /// <param name="servers">A collection of servers that are aware of the alias.</param>
        [JsonConstructor]
        public RoomAliasInformation(string roomId, IReadOnlyCollection<ServerName> servers)
            : this()
        {
            RoomId = roomId;
            Servers = servers;
        }

        /// <summary>
        /// Gets the room ID for the alias.
        /// </summary>
        [JsonProperty("room_id")]
        public string RoomId { get; }

        /// <summary>
        /// Gets a collection of servers that are aware of the room alias.
        /// </summary>
        [JsonProperty("servers")]
        public IReadOnlyCollection<ServerName> Servers { get; }
    }
}
