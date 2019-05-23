// <copyright file="InvitedRoom.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using Newtonsoft.Json;

    /// <summary>
    /// Describes a room to which the user has been invited.
    /// </summary>
    public readonly struct InvitedRoom
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvitedRoom" /> structure.
        /// </summary>
        /// <param name="inviteState">The invite state.</param>
        [JsonConstructor]
        public InvitedRoom(InviteState inviteState)
            : this() =>
            InviteState = inviteState;

        /// <summary>
        /// Gets the invite state for the room.
        /// </summary>
        [JsonProperty("invite_state")]
        public InviteState InviteState { get; }
    }
}
