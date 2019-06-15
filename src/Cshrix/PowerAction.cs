// <copyright file="PowerAction.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    /// <summary>
    /// Defines actions possible on a room.
    /// </summary>
    public enum PowerAction
    {
        /// <summary>
        /// Invite a user.
        /// </summary>
        Invite,

        /// <summary>
        /// Kick a user.
        /// </summary>
        Kick,

        /// <summary>
        /// Ban a user.
        /// </summary>
        Ban,

        /// <summary>
        /// Redact an event.
        /// </summary>
        Redact
    }
}
