// <copyright file="GuestAccess.cs">
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
    /// Specifies guest access type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GuestAccess
    {
        /// <summary>
        /// Guests are allowed to join.
        /// </summary>
        [EnumMember(Value = "can_join")]
        CanJoin,

        /// <summary>
        /// Guests are not allowed to join.
        /// </summary>
        [EnumMember(Value = "forbidden")]
        Forbidden
    }
}
