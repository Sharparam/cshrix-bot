// <copyright file="UnsignedDeviceInfo.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Newtonsoft.Json;

    /// <summary>
    /// Additional data added to device key information that is not covered by signatures.
    /// </summary>
    public readonly struct UnsignedDeviceInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedDeviceInfo" /> structure.
        /// </summary>
        /// <param name="displayName">The display name of the device, as set by a user.</param>
        [JsonConstructor]
        public UnsignedDeviceInfo(string displayName)
            : this() =>
            DisplayName = displayName;

        /// <summary>
        /// Gets the display name of this device, set by a user.
        /// </summary>
        [JsonProperty("device_display_name")]
        public string DisplayName { get; }
    }
}
