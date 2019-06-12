// <copyright file="CriteriaOrdering.cs">
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
    /// Specifies a sort order.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SearchOrdering
    {
        /// <summary>
        /// Sort results by relevancy.
        /// </summary>
        [EnumMember(Value = "rank")]
        Rank,

        /// <summary>
        /// Sort results with most recent first.
        /// </summary>
        [EnumMember(Value = "recent")]
        Recent
    }
}
