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

    using Newtonsoft.Json;

    public readonly struct WhoisResponse
    {
        public WhoisResponse(UserId userId, Dictionary<string, DeviceInfo> devices)
            : this()
        {
            UserId = userId;
            Devices = devices;
        }

        [JsonProperty("user_id")]
        public UserId UserId { get; }

        [JsonProperty("devices")]
        public Dictionary<string, DeviceInfo> Devices { get; }
    }
}