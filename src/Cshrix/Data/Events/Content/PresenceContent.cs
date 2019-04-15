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
    using System.Collections.Generic;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Serialization;

    public class PresenceContent : EventContent
    {
        public PresenceContent([NotNull] IDictionary<string, object> dictionary)
            : base(dictionary)
        {
            AvatarUri = GetValueOrDefault<Uri>("avatar_url");
            DisplayName = GetValueOrDefault<string>("displayname");
            var lastActiveAgoMs = GetValueOrDefault<long>("last_active_ago");
            LastActiveAgo = TimeSpan.FromMilliseconds(lastActiveAgoMs);
            Presence = GetValueOrDefault<Presence>("presence");
            CurrentlyActive = GetValueOrDefault<bool>("currently_active");
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
