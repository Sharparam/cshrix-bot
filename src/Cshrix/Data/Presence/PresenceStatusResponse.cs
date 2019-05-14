// <copyright file="PresenceStatusResponse.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Presence
{
    using System;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// Describes the presence state of a user.
    /// </summary>
    public readonly struct PresenceStatusResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PresenceStatusResponse" /> structure.
        /// </summary>
        /// <param name="presence">The current presence of the user.</param>
        /// <param name="lastActiveAgo">The amount of time since the last activity.</param>
        /// <param name="statusMessage">Optional state message for the user.</param>
        /// <param name="isCurrentlyActive">Whether the user is currently active.</param>
        [JsonConstructor]
        public PresenceStatusResponse(
            Presence presence,
            TimeSpan? lastActiveAgo,
            [CanBeNull] string statusMessage,
            bool isCurrentlyActive)
            : this()
        {
            Presence = presence;
            LastActiveAgo = lastActiveAgo;
            StatusMessage = statusMessage;
            IsCurrentlyActive = isCurrentlyActive;
        }

        /// <summary>
        /// Gets the current presence of the user.
        /// </summary>
        [JsonProperty("presence")]
        public Presence Presence { get; }

        /// <summary>
        /// Gets the amount of time since the user was last active.
        /// </summary>
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        [JsonProperty("last_active_ago")]
        public TimeSpan? LastActiveAgo { get; }

        /// <summary>
        /// Gets the state message for this user, or <c>null</c> if not set.
        /// </summary>
        [JsonProperty("status_msg")]
        [CanBeNull]
        public string StatusMessage { get; }

        /// <summary>
        /// Gets a value indicating whether the user is currently active.
        /// </summary>
        [JsonProperty("currently_active")]
        public bool IsCurrentlyActive { get; }
    }
}
