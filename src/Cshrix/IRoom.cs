// <copyright file="IRoom.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using System.Collections.Generic;

    using Data;

    using JetBrains.Annotations;

    /// <summary>
    /// Defines the API for a room.
    /// </summary>
    public interface IRoom
    {
        /// <summary>
        /// Gets the ID of the room.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the current membership status of the user in this room.
        /// </summary>
        Membership Membership { get; }

        /// <summary>
        /// Gets the canonical alias of the room, if one is set.
        /// </summary>
        [CanBeNull]
        RoomAlias CanonicalAlias { get; }

        /// <summary>
        /// Gets the aliases available for the room.
        /// </summary>
        IReadOnlyCollection<RoomAlias> Aliases { get; }

        /// <summary>
        /// Gets the name of the room.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the topic of the room.
        /// </summary>
        string Topic { get; }
    }
}
