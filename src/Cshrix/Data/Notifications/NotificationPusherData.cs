// <copyright file="NotificationPusherData.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Notifications
{
    using System;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public struct NotificationPusherData
    {
        [JsonConstructor]
        public NotificationPusherData([CanBeNull] Uri uri = null, [CanBeNull] string format = null)
            : this()
        {
            Uri = uri;
            Format = format;
        }

        [JsonProperty(
            "url",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public Uri Uri { get; }

        [JsonProperty(
            "format",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Format { get; }
    }
}
