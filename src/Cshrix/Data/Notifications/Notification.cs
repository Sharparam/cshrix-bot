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

    public readonly struct Notification
    {
        [JsonConstructor]
        public Notification(
            IReadOnlyCollection<NotificationAction> actions,
            Event @event,
            [CanBeNull] string profileTag,
            bool read,
            string roomId,
            DateTimeOffset sentAt)
            : this()
        {
            Actions = actions;
            Event = @event;
            ProfileTag = profileTag;
            Read = read;
            RoomId = roomId;
            SentAt = sentAt;
        }

        [JsonProperty("actions")]
        public IReadOnlyCollection<NotificationAction> Actions { get; }

        [JsonProperty("event")]
        public Event Event { get; }

        [JsonProperty("profile_tag")]
        [CanBeNull]
        public string ProfileTag { get; }

        [JsonProperty("read")]
        public bool Read { get; }

        [JsonProperty("room_id")]
        public string RoomId { get; }

        [JsonProperty("ts")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTimeOffset SentAt { get; }
    }
}
