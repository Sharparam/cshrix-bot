// <copyright file="RoomVisibility.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Runtime.Serialization;

    using JetBrains.Annotations;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// The visibility of a room in the <c>/publicRooms</c> API.
    /// </summary>
    [PublicAPI]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RoomVisibility
    {
        /// <summary>
        /// The room is visible in the API.
        /// </summary>
        [EnumMember(Value = "public")]
        Public,

        /// <summary>
        /// The room is not visible in the API.
        /// </summary>
        [EnumMember(Value = "private")]
        Private
    }
}
