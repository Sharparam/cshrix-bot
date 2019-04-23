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

    [JsonConverter(typeof(StringEnumConverter))]
    public enum NotificationPushRuleKind
    {
        [EnumMember(Value = "override")]
        Override,

        [EnumMember(Value = "underride")]
        Underride,

        [EnumMember(Value = "sender")]
        Sender,

        [EnumMember(Value = "room")]
        Room,

        [EnumMember(Value = "content")]
        Content
    }
}
