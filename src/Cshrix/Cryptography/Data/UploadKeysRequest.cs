// <copyright file="UploadKeysRequest.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Cryptography.Data
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Cshrix.Data.Devices;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains encryption keys to upload to a homeserver.
    /// </summary>
    public readonly struct UploadKeysRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadKeysRequest" /> structure.
        /// </summary>
        /// <param name="deviceKeys">Identity keys for the current device.</param>
        /// <param name="oneTimeKeys">One-time keys for "pre-key" messages.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadKeysRequest" /> structure.
        /// </summary>
        /// <param name="deviceKeys">Identity keys for the current device.</param>
        /// <param name="oneTimeKeys">One-time keys for "pre-key" messages.</param>
        [JsonConstructor]
        public UploadKeysRequest(
            DeviceKeys? deviceKeys = null,
            [CanBeNull] IReadOnlyDictionary<string, object> oneTimeKeys = null)
            : this()
        {
            DeviceKeys = deviceKeys;
            OneTimeKeys = oneTimeKeys;
        }

        /// <summary>
        /// Gets identity keys for the current device. May be <c>null</c> if no new identity keys are required.
        /// </summary>
        [JsonProperty("device_keys", NullValueHandling = NullValueHandling.Ignore)]
        public DeviceKeys? DeviceKeys { get; }

        /// <summary>
        /// Gets a dictionary of one-time public keys for "pre-key" messages. The keys in the dict should be in the
        /// format <c>{algorithm}:{key_id}</c>. The format of the dictionary value (the encryption key) is
        /// determined by the key algorithm.
        /// </summary>
        /// <remarks>May be <c>null</c> if no new one-time keys are required.</remarks>
        [JsonProperty(
            "one_time_keys",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyDictionary<string, object> OneTimeKeys { get; }
    }
}
