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

    /// <summary>
    /// Membership status in a room.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Membership
    {
        /// <summary>
        /// User is invited to the room.
        /// </summary>
        [EnumMember(Value = "invite")]
        Invited,

        /// <summary>
        /// User is joined to the room.
        /// </summary>
        [EnumMember(Value = "join")]
        Joined,

        /// <summary>
        /// Reserved for future use.
        /// </summary>
        [EnumMember(Value = "knock")]
        Knock,

        /// <summary>
        /// User has left the room.
        /// </summary>
        [EnumMember(Value = "leave")]
        Left,

        /// <summary>
        /// User has been banned from the room.
        /// </summary>
        [EnumMember(Value = "ban")]
        Banned
    }
}
