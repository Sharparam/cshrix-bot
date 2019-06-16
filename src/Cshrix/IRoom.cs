// <copyright file="IRoom.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using System;
    using System.Collections.Generic;

    using Data;
    using Data.Events.Content;

    using JetBrains.Annotations;

    /// <summary>
    /// Defines the API for a room.
    /// </summary>
    public interface IRoom
    {
        /// <summary>
        /// Raised when a message is received in this room.
        /// </summary>
        event EventHandler<MessageEventArgs> Message;

        /// <summary>
        /// Raised when this room is upgraded to a new version.
        /// </summary>
        event EventHandler<TombstonedEventArgs> Tombstoned;

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

        /// <summary>
        /// Gets the ID of the user that created this room.
        /// </summary>
        [CanBeNull]
        UserId Creator { get; }

        /// <summary>
        /// Gets the version of this room.
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Gets the latest power levels set on the room.
        /// </summary>
        IPowerLevels PowerLevels { get; }

        /// <summary>
        /// Gets a value indicating whether this room has been tombstoned.
        /// </summary>
        bool IsTombstoned { get; }

        /// <summary>
        /// Gets information about the tombstone event, if any.
        /// </summary>
        [CanBeNull]
        TombstoneContent TombstoneContent { get; }
    }
}
