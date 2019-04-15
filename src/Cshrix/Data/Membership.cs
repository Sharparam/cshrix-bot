// <copyright file="Membership.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Runtime.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Membership
    {
        [EnumMember(Value = "invite")]
        Invited,

        [EnumMember(Value = "join")]
        Joined,

        [EnumMember(Value = "knock")]
        Knock,

        [EnumMember(Value = "leave")]
        Left,

        [EnumMember(Value = "ban")]
        Banned
    }
}
