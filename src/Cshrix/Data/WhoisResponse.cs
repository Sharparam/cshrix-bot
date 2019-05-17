// <copyright file="WhoisResponse.cs">
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

    using Newtonsoft.Json;

    /// <summary>
    /// Contains the result of a whois request.
    /// </summary>
    public readonly struct WhoisResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WhoisResponse" /> structure.
        /// </summary>
        /// <param name="userId">The Matrix user ID of the user.</param>
        /// <param name="devices">The user's devices.</param>
        public WhoisResponse(UserId userId, IDictionary<string, DeviceInfo> devices)
            : this(userId, (IReadOnlyDictionary<string, DeviceInfo>)new ReadOnlyDictionary<string, DeviceInfo>(devices))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WhoisResponse" /> structure.
        /// </summary>
        /// <param name="userId">The Matrix user ID of the user.</param>
        /// <param name="devices">The user's devices.</param>
        [JsonConstructor]
        public WhoisResponse(UserId userId, IReadOnlyDictionary<string, DeviceInfo> devices)
            : this()
        {
            UserId = userId;
            Devices = devices;
        }

        /// <summary>
        /// Gets the Matrix user ID of the user.
        /// </summary>
        [JsonProperty("user_id")]
        public UserId UserId { get; }

        /// <summary>
        /// Gets a dictionary of the user's devices.
        /// </summary>
        [JsonProperty("devices")]
        public IReadOnlyDictionary<string, DeviceInfo> Devices { get; }
    }
}
