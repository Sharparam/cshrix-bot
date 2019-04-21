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

    public struct RoomAliasInformation
    {
        [JsonConstructor]
        public RoomAliasInformation(string roomId, IReadOnlyCollection<string> servers)
            : this()
        {
            RoomId = roomId;
            Servers = servers;
        }

        [JsonProperty("room_id")]
        public string RoomId { get; }

        [JsonProperty("servers")]
        public IReadOnlyCollection<string> Servers { get; }
    }
}
