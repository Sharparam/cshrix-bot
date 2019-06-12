// <copyright file="ConnectionInfo.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;
    using System.Net;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// Contains information about a connection.
    /// </summary>
    public readonly struct ConnectionInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionInfo" /> structure.
        /// </summary>
        /// <param name="ip">Last seen IP for the connection.</param>
        /// <param name="lastSeenAt">Last active time for the connection.</param>
        /// <param name="userAgent">Last seen user agent string for the connection.</param>
        [JsonConstructor]
        public ConnectionInfo(IPAddress ip, DateTimeOffset lastSeenAt, string userAgent)
            : this()
        {
            Ip = ip;
            LastSeenAt = lastSeenAt;
            UserAgent = userAgent;
        }

        /// <summary>
        /// Gets the most recently seen IP for this connection.
        /// </summary>
        [JsonProperty("ip")]
        [JsonConverter(typeof(StringIpAddressConverter))]
        public IPAddress Ip { get; }

        /// <summary>
        /// Gets the time at which the connection was last active.
        /// </summary>
        [JsonProperty("last_seen")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTimeOffset LastSeenAt { get; }

        /// <summary>
        /// Gets the user agent string last seen for this connection.
        /// </summary>
        [JsonProperty("user_agent")]
        public string UserAgent { get; }
    }
}
