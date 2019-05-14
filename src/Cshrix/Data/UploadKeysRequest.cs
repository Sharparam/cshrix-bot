// <copyright file="UploadKeysRequest.cs">
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

    using Devices;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct UploadKeysRequest
    {
        public UploadKeysRequest(
            DeviceKeys? deviceKeys = null,
            [CanBeNull] IDictionary<string, object> oneTimeKeys = null)
            : this(
                deviceKeys,
                (IReadOnlyDictionary<string, object>)(oneTimeKeys == null
                    ? null
                    : new ReadOnlyDictionary<string, object>(oneTimeKeys)))
        {
        }

        [JsonConstructor]
        public UploadKeysRequest(
            DeviceKeys? deviceKeys = null,
            [CanBeNull] IReadOnlyDictionary<string, object> oneTimeKeys = null)
            : this()
        {
            DeviceKeys = deviceKeys;
            OneTimeKeys = oneTimeKeys;
        }

        [JsonProperty("device_keys", NullValueHandling = NullValueHandling.Ignore)]
        public DeviceKeys? DeviceKeys { get; }

        [JsonProperty(
            "one_time_keys",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyDictionary<string, object> OneTimeKeys { get; }
    }
}
