// <copyright file="DeviceKeys.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Newtonsoft.Json;

    public readonly struct DeviceKeys
    {
        public DeviceKeys(
            Identifier userId,
            string deviceId,
            IEnumerable<string> algorithms,
            IDictionary<string, string> keys,
            IDictionary<Identifier, IDictionary<string, string>> signatures,
            UnsignedDeviceInfo? unsignedDeviceInfo = null)
            : this(
                userId,
                deviceId,
                algorithms.ToList().AsReadOnly(),
                new ReadOnlyDictionary<string, string>(keys),
                new ReadOnlyDictionary<Identifier, IReadOnlyDictionary<string, string>>(
                    signatures.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IReadOnlyDictionary<string, string>)new ReadOnlyDictionary<string, string>(kvp.Value))),
                unsignedDeviceInfo)
        {
        }

        [JsonConstructor]
        public DeviceKeys(
            Identifier userId,
            string deviceId,
            IReadOnlyCollection<string> algorithms,
            IReadOnlyDictionary<string, string> keys,
            IReadOnlyDictionary<Identifier, IReadOnlyDictionary<string, string>> signatures,
            UnsignedDeviceInfo? unsignedDeviceInfo = null)
            : this()
        {
            UserId = userId;
            DeviceId = deviceId;
            Algorithms = algorithms;
            Keys = keys;
            Signatures = signatures;
            UnsignedDeviceInfo = unsignedDeviceInfo;
        }

        [JsonProperty("user_id")]
        public Identifier UserId { get; }

        [JsonProperty("device_id")]
        public string DeviceId { get; }

        [JsonProperty("algorithms")]
        public IReadOnlyCollection<string> Algorithms { get; }

        [JsonProperty("keys")]
        public IReadOnlyDictionary<string, string> Keys { get; }

        [JsonProperty("signatures")]
        public IReadOnlyDictionary<Identifier, IReadOnlyDictionary<string, string>> Signatures { get; }

        [JsonProperty("unsigned")]
        public UnsignedDeviceInfo? UnsignedDeviceInfo { get; }
    }
}
