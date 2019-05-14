// <copyright file="DevicesContainer.cs">
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

    using Newtonsoft.Json;

    public readonly struct DevicesResponse
    {
        public DevicesResponse(IEnumerable<Device> devices)
            : this(devices.ToList().AsReadOnly())
        {
        }

        [JsonConstructor]
        public DevicesResponse(IReadOnlyCollection<Device> devices)
            : this() =>
            Devices = devices;

        [JsonProperty("devices")]
        public IReadOnlyCollection<Device> Devices { get; }
    }
}
