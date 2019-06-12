// <copyright file="DeleteDevicesRequest.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Devices
{
    using System.Collections.Generic;
    using System.Linq;

    using Authentication;

    using Newtonsoft.Json;

    /// <summary>
    /// Specifies devices to delete.
    /// </summary>
    public sealed class DeleteDevicesRequest : AuthenticationContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteDevicesRequest" /> structure.
        /// </summary>
        /// <param name="devices">A collection of device IDs to delete.</param>
        /// <param name="authentication">Data authenticating the user.</param>
        public DeleteDevicesRequest(IEnumerable<string> devices, SessionContainer authentication = null)
            : this(devices.ToList().AsReadOnly(), authentication)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteDevicesRequest" /> structure.
        /// </summary>
        /// <param name="devices">A collection of device IDs to delete.</param>
        /// <param name="authentication">Data authenticating the user.</param>
        [JsonConstructor]
        public DeleteDevicesRequest(IReadOnlyCollection<string> devices, SessionContainer authentication = null)
            : base(authentication) =>
            Devices = devices;

        /// <summary>
        /// Gets a collection of device IDs to delete.
        /// </summary>
        [JsonProperty("devices")]
        public IReadOnlyCollection<string> Devices { get; }
    }
}
