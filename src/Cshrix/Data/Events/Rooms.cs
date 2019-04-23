// <copyright file="Rooms.cs">
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

    public readonly struct Rooms
    {
        [JsonConstructor]
        public Rooms(
            IReadOnlyDictionary<string, JoinedRoom> joined,
            IReadOnlyDictionary<string, InvitedRoom> invited,
            IReadOnlyDictionary<string, LeftRoom> left)
            : this()
        {
            Joined = joined;
            Invited = invited;
            Left = left;
        }

        [JsonProperty("join")]
        public IReadOnlyDictionary<string, JoinedRoom> Joined { get; }

        [JsonProperty("invite")]
        public IReadOnlyDictionary<string, InvitedRoom> Invited { get; }

        [JsonProperty("leave")]
        public IReadOnlyDictionary<string, LeftRoom> Left { get; }
    }
}
