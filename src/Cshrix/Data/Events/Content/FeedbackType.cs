// <copyright file="FeedbackType.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System.Runtime.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Available feedback types for an event.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FeedbackType
    {
        /// <summary>
        /// The event has been delivered.
        /// </summary>
        [EnumMember(Value = "delivered")]
        Delivered,

        /// <summary>
        /// The event has been read/observed.
        /// </summary>
        [EnumMember(Value = "read")]
        Read
    }
}
