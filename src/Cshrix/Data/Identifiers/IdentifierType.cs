// <copyright file="IdentifierType.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    /// <summary>
    /// Available types of identifiers.
    /// </summary>
    public enum IdentifierType
    {
        /// <summary>
        /// A user ID. These are in the form of <c>@{localpart}:{server}</c>.
        /// </summary>
        User,

        /// <summary>
        /// A group ID. These are in the form of <c>+{localpart}:{server}</c>.
        /// </summary>
        Group,

        /// <summary>
        /// An alias for a room. These are in the form of <c>#{localpart}:{server}</c>.
        /// </summary>
        RoomAlias
    }
}
