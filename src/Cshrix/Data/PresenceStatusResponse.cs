// <copyright file="PresenceStatusResponse.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Serialization;

    public readonly struct PresenceStatusResponse
    {
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

        [JsonProperty("presence")]
        public Presence Presence { get; }

        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        [JsonProperty("last_active_ago")]
        public TimeSpan? LastActiveAgo { get; }

        [JsonProperty("status_msg")]
        [CanBeNull]
        public string StatusMessage { get; }

        [JsonProperty("currently_active")]
        public bool IsCurrentlyActive { get; }
    }
}
