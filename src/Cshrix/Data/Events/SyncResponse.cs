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

    using Devices;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains the data from a call to the sync API.
    /// </summary>
    public readonly struct SyncResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyncResponse" /> structure.
        /// </summary>
        /// <param name="nextBatchToken">The token to use for obtaining the next set of data.</param>
        /// <param name="rooms">Updates to rooms.</param>
        /// <param name="presence">Updates to users' presence.</param>
        /// <param name="accountData">Global private data created by the user.</param>
        /// <param name="toDevice">Information about send-to-device messages.</param>
        /// <param name="deviceChangeLists">Information about end-to-end device updates.</param>
        /// <param name="deviceOneTimeKeysCount">Information about end-to-end encryption keys.</param>
        [JsonConstructor]
        public SyncResponse(
            string nextBatchToken,
            SyncedRooms rooms,
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

        /// <summary>
        /// Gets a token to use for obtaining the next set of sync data.
        /// </summary>
        [JsonProperty("next_batch")]
        public string NextBatchToken { get; }

        /// <summary>
        /// Gets an object containing updates to rooms.
        /// </summary>
        [JsonProperty("rooms")]
        public SyncedRooms Rooms { get; }

        /// <summary>
        /// Gets an object containing updates to presence status of users.
        /// </summary>
        [JsonProperty("presence")]
        public EventsContainer Presence { get; }

        /// <summary>
        /// Gets an object containing updates to account data.
        /// </summary>
        [JsonProperty("account_data")]
        public EventsContainer AccountData { get; }

        /// <summary>
        /// Gets information about send-to-device messages for the client device.
        /// </summary>
        [JsonProperty("to_device")]
        public ToDevice? ToDevice { get; }

        /// <summary>
        /// Gets information about end-to-end device updates.
        /// </summary>
        [JsonProperty("device_lists")]
        public DeviceChangeLists? DeviceChangeLists { get; }

        /// <summary>
        /// Gets information about end-to-end encryption keys.
        /// </summary>
        [JsonProperty("device_one_time_keys_count")]
        [CanBeNull]
        public IReadOnlyDictionary<string, int> DeviceOneTimeKeysCount { get; }
    }
}
