// <copyright file="Notification.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Notifications
{
    using System;
    using System.Collections.Generic;

    using Events;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// Contains information about a notification.
    /// </summary>
    public readonly struct Notification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Notification" /> structure.
        /// </summary>
        /// <param name="actions">Actions that should be performed as a response to the notification.</param>
        /// <param name="event">The event that triggered the notification.</param>
        /// <param name="profileTag">The profile tag of the rule that matched the event.</param>
        /// <param name="isRead">Whether the user has sent a read receipt for the notification event.</param>
        /// <param name="roomId">The ID of the room in which the event was posted.</param>
        /// <param name="sentAt">The date and time at which the event notification was sent.</param>
        [JsonConstructor]
        public Notification(
            IReadOnlyCollection<NotificationAction> actions,
            Event @event,
            [CanBeNull] string profileTag,
            bool isRead,
            string roomId,
            DateTimeOffset sentAt)
            : this()
        {
            Actions = actions;
            Event = @event;
            ProfileTag = profileTag;
            IsRead = isRead;
            RoomId = roomId;
            SentAt = sentAt;
        }

        /// <summary>
        /// Gets a collection of actions that should be performed when the conditions for this rule are met.
        /// </summary>
        [JsonProperty("actions")]
        public IReadOnlyCollection<NotificationAction> Actions { get; }

        /// <summary>
        /// Gets the event that triggered the notification.
        /// </summary>
        [JsonProperty("event")]
        public Event Event { get; }

        /// <summary>
        /// Gets the profile tag of the rule that matched the event.
        /// </summary>
        [JsonProperty(
            "profile_tag",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string ProfileTag { get; }

        /// <summary>
        /// Get a value indicating whether the user has sent a read receipt indicating that they have acknowledged
        /// this notification.
        /// </summary>
        [JsonProperty("read")]
        public bool IsRead { get; }

        /// <summary>
        /// Gets the ID of the room in which the event was posted.
        /// </summary>
        [JsonProperty("room_id")]
        public string RoomId { get; }

        /// <summary>
        /// Gets the date and time at which the event notification was sent.
        /// </summary>
        [JsonProperty("ts")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTimeOffset SentAt { get; }
    }
}
