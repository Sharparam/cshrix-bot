// <copyright file="JoinedEventArgs.cs">
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
    /// Contains the data for the event when the user joins a room.
    /// </summary>
    public sealed class JoinedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinedEventArgs" /> class.
        /// </summary>
        /// <param name="room">The room the user joined.</param>
        public JoinedEventArgs(IRoom room) => Room = room;

        /// <summary>
        /// Gets the room the user joined.
        /// </summary>
        public IRoom Room { get; }
    }
}
