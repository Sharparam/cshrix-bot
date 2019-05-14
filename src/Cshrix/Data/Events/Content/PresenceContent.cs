// <copyright file="PresenceContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Presence;

    using Serialization;

    public class PresenceContent : EventContent
    {
        public PresenceContent(
            Uri avatarUri,
            string displayName,
            TimeSpan lastActiveAgo,
            Presence presence,
            bool currentlyActive)
        {
            AvatarUri = avatarUri;
            DisplayName = displayName;
            LastActiveAgo = lastActiveAgo;
            Presence = presence;
            CurrentlyActive = currentlyActive;
        }

        [JsonProperty("avatar_url")]
        [CanBeNull]
        public Uri AvatarUri { get; }

        [JsonProperty("displayname")]
        [CanBeNull]
        public string DisplayName { get; }

        [JsonProperty("last_active_ago")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan LastActiveAgo { get; }

        [JsonProperty("presence")]
        public Presence Presence { get; }

        [JsonProperty("currently_active")]
        public bool CurrentlyActive { get; }
    }
}
