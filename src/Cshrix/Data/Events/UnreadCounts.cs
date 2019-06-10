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

    /// <summary>
    /// Describes unread counts for messages.
    /// </summary>
    public readonly struct UnreadCounts
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnreadCounts" /> structure.
        /// </summary>
        /// <param name="highlightCount">The number of unread notifications with the highlight flag set.</param>
        /// <param name="notificationCount">The total number of unread notifications.</param>
        [JsonConstructor]
        public UnreadCounts(int highlightCount, int notificationCount)
            : this()
        {
            HighlightCount = highlightCount;
            NotificationCount = notificationCount;
        }

        /// <summary>
        /// Gets the number of unread notifications for this room with the highlight flag set.
        /// </summary>
        [JsonProperty("highlight_count")]
        public int HighlightCount { get; }

        /// <summary>
        /// Gets the total number of unread notifications for this room.
        /// </summary>
        [JsonProperty("notification_count")]
        public int NotificationCount { get; }
    }
}
