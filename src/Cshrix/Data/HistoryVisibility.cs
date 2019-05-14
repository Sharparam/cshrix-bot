// <copyright file="HistoryVisibility.cs">
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
    /// Different history visibility states for a room.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HistoryVisibility
    {
        /// <summary>
        /// Anyone can read history, even non-joined users.
        /// </summary>
        [EnumMember(Value = "world_readable")]
        WorldReadable,

        /// <summary>
        /// All history is available to joined users.
        /// </summary>
        [EnumMember(Value = "shared")]
        Shared,

        /// <summary>
        /// History is available to joined users from the point in time they were invited.
        /// </summary>
        [EnumMember(Value = "invited")]
        Invited,

        /// <summary>
        /// History is available to joined users from the point in time they joined.
        /// </summary>
        [EnumMember(Value = "joined")]
        Joined
    }
}
