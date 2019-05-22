// <copyright file="DeviceMetadata.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Devices
{
    using Newtonsoft.Json;

    /// <summary>
    /// Contains metadata for a device.
    /// </summary>
    public readonly struct DeviceMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceMetadata" /> structure.
        /// </summary>
        /// <param name="displayName">The display name of the device.</param>
        [JsonConstructor]
        public DeviceMetadata(string displayName)
            : this() =>
            DisplayName = displayName;

        /// <summary>
        /// Gets the display name of the device.
        /// </summary>
        [JsonProperty("display_name")]
        public string DisplayName { get; }
    }
}
