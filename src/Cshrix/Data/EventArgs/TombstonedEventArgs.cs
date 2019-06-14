// <copyright file="TombstonedEventArgs.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;

    using Events.Content;

    /// <summary>
    /// Contains data for the event raised when a room is tombstoned.
    /// </summary>
    public sealed class TombstonedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TombstonedEventArgs" /> class.
        /// </summary>
        /// <param name="content">Information about the tombstone event.</param>
        public TombstonedEventArgs(TombstoneContent content) => Content = content;

        /// <summary>
        /// Gets information about the tombstone event, such as the replacement room ID.
        /// </summary>
        public TombstoneContent Content { get; }
    }
}
