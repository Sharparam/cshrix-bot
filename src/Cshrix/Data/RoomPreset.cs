// <copyright file="RoomPreset.cs">
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
    /// Identifies a preset configuration when creating rooms.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RoomPreset
    {
        /// <summary>
        /// The room is available for anyone to join, all history is available to joined users.
        /// Guests users are not allowed to join.
        /// </summary>
        [EnumMember(Value = "public_chat")]
        PublicChat,

        /// <summary>
        /// The room is invite only, all history is available to joined users. Guest users are allowed to join.
        /// </summary>
        [EnumMember(Value = "private_chat")]
        PrivateChat,

        /// <summary>
        /// The room is invite only, all history is available to joined users. Guest users are allowed to join.
        /// Additionally, all invitees are given the same power level as the room creator.
        /// </summary>
        [EnumMember(Value = "trusted_private_chat")]
        TrustedPrivateChat
    }
}
