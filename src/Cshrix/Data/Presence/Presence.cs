// <copyright file="Presence.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Presence
{
    using System.Runtime.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// User presence.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Presence
    {
        /// <summary>
        /// Unavailable for unknown reasons.
        /// </summary>
        [EnumMember(Value = "unavailable")]
        Unavailable,

        /// <summary>
        /// User is currently online.
        /// </summary>
        [EnumMember(Value = "online")]
        Online,

        /// <summary>
        /// User is offline.
        /// </summary>
        [EnumMember(Value = "offline")]
        Offline
    }
}
