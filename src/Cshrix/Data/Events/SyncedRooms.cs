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

    /// <summary>
    /// Contains sync data for rooms in various states.
    /// </summary>
    public readonly struct SyncedRooms
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyncedRooms" /> structure.
        /// </summary>
        /// <param name="joined">Data for rooms the user is joined to.</param>
        /// <param name="invited">Data for rooms the user has been invited to.</param>
        /// <param name="left">Last known data for rooms the user has left.</param>
        [JsonConstructor]
        public SyncedRooms(
            IReadOnlyDictionary<string, JoinedRoom> joined,
            IReadOnlyDictionary<string, InvitedRoom> invited,
            IReadOnlyDictionary<string, LeftRoom> left)
            : this()
        {
            Joined = joined;
            Invited = invited;
            Left = left;
        }

        /// <summary>
        /// Gets a dictionary of joined rooms and their associated events and state.
        /// </summary>
        [JsonProperty("join")]
        public IReadOnlyDictionary<string, JoinedRoom> Joined { get; }

        /// <summary>
        /// Gets a dictionary of rooms the user has been invited to, along with their events and state.
        /// </summary>
        [JsonProperty("invite")]
        public IReadOnlyDictionary<string, InvitedRoom> Invited { get; }

        /// <summary>
        /// Gets a dictionary of rooms which the user has left, along with the last known events and state.
        /// </summary>
        [JsonProperty("leave")]
        public IReadOnlyDictionary<string, LeftRoom> Left { get; }
    }
}
