// <copyright file="Device.cs">
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

    public readonly struct Device
    {
        [JsonConstructor]
        public Device(string id, string displayName, IPAddress lastSeenIp, DateTimeOffset lastSeenAt)
            : this()
        {
            Id = id;
            DisplayName = displayName;
            LastSeenIp = lastSeenIp;
            LastSeenAt = lastSeenAt;
        }

        [JsonProperty("device_id")]
        public string Id { get; }

        [JsonProperty("display_name")]
        public string DisplayName { get; }

        [JsonProperty("last_seen_ip")]
        [JsonConverter(typeof(StringIpAddressConverter))]
        public IPAddress LastSeenIp { get; }

        [JsonProperty("last_seen_ts")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTimeOffset LastSeenAt { get; }
    }
}
