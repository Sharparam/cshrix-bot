// <copyright file="SyncEventArgs.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;

    using Events;

    /// <summary>
    /// Contains the sync response on a sync event.
    /// </summary>
    public class SyncEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyncEventArgs" /> class.
        /// </summary>
        /// <param name="response">The sync response.</param>
        public SyncEventArgs(SyncResponse response)
        {
            Response = response;
        }

        /// <summary>
        /// Gets the sync response.
        /// </summary>
        public SyncResponse Response { get; }
    }
}
