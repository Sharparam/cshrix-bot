// <copyright file="NotificationPusher.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Notifications
{
    using System.ComponentModel;
    using System.Globalization;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct NotificationPusher
    {
        [JsonConstructor]
        public NotificationPusher(
            string pushKey,
            string kind,
            string applicationId,
            string applicationDisplayName,
            string deviceDisplayName,
            [CanBeNull] string profileTag,
            CultureInfo culture,
            NotificationPusherData data,
            bool append = false)
            : this()
        {
            PushKey = pushKey;
            Kind = kind;
            ApplicationId = applicationId;
            ApplicationDisplayName = applicationDisplayName;
            DeviceDisplayName = deviceDisplayName;
            ProfileTag = profileTag;
            Culture = culture;
            Data = data;
            Append = append;
        }

        [JsonProperty("pushkey")]
        public string PushKey { get; }

        [JsonProperty("kind")]
        public string Kind { get; }

        [JsonProperty("app_id")]
        public string ApplicationId { get; }

        [JsonProperty("app_display_name")]
        public string ApplicationDisplayName { get; }

        [JsonProperty("device_display_name")]
        public string DeviceDisplayName { get; }

        [JsonProperty("profile_tag")]
        [CanBeNull]
        public string ProfileTag { get; }

        [JsonProperty("lang")]
        public CultureInfo Culture { get; }

        [JsonProperty("data")]
        public NotificationPusherData Data { get; }

        [DefaultValue(false)]
        [JsonProperty("append")]
        public bool Append { get; }
    }
}
