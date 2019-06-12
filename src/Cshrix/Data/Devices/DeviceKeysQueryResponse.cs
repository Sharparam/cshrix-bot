// <copyright file="DeviceKeysQueryResponse.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Devices
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains the response to a query asking to download device keys.
    /// </summary>
    public readonly struct DeviceKeysQueryResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceKeysQueryResponse" /> structure.
        /// </summary>
        /// <param name="failures">Any failures recorded from remote homeservers.</param>
        /// <param name="deviceKeys">The requested device keys.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceKeysQueryResponse" /> structure.
        /// </summary>
        /// <param name="failures">Any failures recorded from remote homeservers.</param>
        /// <param name="deviceKeys">The requested device keys.</param>
        [JsonConstructor]
        public DeviceKeysQueryResponse(
            IReadOnlyDictionary<string, object> failures,
            IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, DeviceKeys>> deviceKeys)
            : this()
        {
            Failures = failures;
            DeviceKeys = deviceKeys;
        }

        /// <summary>
        /// Gets a dictionary containing any remote homeserver that could not be reached. The key is the name of
        /// the unreachable server.
        /// </summary>
        /// <remarks>
        /// If a homeserver could not be reached, but the user or device was unknown, no failure is recorded. Instead,
        /// the corresponding user or device is missing from <see cref="DeviceKeys" />.
        /// </remarks>
        [JsonProperty("failures")]
        public IReadOnlyDictionary<string, object> Failures { get; }

        /// <summary>
        /// Gets a dictionary containing information on the queried devices.
        /// </summary>
        /// <remarks>
        /// Maps a user ID to a map from device ID to the information for said device. For each device, the
        /// information returned will be the same as uploaded via <c>/keys/upload</c>, with the addition of an
        /// <c>unsigned</c> property.
        /// </remarks>
        [JsonProperty("device_keys")]
        public IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, DeviceKeys>> DeviceKeys { get; }
    }
}
