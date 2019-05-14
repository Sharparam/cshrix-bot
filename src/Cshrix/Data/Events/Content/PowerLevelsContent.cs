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

    public sealed class PowerLevelsContent : EventContent
    {
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

        [JsonProperty("ban")]
        [DefaultValue(50)]
        public int Ban { get; }

        [JsonProperty("events")]
        public IReadOnlyDictionary<string, int> Events { get; }

        [JsonProperty("events_default")]
        [DefaultValue(0)]
        public int EventsDefault { get; }

        [JsonProperty("invite")]
        [DefaultValue(50)]
        public int Invite { get; }

        [JsonProperty("kick")]
        [DefaultValue(50)]
        public int Kick { get; }

        [JsonProperty("redact")]
        [DefaultValue(50)]
        public int Redact { get; }

        [JsonProperty("state_default")]
        [DefaultValue(50)]
        public int StateDefault { get; }

        [JsonProperty("users")]
        public IReadOnlyDictionary<UserId, int> Users { get; }

        [JsonProperty("users_default")]
        [DefaultValue(0)]
        public int UsersDefault { get; }

        [JsonProperty("notifications")]
        public NotificationsPowerLevels Notifications { get; }
    }
}
