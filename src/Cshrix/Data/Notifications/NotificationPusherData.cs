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

    /// <summary>
    /// Contains information for a notification pusher implementation.
    /// </summary>
    public struct NotificationPusherData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationPusherData" /> structure.
        /// </summary>
        /// <param name="uri">A URI to send notifications to.</param>
        /// <param name="format">A format to use when sending notifications to a push gateway.</param>
        [JsonConstructor]
        public NotificationPusherData([CanBeNull] Uri uri = null, [CanBeNull] string format = null)
            : this()
        {
            Uri = uri;
            Format = format;
        }

        /// <summary>
        /// Gets the URI to send notifications to.
        /// </summary>
        /// <remarks>
        /// Must be supplied if <see cref="NotificationPusher.Kind" /> is <c>http</c>.
        /// </remarks>
        [JsonProperty(
            "url",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public Uri Uri { get; }

        /// <summary>
        /// Gets the format to use when sending notifications to the push gateway.
        /// </summary>
        [JsonProperty(
            "format",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Format { get; }
    }
}
