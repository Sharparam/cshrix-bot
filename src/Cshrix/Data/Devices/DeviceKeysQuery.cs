// <copyright file="DeviceKeysQuery.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Devices
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Newtonsoft.Json;

    using Serialization;

    public readonly struct DeviceKeysQuery
    {
        public DeviceKeysQuery(
            IDictionary<UserId, IEnumerable<string>> deviceKeys,
            string token,
            TimeSpan? timeout = null)
            : this(
                new ReadOnlyDictionary<UserId, IReadOnlyCollection<string>>(
                    deviceKeys.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IReadOnlyCollection<string>)kvp.Value.ToList().AsReadOnly())),
                token,
                timeout)
        {
        }

        [JsonConstructor]
        public DeviceKeysQuery(
            IReadOnlyDictionary<UserId, IReadOnlyCollection<string>> deviceKeys,
            string token,
            TimeSpan? timeout = null)
            : this()
        {
            Timeout = timeout ?? TimeSpan.FromSeconds(10);
            DeviceKeys = deviceKeys;
            Token = token;
        }

        [JsonProperty("timeout")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan Timeout { get; }

        [JsonProperty("device_keys")]
        public IReadOnlyDictionary<UserId, IReadOnlyCollection<string>> DeviceKeys { get; }

        [JsonProperty("token")]
        public string Token { get; }
    }
}
