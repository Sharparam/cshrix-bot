// <copyright file="PowerLevelsContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using Newtonsoft.Json;

    /// <summary>
    /// Describes configured power levels in a room.
    /// </summary>
    public sealed class PowerLevelsContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PowerLevelsContent" /> class.
        /// </summary>
        /// <param name="ban">Level required to ban users.</param>
        /// <param name="events">Dictionary mapping event types to the level required to send them.</param>
        /// <param name="eventsDefault">Default level required to send events.</param>
        /// <param name="invite">Level required to invite users.</param>
        /// <param name="kick">Level required to kick users.</param>
        /// <param name="redact">Level required to redact events.</param>
        /// <param name="stateDefault">Default level required to send state events.</param>
        /// <param name="users">Dictionary mapping user IDs to their power level.</param>
        /// <param name="usersDefault">Default power level for users.</param>
        /// <param name="notifications">Object specifying level required for specific notification types.</param>
        public PowerLevelsContent(
            int ban,
            IReadOnlyDictionary<string, int> events,
            int eventsDefault,
            int invite,
            int kick,
            int redact,
            int stateDefault,
            IReadOnlyDictionary<UserId, int> users,
            int usersDefault,
            NotificationsPowerLevels notifications)
        {
            Ban = ban;
            Events = events;
            EventsDefault = eventsDefault;
            Invite = invite;
            Kick = kick;
            Redact = redact;
            StateDefault = stateDefault;
            Users = users;
            UsersDefault = usersDefault;
            Notifications = notifications;
        }

        /// <summary>
        /// Gets the level required to ban a user.
        /// </summary>
        [JsonProperty("ban")]
        [DefaultValue(50)]
        public int Ban { get; }

        /// <summary>
        /// Gets a dictionary mapping event types to the level required to send them.
        /// </summary>
        [JsonProperty("events")]
        public IReadOnlyDictionary<string, int> Events { get; }

        /// <summary>
        /// Gets the default level required to send message events.
        /// </summary>
        [JsonProperty("events_default")]
        [DefaultValue(0)]
        public int EventsDefault { get; }

        /// <summary>
        /// Gets the level required to invite a user.
        /// </summary>
        [JsonProperty("invite")]
        [DefaultValue(50)]
        public int Invite { get; }

        /// <summary>
        /// Gets the level required to kick a user.
        /// </summary>
        [JsonProperty("kick")]
        [DefaultValue(50)]
        public int Kick { get; }

        /// <summary>
        /// Gets the level required to redact an event.
        /// </summary>
        [JsonProperty("redact")]
        [DefaultValue(50)]
        public int Redact { get; }

        /// <summary>
        /// Gets the default level required to send state events.
        /// </summary>
        [JsonProperty("state_default")]
        [DefaultValue(50)]
        public int StateDefault { get; }

        /// <summary>
        /// Gets a dictionary mapping user IDs to their configured power level.
        /// </summary>
        [JsonProperty("users")]
        public IReadOnlyDictionary<UserId, int> Users { get; }

        /// <summary>
        /// Gets the default power level for users in the room.
        /// </summary>
        [JsonProperty("users_default")]
        [DefaultValue(0)]
        public int UsersDefault { get; }

        /// <summary>
        /// Gets an object specifying level requirements for specific notification types.
        /// </summary>
        [JsonProperty("notifications")]
        public NotificationsPowerLevels Notifications { get; }
    }
}
