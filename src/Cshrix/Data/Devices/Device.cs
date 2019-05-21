// <copyright file="Device.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Devices
{
    using System;
    using System.Net;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// Represents a device.
    /// </summary>
    public readonly struct Device
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Device" /> structure.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="displayName"></param>
        /// <param name="lastSeenIp"></param>
        /// <param name="lastSeenAt"></param>
        [JsonConstructor]
        public Device(string id, string displayName, IPAddress lastSeenIp, DateTimeOffset lastSeenAt)
            : this()
        {
            Id = id;
            DisplayName = displayName;
            LastSeenIp = lastSeenIp;
            LastSeenAt = lastSeenAt;
        }

        /// <summary>
        /// Gets the ID of the device.
        /// </summary>
        [JsonProperty("device_id")]
        public string Id { get; }

        /// <summary>
        /// Gets the display name of the device.
        /// </summary>
        [JsonProperty("display_name")]
        public string DisplayName { get; }

        /// <summary>
        /// Gets the IP address this device was last seen connecting from.
        /// </summary>
        [JsonProperty("last_seen_ip")]
        [JsonConverter(typeof(StringIpAddressConverter))]
        public IPAddress LastSeenIp { get; }

        /// <summary>
        /// Gets the date and time at which this device was last seen.
        /// </summary>
        [JsonProperty("last_seen_ts")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTimeOffset LastSeenAt { get; }
    }
}
