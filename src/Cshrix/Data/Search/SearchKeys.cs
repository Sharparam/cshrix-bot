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

    [Flags]
    [JsonConverter(typeof(StringArrayFlagsEnumConverter<SearchKeys>))]
    public enum SearchKeys
    {
        None = 0,

        [EnumMember(Value = "content.body")]
        ContentBody = 1,

        [EnumMember(Value = "content.name")]
        ContentName = 1 << 1,

        [EnumMember(Value = "content.topic")]
        ContentTopic = 1 << 2,

        All = ContentBody | ContentName | ContentTopic
    }
}
