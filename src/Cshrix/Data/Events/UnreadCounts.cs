// <copyright file="UnreadNotificationCounts.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using Newtonsoft.Json;

    public readonly struct UnreadCounts
    {
        public UnreadCounts(int highlightCount, int notificationCount)
            : this()
        {
            HighlightCount = highlightCount;
            NotificationCount = notificationCount;
        }

        [JsonProperty("highlight_count")]
        public int HighlightCount { get; }

        [JsonProperty("notification_count")]
        public int NotificationCount { get; }
    }
}
