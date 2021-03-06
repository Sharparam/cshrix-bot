// <copyright file="DeviceInfo.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Devices
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a user's device.
    /// </summary>
    public readonly struct DeviceInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceInfo" /> structure.
        /// </summary>
        /// <param name="sessions">A collection of sessions.</param>
        public DeviceInfo(IEnumerable<SessionInfo> sessions)
            : this(sessions.ToList().AsReadOnly())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceInfo" /> structure.
        /// </summary>
        /// <param name="sessions">A collection of sessions.</param>
        [JsonConstructor]
        public DeviceInfo(IReadOnlyCollection<SessionInfo> sessions)
            : this() =>
            Sessions = sessions;

        /// <summary>
        /// Gets a collection of sessions for this device.
        /// </summary>
        [JsonProperty("sessions")]
        public IReadOnlyCollection<SessionInfo> Sessions { get; }
    }
}
