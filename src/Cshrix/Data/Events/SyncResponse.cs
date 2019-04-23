// <copyright file="SyncResponse.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using System.Collections.Generic;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct SyncResponse
    {
        [JsonConstructor]
        public SyncResponse(
            string nextBatchToken,
            Rooms rooms,
            EventsContainer presence,
            EventsContainer accountData,
            ToDevice? toDevice,
            DeviceChangeLists? deviceChangeLists,
            [CanBeNull] IReadOnlyDictionary<string, int> deviceOneTimeKeysCount)
            : this()
        {
            NextBatchToken = nextBatchToken;
            Rooms = rooms;
            Presence = presence;
            AccountData = accountData;
            ToDevice = toDevice;
            DeviceChangeLists = deviceChangeLists;
            DeviceOneTimeKeysCount = deviceOneTimeKeysCount;
        }

        [JsonProperty("next_batch")]
        public string NextBatchToken { get; }

        [JsonProperty("rooms")]
        public Rooms Rooms { get; }

        [JsonProperty("presence")]
        public EventsContainer Presence { get; }

        [JsonProperty("account_data")]
        public EventsContainer AccountData { get; }

        [JsonProperty("to_device")]
        public ToDevice? ToDevice { get; }

        [JsonProperty("device_lists")]
        public DeviceChangeLists? DeviceChangeLists { get; }

        [JsonProperty("device_one_time_keys_count")]
        [CanBeNull]
        public IReadOnlyDictionary<string, int> DeviceOneTimeKeysCount { get; }
    }
}
