// <copyright file="NotificationPushRuleKind.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Notifications
{
    using System.Runtime.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Available push notification rule kinds.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NotificationPushRuleKind
    {
        /// <summary>
        /// The highest priority kind of rule.
        /// </summary>
        [EnumMember(Value = "override")]
        Override,

        /// <summary>
        /// Rules operating on message contents that match certain patterns.
        /// </summary>
        [EnumMember(Value = "content")]
        Content,

        /// <summary>
        /// Rules operating on all messages in a specific room.
        /// </summary>
        [EnumMember(Value = "room")]
        Room,

        /// <summary>
        /// Rules operating on all messages from a specific sender.
        /// </summary>
        [EnumMember(Value = "sender")]
        Sender,

        /// <summary>
        /// Identical to <see cref="Override" /> rules except they are applied last, after all other kinds of rules.
        /// </summary>
        [EnumMember(Value = "underride")]
        Underride
    }
}
