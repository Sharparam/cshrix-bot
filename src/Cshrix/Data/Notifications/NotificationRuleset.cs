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

    /// <summary>
    /// Contains information about a push notification ruleset.
    /// </summary>
    public readonly struct NotificationRuleset
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationRuleset" /> structure.
        /// </summary>
        /// <param name="override">Override rules.</param>
        /// <param name="content">Content rules.</param>
        /// <param name="room">Room rules.</param>
        /// <param name="sender">Sender rules.</param>
        /// <param name="underride">Underride rules.</param>
        [JsonConstructor]
        public NotificationRuleset(
            [CanBeNull] IReadOnlyCollection<NotificationPushRule> @override,
            [CanBeNull] IReadOnlyCollection<NotificationPushRule> content,
            [CanBeNull] IReadOnlyCollection<NotificationPushRule> room,
            [CanBeNull] IReadOnlyCollection<NotificationPushRule> sender,
            [CanBeNull] IReadOnlyCollection<NotificationPushRule> underride)
            : this()
        {
            Override = @override;
            Content = content;
            Room = room;
            Sender = sender;
            Underride = underride;
        }

        /// <summary>
        /// Gets a collection of override rules.
        /// </summary>
        /// <remarks>
        /// The highest priority rules are user-configured overrides.
        /// </remarks>
        [JsonProperty(
            "override",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<NotificationPushRule> Override { get; }

        /// <summary>
        /// Gets a collection of content rules.
        /// </summary>
        /// <remarks>
        /// These configure behaviour for (unencrypted) messages that match certain patterns. Content rules take one
        /// parameter: <see cref="NotificationPushRule.Pattern" />, that gives the glob pattern to match against.
        /// This is treated in the same way as <see cref="NotificationPushCondition.Pattern" /> for
        /// <see cref="NotificationPushConditionKind.EventMatch" />.
        /// </remarks>
        [JsonProperty(
            "content",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<NotificationPushRule> Content { get; }

        /// <summary>
        /// Gets a collection of room rules.
        /// </summary>
        /// <remarks>
        /// These rules change the behaviour of all messages for a given room.
        /// The <see cref="NotificationPushRule.Id" /> of a room rule is always the ID of the room that it affects.
        /// </remarks>
        [JsonProperty(
            "room",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<NotificationPushRule> Room { get; }

        /// <summary>
        /// Gets a collection of sender rules.
        /// </summary>
        /// <remarks>
        /// These rules configure notification behaviour for messages from a specific Matrix user ID.
        /// The <see cref="NotificationPushRule.Id" /> of sender rules is always the Matrix user ID of the user whose
        /// messages they'd apply to.
        /// </remarks>
        [JsonProperty(
            "sender",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<NotificationPushRule> Sender { get; }

        /// <summary>
        /// Gets a collection of underride rules.
        /// </summary>
        /// <remarks>
        /// These are identical to <see cref="Override" /> rules, but have a lower priority than <see cref="Content" />,
        /// <see cref="Room" />, and <see cref="Sender" /> rules.
        /// </remarks>
        [JsonProperty(
            "underride",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<NotificationPushRule> Underride { get; }
    }
}
