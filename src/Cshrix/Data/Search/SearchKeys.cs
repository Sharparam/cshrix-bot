// <copyright file="CriteriaKeys.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Search
{
    using System;
    using System.Runtime.Serialization;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// Different keys that can be searched against in events.
    /// </summary>
    [Flags]
    [JsonConverter(typeof(StringArrayFlagsEnumConverter<SearchKeys>))]
    public enum SearchKeys
    {
        /// <summary>
        /// Don't search any keys.
        /// </summary>
        None = 0,

        /// <summary>
        /// Search in the body of the event content.
        /// </summary>
        [EnumMember(Value = "content.body")]
        ContentBody = 1,

        /// <summary>
        /// Search in the name of the event content.
        /// </summary>
        [EnumMember(Value = "content.name")]
        ContentName = 1 << 1,

        /// <summary>
        /// Search in the topic of the content.
        /// </summary>
        [EnumMember(Value = "content.topic")]
        ContentTopic = 1 << 2,

        /// <summary>
        /// Search all fields.
        /// </summary>
        All = ContentBody | ContentName | ContentTopic
    }
}
