// <copyright file="DeviceChangeLists.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Devices
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public readonly struct DeviceChangeLists
    {
        [JsonConstructor]
        public DeviceChangeLists(IReadOnlyCollection<UserId> changed, IReadOnlyCollection<UserId> left)
            : this()
        {
            Changed = changed;
            Left = left;
        }

        [JsonProperty("changed")]
        public IReadOnlyCollection<UserId> Changed { get; }

        [JsonProperty("left")]
        public IReadOnlyCollection<UserId> Left { get; }
    }
}
