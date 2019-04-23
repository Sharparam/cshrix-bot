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

    public readonly struct SessionInfo
    {
        public SessionInfo(IEnumerable<ConnectionInfo> connections)
            : this(connections.ToList().AsReadOnly())
        {
        }

        [JsonConstructor]
        public SessionInfo(IReadOnlyCollection<ConnectionInfo> connections)
            : this() =>
            Connections = connections;

        [JsonProperty("connections")]
        public IReadOnlyCollection<ConnectionInfo> Connections { get; }
    }
}
