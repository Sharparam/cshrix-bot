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

    public readonly struct ConnectionInfo
    {
        [JsonConstructor]
        public ConnectionInfo(IPAddress ip, DateTimeOffset lastSeenAt, string userAgent)
            : this()
        {
            Ip = ip;
            LastSeenAt = lastSeenAt;
            UserAgent = userAgent;
        }

        [JsonProperty("ip")]
        [JsonConverter(typeof(StringIpAddressConverter))]
        public IPAddress Ip { get; }

        [JsonProperty("last_seen")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTimeOffset LastSeenAt { get; }

        [JsonProperty("user_agent")]
        public string UserAgent { get; }
    }
}
