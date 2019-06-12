// <copyright file="SessionInfo.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a session.
    /// </summary>
    public readonly struct SessionInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionInfo" /> structure.
        /// </summary>
        /// <param name="connections">A collection of connections.</param>
        public SessionInfo(IEnumerable<ConnectionInfo> connections)
            : this(connections.ToList().AsReadOnly())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionInfo" /> structure.
        /// </summary>
        /// <param name="connections">A collection of connections.</param>
        [JsonConstructor]
        public SessionInfo(IReadOnlyCollection<ConnectionInfo> connections)
            : this() =>
            Connections = connections;

        /// <summary>
        /// Gets a collection of connections for this session.
        /// </summary>
        [JsonProperty("connections")]
        public IReadOnlyCollection<ConnectionInfo> Connections { get; }
    }
}
