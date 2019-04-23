// <copyright file="NotificationPushCondition.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Notifications
{
    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Serialization;

    public readonly struct NotificationPushCondition
    {
        [JsonConstructor]
        public NotificationPushCondition(
            string kind,
            [CanBeNull] string key,
            [CanBeNull] string pattern,
            ComparisonCondition<int>? condition)
            : this()
        {
            Kind = kind;
            Key = key;
            Pattern = pattern;
            Condition = condition;
        }

        [JsonProperty("kind")]
        public string Kind { get; }

        [JsonProperty("key")]
        [CanBeNull]
        public string Key { get; }

        [JsonProperty("pattern")]
        [CanBeNull]
        public string Pattern { get; }

        [JsonConverter(typeof(ComparisonConditionConverter<int>))]
        [JsonProperty("is")]
        public ComparisonCondition<int>? Condition { get; }
    }
}
