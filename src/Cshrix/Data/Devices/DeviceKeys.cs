// <copyright file="DeviceKeys.cs">
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

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains identity keys for a device.
    /// </summary>
    public readonly struct DeviceKeys
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceKeys" /> structure.
        /// </summary>
        /// <param name="userId">The user ID of the user the device belongs to.</param>
        /// <param name="deviceId">The ID of the device the keys belong to.</param>
        /// <param name="algorithms">A collection of encryption algorithms supported by the device.</param>
        /// <param name="keys">A dictionary of public identity keys.</param>
        /// <param name="signatures">Signatures for this object.</param>
        /// <param name="unsignedDeviceInfo">Additional data not covered by signatures.</param>
        public DeviceKeys(
            UserId userId,
            string deviceId,
            IEnumerable<string> algorithms,
            IDictionary<string, string> keys,
            [CanBeNull] IDictionary<UserId, IDictionary<string, string>> signatures,
            UnsignedDeviceInfo? unsignedDeviceInfo = null)
            : this(
                userId,
                deviceId,
                algorithms.ToList().AsReadOnly(),
                new ReadOnlyDictionary<string, string>(keys),
                signatures == null
                    ? null
                    : new ReadOnlyDictionary<UserId, IReadOnlyDictionary<string, string>>(
                        signatures.ToDictionary(
                            kvp => kvp.Key,
                            kvp => (IReadOnlyDictionary<string, string>)new ReadOnlyDictionary<string, string>(
                                kvp.Value))),
                unsignedDeviceInfo)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceKeys" /> structure.
        /// </summary>
        /// <param name="userId">The user ID of the user the device belongs to.</param>
        /// <param name="deviceId">The ID of the device the keys belong to.</param>
        /// <param name="algorithms">A collection of encryption algorithms supported by the device.</param>
        /// <param name="keys">A dictionary of public identity keys.</param>
        /// <param name="signatures">Signatures for this object.</param>
        /// <param name="unsignedDeviceInfo">Additional data not covered by signatures.</param>
        [JsonConstructor]
        public DeviceKeys(
            UserId userId,
            string deviceId,
            IReadOnlyCollection<string> algorithms,
            IReadOnlyDictionary<string, string> keys,
            [CanBeNull] IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, string>> signatures,
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

        /// <summary>
        /// Gets the ID of the user the device belongs to. Must match the user ID used when logging in.
        /// </summary>
        [JsonProperty("user_id", Order = 6)]
        public UserId UserId { get; }

        /// <summary>
        /// Gets the ID of the device these keys belong to. Must match the device ID used when logging in.
        /// </summary>
        [JsonProperty("device_id", Order = 2)]
        public string DeviceId { get; }

        /// <summary>
        /// Gets a collection of encryption algorithms supported by this device.
        /// </summary>
        [JsonProperty("algorithms", Order = 1)]
        public IReadOnlyCollection<string> Algorithms { get; }

        /// <summary>
        /// Gets a dictionary of public identity keys.
        /// </summary>
        /// <remarks>
        /// Each key should be in the format <c>{algorithm}:{device_id}</c>. The values should be encoded as specified
        /// by the key algorithm.
        /// </remarks>
        [JsonProperty("keys", Order = 3)]
        public IReadOnlyDictionary<string, string> Keys { get; }

        /// <summary>
        /// Gets signatures for the device key object. Maps user IDs to a dictionary mapping
        /// <c>{algorithm}:{device_id}</c> to a signature.
        /// </summary>
        /// <remarks>
        /// The signatures are calculated using the process described at
        /// <a href="https://matrix.org/docs/spec/appendices.html#signing-json">Signing JSON</a>.
        /// </remarks>
        [JsonProperty(
            "signatures",
            Order = 4,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, string>> Signatures { get; }

        /// <summary>
        /// Gets additional device data not covered by signatures.
        /// </summary>
        [JsonProperty(
            "unsigned",
            Order = 5,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public UnsignedDeviceInfo? UnsignedDeviceInfo { get; }
    }
}
