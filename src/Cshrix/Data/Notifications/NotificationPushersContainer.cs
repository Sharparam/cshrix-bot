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

    public readonly struct NotificationPushersContainer
    {
        [JsonConstructor]
        public NotificationPushersContainer(IReadOnlyCollection<NotificationPusher> pushers)
            : this() =>
            Pushers = pushers;

        [JsonProperty("pushers")]
        public IReadOnlyCollection<NotificationPusher> Pushers { get; }
    }
}
