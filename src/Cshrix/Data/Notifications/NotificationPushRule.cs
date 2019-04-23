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

    public readonly struct NotificationPushRule
    {
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

        [JsonProperty("actions")]
        public IReadOnlyCollection<NotificationAction> Actions { get; }

        [JsonProperty("default")]
        public bool IsDefault { get; }

        [JsonProperty("enabled")]
        public bool IsEnabled { get; }

        [JsonProperty("rule_id")]
        public string Id { get; }

        [JsonProperty("conditions")]
        [CanBeNull]
        public IReadOnlyCollection<NotificationPushCondition> Conditions { get; }

        [JsonProperty("pattern")]
        [CanBeNull]
        public string Pattern { get; }
    }
}
