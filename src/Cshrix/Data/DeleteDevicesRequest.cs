// <copyright file="DeleteDevicesRequest.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Authentication;

    using Newtonsoft.Json;

    public class DeleteDevicesRequest : AuthenticationContainer
    {
        public DeleteDevicesRequest(IEnumerable<string> devices, SessionContainer authentication = null)
            : this(devices.ToList().AsReadOnly(), authentication)
        {
        }

        [JsonConstructor]
        public DeleteDevicesRequest(IReadOnlyCollection<string> devices, SessionContainer authentication = null)
            : base(authentication) =>
            Devices = devices;

        [JsonProperty("devices")]
        public IReadOnlyCollection<string> Devices { get; }
    }
}
