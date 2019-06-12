// <copyright file="DeviceChangeLists.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Devices
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains information on E2E device updates.
    /// </summary>
    /// <remarks>Only present on an initial sync.</remarks>
    public readonly struct DeviceChangeLists
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceChangeLists" /> structure.
        /// </summary>
        /// <param name="changed">
        /// User IDs whose devices have updated or recently joined an E2EE room with the client.
        /// </param>
        /// <param name="left">User IDs whose devices we no longer share encrypted rooms with.</param>
        [JsonConstructor]
        public DeviceChangeLists(IReadOnlyCollection<UserId> changed, IReadOnlyCollection<UserId> left)
            : this()
        {
            Changed = changed;
            Left = left;
        }

        /// <summary>
        /// Gets a collection of user IDs who have updated their device identity keys, or who now share an encrypted
        /// room with the client since the previous sync response.
        /// </summary>
        [JsonProperty("changed")]
        public IReadOnlyCollection<UserId> Changed { get; }

        /// <summary>
        /// Gets a collection of user IDs with whom we do not share any encrypted rooms anymore since the previous
        /// sync response.
        /// </summary>
        [JsonProperty("left")]
        public IReadOnlyCollection<UserId> Left { get; }
    }
}
