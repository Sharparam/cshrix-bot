// <copyright file="NotificationPushRule.cs">
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
    /// Contains information about a push notification rule.
    /// </summary>
    public readonly struct NotificationPushRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationPushRule" /> structure.
        /// </summary>
        /// <param name="actions">A collection of actions to perform when the rule is matched.</param>
        /// <param name="isDefault">Whether the rule is a default rule.</param>
        /// <param name="isEnabled">Whether the rule is enabled.</param>
        /// <param name="id">The ID of the rule.</param>
        /// <param name="conditions">Conditions that must be met for the rule to take effect.</param>
        /// <param name="pattern">A glob-style pattern to match against for <c>content</c> rules.</param>
        [JsonConstructor]
        public NotificationPushRule(
            IReadOnlyCollection<NotificationAction> actions,
            bool isDefault,
            bool isEnabled,
            string id,
            [CanBeNull] IReadOnlyCollection<NotificationPushCondition> conditions,
            [CanBeNull] string pattern)
            : this()
        {
            Actions = actions;
            IsDefault = isDefault;
            IsEnabled = isEnabled;
            Id = id;
            Conditions = conditions;
            Pattern = pattern;
        }

        /// <summary>
        /// Gets a collection of actions to perform when this rule is matched.
        /// </summary>
        [JsonProperty("actions")]
        public IReadOnlyCollection<NotificationAction> Actions { get; }

        /// <summary>
        /// Gets a value indicating whether this is a default rule, or has been set explicitly.
        /// </summary>
        [JsonProperty("default")]
        public bool IsDefault { get; }

        /// <summary>
        /// Gets a value indicating whether this rule is enabled.
        /// </summary>
        [JsonProperty("enabled")]
        public bool IsEnabled { get; }

        /// <summary>
        /// Gets the ID of this rule.
        /// </summary>
        [JsonProperty("rule_id")]
        public string Id { get; }

        /// <summary>
        /// Gets a collection containing conditions that must be met for this rule to be applied to an event.
        /// </summary>
        /// <remarks>
        /// A rule with no conditions always matches. Only applicable to <c>underride</c> and <c>override</c> rules.
        /// </remarks>
        [JsonProperty("conditions")]
        [CanBeNull]
        public IReadOnlyCollection<NotificationPushCondition> Conditions { get; }

        /// <summary>
        /// Gets a glob-style pattern to match against. Only applicable to <c>content</c> rules.
        /// </summary>
        [JsonProperty("pattern")]
        [CanBeNull]
        public string Pattern { get; }
    }
}
