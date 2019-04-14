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
            IReadOnlyDictionary<Identifier, JoinedRoom> joined,
            IReadOnlyDictionary<Identifier, InvitedRoom> invited,
            IReadOnlyDictionary<Identifier, LeftRoom> left)
            : this()
        {
            Joined = joined;
            Invited = invited;
            Left = left;
        }

        [JsonProperty("join")]
        public IReadOnlyDictionary<Identifier, JoinedRoom> Joined { get; }

        [JsonProperty("invite")]
        public IReadOnlyDictionary<Identifier, InvitedRoom> Invited { get; }

        [JsonProperty("leave")]
        public IReadOnlyDictionary<Identifier, LeftRoom> Left { get; }
    }
}
