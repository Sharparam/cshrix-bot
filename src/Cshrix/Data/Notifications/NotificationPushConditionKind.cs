// <copyright file="NotificationPushConditionKind.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Notifications
{
    using System.Runtime.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Available kinds for a notification push condition.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NotificationPushConditionKind
    {
        /// <summary>
        /// Matches an event.
        /// </summary>
        [EnumMember(Value = "event_match")]
        EventMatch,

        /// <summary>
        /// Contains the user's display name.
        /// </summary>
        [EnumMember(Value = "contains_display_name")]
        ContainsDisplayName,

        /// <summary>
        /// A certain room member count is reached.
        /// </summary>
        [EnumMember(Value = "room_member_count")]
        RoomMemberCount,

        /// <summary>
        /// This takes into account the current power levels in the room, ensuring the sender of the event has
        /// high enough power to trigger the notification.
        /// </summary>
        [EnumMember(Value = "sender_notification_permission")]
        SenderNotificationPermission
    }
}
