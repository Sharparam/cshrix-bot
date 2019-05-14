// <copyright file="SearchGroupKey.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Search
{
    using System.Runtime.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Keys that are available to group by in search results.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SearchGroupKey
    {
        /// <summary>
        /// Group by room ID.
        /// </summary>
        [EnumMember(Value = "room_id")]
        RoomId,

        /// <summary>
        /// Group by sender.
        /// </summary>
        [EnumMember(Value = "sender")]
        Sender
    }
}
