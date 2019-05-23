// <copyright file="InvitedEventArgs.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;

    /// <summary>
    /// Contains the data for the event when the user is invited to a room.
    /// </summary>
    public sealed class InvitedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvitedEventArgs" /> class.
        /// </summary>
        /// <param name="room">The room the user was invited to.</param>
        public InvitedEventArgs(IRoom room)
        {
            Room = room;
        }

        /// <summary>
        /// Gets the room the user was invited to.
        /// </summary>
        public IRoom Room { get; }
    }
}
