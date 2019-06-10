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

    /// <summary>
    /// Specifies the power level requirement for specific notification types.
    /// </summary>
    public readonly struct NotificationsPowerLevels
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationsPowerLevels" /> structure.
        /// </summary>
        /// <param name="room"></param>
        [JsonConstructor]
        public NotificationsPowerLevels(int room = 50)
            : this() =>
            Room = room;

        /// <summary>
        /// Level required for the <c>room</c> notification type.
        /// </summary>
        [JsonProperty("room")]
        [DefaultValue(50)]
        public int Room { get; }
    }
}
