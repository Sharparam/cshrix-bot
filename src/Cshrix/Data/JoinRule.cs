// <copyright file="JoinRule.cs">
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

    /// <summary>
    /// Join rule for a room.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum JoinRule
    {
        /// <summary>
        /// Room can be joined by anyone.
        /// </summary>
        [EnumMember(Value = "public")]
        Public,

        /// <summary>
        /// Reserved for future use.
        /// </summary>
        [EnumMember(Value = "knock")]
        Knock,

        /// <summary>
        /// Users must be invited to the room.
        /// </summary>
        [EnumMember(Value = "invite")]
        Invite,

        /// <summary>
        /// Reserved for future use.
        /// </summary>
        [EnumMember(Value = "private")]
        Private
    }
}
