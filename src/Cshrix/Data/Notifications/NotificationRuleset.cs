// <copyright file="NotificationRuleset.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Notifications
{
    using System.Collections.Generic;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct NotificationRuleset
    {
        [JsonConstructor]
        public NotificationRuleset(
            [CanBeNull] IReadOnlyCollection<NotificationPushRule> content,
            [CanBeNull] IReadOnlyCollection<NotificationPushRule> @override,
            [CanBeNull] IReadOnlyCollection<NotificationPushRule> room,
            [CanBeNull] IReadOnlyCollection<NotificationPushRule> sender,
            [CanBeNull] IReadOnlyCollection<NotificationPushRule> underride)
            : this()
        {
            Content = content;
            Override = @override;
            Room = room;
            Sender = sender;
            Underride = underride;
        }

        [JsonProperty(
            "content",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<NotificationPushRule> Content { get; }

        [JsonProperty(
            "override",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<NotificationPushRule> Override { get; }

        [JsonProperty(
            "room",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<NotificationPushRule> Room { get; }

        [JsonProperty(
            "sender",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<NotificationPushRule> Sender { get; }

        [JsonProperty(
            "underride",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<NotificationPushRule> Underride { get; }
    }
}
