// <copyright file="NotificationPushersContainer.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Notifications
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// A wrapper structure containing a collection of notification pushers.
    /// </summary>
    public readonly struct NotificationPushersContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationPushersContainer" /> structure.
        /// </summary>
        /// <param name="pushers">A collection of notification pushers.</param>
        [JsonConstructor]
        public NotificationPushersContainer(IReadOnlyCollection<NotificationPusher> pushers)
            : this() =>
            Pushers = pushers;

        /// <summary>
        /// Gets the collection of notification pushers.
        /// </summary>
        [JsonProperty("pushers")]
        public IReadOnlyCollection<NotificationPusher> Pushers { get; }
    }
}
