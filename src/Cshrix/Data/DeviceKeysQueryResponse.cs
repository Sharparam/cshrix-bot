// <copyright file="DeviceKeysQueryResponse.cs">
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

    public readonly struct DeviceKeysQueryResponse
    {
        public DeviceKeysQueryResponse(
            IDictionary<string, object> failures,
            IDictionary<UserId, IDictionary<string, DeviceKeys>> deviceKeys)
            : this(
                new ReadOnlyDictionary<string, object>(failures),
                new ReadOnlyDictionary<UserId, IReadOnlyDictionary<string, DeviceKeys>>(
                    deviceKeys.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IReadOnlyDictionary<string, DeviceKeys>)new ReadOnlyDictionary<string, DeviceKeys>(
                            kvp.Value))))
        {
        }

        [JsonConstructor]
        public DeviceKeysQueryResponse(
            IReadOnlyDictionary<string, object> failures,
            IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, DeviceKeys>> deviceKeys)
            : this()
        {
            Failures = failures;
            DeviceKeys = deviceKeys;
        }

        [JsonProperty("failures")]
        public IReadOnlyDictionary<string, object> Failures { get; }

        [JsonProperty("device_keys")]
        public IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, DeviceKeys>> DeviceKeys { get; }
    }
}
