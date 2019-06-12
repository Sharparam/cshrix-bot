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

    /// <summary>
    /// A condition for a push notification rule.
    /// </summary>
    public readonly struct NotificationPushCondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationPushCondition" /> structure.
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="key"></param>
        /// <param name="pattern"></param>
        /// <param name="condition"></param>
        [JsonConstructor]
        public NotificationPushCondition(
            NotificationPushConditionKind kind,
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

        /// <summary>
        /// Gets the kind of this condition.
        /// </summary>
        [JsonProperty("kind")]
        public NotificationPushConditionKind Kind { get; }

        /// <summary>
        /// Gets the dot-separated field of the event to match.
        /// </summary>
        /// <remarks>
        /// Required if <see cref="Kind" /> is <see cref="NotificationPushConditionKind.EventMatch" />.
        /// </remarks>
        [JsonProperty("key")]
        [CanBeNull]
        public string Key { get; }

        /// <summary>
        /// Gets the glob-style pattern to match against.
        /// </summary>
        /// <remarks>
        /// Required if <see cref="Kind" /> is <see cref="NotificationPushConditionKind.EventMatch" />.
        /// Patterns with no special glob characters should be treated as having asterisks prepended and appended
        /// when testing the condition.
        /// </remarks>
        [JsonProperty("pattern")]
        [CanBeNull]
        public string Pattern { get; }

        /// <summary>
        /// Gets a comparison condition to apply.
        /// </summary>
        /// <remarks>
        /// Required if <see cref="Kind" /> is <see cref="NotificationPushConditionKind.RoomMemberCount" />.
        /// The comparer specifies under what conditions the push condition will trigger.
        /// </remarks>
        [JsonConverter(typeof(ComparisonConditionConverter<int>))]
        [JsonProperty("is")]
        public ComparisonCondition<int>? Condition { get; }
    }
}
