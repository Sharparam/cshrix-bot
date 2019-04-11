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

    using Newtonsoft.Json;

    using Serialization;

    public readonly struct ConnectionInfo
    {
        [JsonConstructor]
        public ConnectionInfo(string ip, DateTimeOffset lastSeen, string userAgent)
            : this()
        {
            Ip = ip;
            LastSeen = lastSeen;
            UserAgent = userAgent;
        }

        [JsonProperty("ip")]
        public string Ip { get; }

        [JsonProperty("last_seen")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTimeOffset LastSeen { get; }

        [JsonProperty("user_agent")]
        public string UserAgent { get; }
    }
}
