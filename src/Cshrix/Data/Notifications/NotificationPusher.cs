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

    /// <summary>
    /// Describes a notification pusher.
    /// </summary>
    public readonly struct NotificationPusher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationPusher" /> structure.
        /// </summary>
        /// <param name="pushKey">The unique identifier for the pusher.</param>
        /// <param name="kind">The kind of the pusher.</param>
        /// <param name="applicationId">The reverse-DNS style identifier for the application.</param>
        /// <param name="applicationDisplayName">The display name of the application.</param>
        /// <param name="deviceDisplayName">The display name of the device owning the pusher.</param>
        /// <param name="profileTag">
        /// The string determining a set of device-specific rules for the pusher to execute.
        /// </param>
        /// <param name="culture">Preferred culture for receiving events.</param>
        /// <param name="data">Information for the pusher implementation.</param>
        /// <param name="append">
        /// Whether to append this pusher or overwrite existing ones sharing the same <see cref="PushKey" /> and
        /// <see cref="ApplicationId" />.
        /// </param>
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

        /// <summary>
        /// Gets a unique identifier for this pusher.
        /// </summary>
        [JsonProperty("pushkey")]
        public string PushKey { get; }

        /// <summary>
        /// Gets the kind of this pusher.
        /// </summary>
        /// <remarks><c>http</c> is a pusher that sends HTTP pokes, for example.</remarks>
        [JsonProperty("kind")]
        public string Kind { get; }

        /// <summary>
        /// Gets a reverse-DNS style identifier for the application.
        /// </summary>
        /// <example><c>com.sharparam.custompusher</c></example>
        [JsonProperty("app_id")]
        public string ApplicationId { get; }

        /// <summary>
        /// Gets a string that will allow the user to identify what application owns this pusher.
        /// </summary>
        [JsonProperty("app_display_name")]
        public string ApplicationDisplayName { get; }

        /// <summary>
        /// Gets a string that will allow the user to identify what device owns this pusher.
        /// </summary>
        [JsonProperty("device_display_name")]
        public string DeviceDisplayName { get; }

        /// <summary>
        /// Gets a string that determines which set of device-specific rules this pusher executes.
        /// </summary>
        [JsonProperty("profile_tag")]
        [CanBeNull]
        public string ProfileTag { get; }

        /// <summary>
        /// Gets the preferred culture for receiving notifications, e.g. <c>en-US</c>.
        /// </summary>
        [JsonProperty("lang")]
        public CultureInfo Culture { get; }

        /// <summary>
        /// Gets an object containing information for the pusher implementation itself.
        /// </summary>
        [JsonProperty("data")]
        public NotificationPusherData Data { get; }

        /// <summary>
        /// Gets a value indicating whether the homeserver should add another pusher with the given
        /// <see cref="PushKey" /> and <see cref="ApplicationId" /> in addition to any others with different user IDs.
        /// Otherwise, the homeserver must remove any other pushers with the same <see cref="ApplicationId" /> and
        /// <see cref="PushKey" /> for different users.
        /// </summary>
        [DefaultValue(false)]
        [JsonProperty("append")]
        public bool Append { get; }
    }
}
