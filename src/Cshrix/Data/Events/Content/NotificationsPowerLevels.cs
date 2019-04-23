// <copyright file="NotificationsPowerLevels.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System.ComponentModel;

    using Newtonsoft.Json;

    public readonly struct NotificationsPowerLevels
    {
        [JsonConstructor]
        public NotificationsPowerLevels(int room = 50)
            : this() =>
            Room = room;

        [JsonProperty("room")]
        [DefaultValue(50)]
        public int Room { get; }
    }
}
