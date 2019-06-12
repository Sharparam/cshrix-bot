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

    /// <summary>
    /// Describes a presence event.
    /// </summary>
    public sealed class PresenceContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PresenceContent" /> class.
        /// </summary>
        /// <param name="avatarUri">The URI to the user's avatar.</param>
        /// <param name="displayName">The user's display name.</param>
        /// <param name="lastActiveAgo">The duration since the user was last active.</param>
        /// <param name="presence">The user's current presence.</param>
        /// <param name="currentlyActive">Whether the user is currently active.</param>
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

        /// <summary>
        /// Gets the user's current avatar URI.
        /// </summary>
        [JsonProperty("avatar_url")]
        [CanBeNull]
        public Uri AvatarUri { get; }

        /// <summary>
        /// Gets the user's current display name.
        /// </summary>
        [JsonProperty("displayname")]
        [CanBeNull]
        public string DisplayName { get; }

        /// <summary>
        /// Gets a duration for how long ago the user was active.
        /// </summary>
        [JsonProperty("last_active_ago")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan LastActiveAgo { get; }

        /// <summary>
        /// Gets the user's current presence.
        /// </summary>
        [JsonProperty("presence")]
        public Presence Presence { get; }

        /// <summary>
        /// Gets a value indicating whether the user is currently active.
        /// </summary>
        [JsonProperty("currently_active")]
        public bool CurrentlyActive { get; }
    }
}
