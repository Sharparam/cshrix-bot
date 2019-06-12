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

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// Specifies a set of keys to download.
    /// </summary>
    public readonly struct DeviceKeysQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceKeysQuery" /> structure.
        /// </summary>
        /// <param name="deviceKeys">A dictionary specifying which keys to download.</param>
        /// <param name="timeout">The time to wait when downloading remote keys.</param>
        /// <param name="token">A "since" token to specify a point in time to return keys from.</param>
        public DeviceKeysQuery(
            IDictionary<UserId, IEnumerable<string>> deviceKeys,
            TimeSpan? timeout = null,
            string token = null)
            : this(
                new ReadOnlyDictionary<UserId, IReadOnlyCollection<string>>(
                    deviceKeys.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IReadOnlyCollection<string>)kvp.Value.ToList().AsReadOnly())),
                timeout,
                token)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceKeysQuery" /> structure.
        /// </summary>
        /// <param name="deviceKeys">A dictionary specifying which keys to download.</param>
        /// <param name="timeout">The time to wait when downloading remote keys.</param>
        /// <param name="token">A "since" token to specify a point in time to return keys from.</param>
        [JsonConstructor]
        public DeviceKeysQuery(
            IReadOnlyDictionary<UserId, IReadOnlyCollection<string>> deviceKeys,
            TimeSpan? timeout = null,
            [CanBeNull] string token = null)
            : this()
        {
            DeviceKeys = deviceKeys;
            Timeout = timeout ?? TimeSpan.FromSeconds(10);
            Token = token;
        }

        /// <summary>
        /// Gets a dictionary specifying the keys to download.
        /// </summary>
        /// <remarks>
        /// The dictionary maps user IDs to a collection of device IDs, or to an empty collection to indicate all
        /// devices for the corresponding user.
        /// </remarks>
        [JsonProperty("device_keys")]
        public IReadOnlyDictionary<UserId, IReadOnlyCollection<string>> DeviceKeys { get; }

        /// <summary>
        /// Gets the time to wait when downloading keys from remote servers.
        /// </summary>
        [JsonProperty("timeout")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan Timeout { get; }

        /// <summary>
        /// Gets a "since" token to use to specify that keys are being fetched in response to a received device
        /// update from a sync request.
        /// </summary>
        /// <remarks>
        /// This allows the server to ensure its response contains the keys advertised by the notification in the sync.
        /// </remarks>
        [JsonProperty(
            "token",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Token { get; }
    }
}
