// <copyright file="SearchContextConfiguration.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Search
{
    using System.ComponentModel;

    using Newtonsoft.Json;

    public readonly struct SearchContextConfiguration
    {
        public const int DefaultLimit = 5;

        [JsonConstructor]
        public SearchContextConfiguration(
            int limitBefore = DefaultLimit,
            int limitAfter = DefaultLimit,
            bool includeProfile = false)
            : this()
        {
            LimitBefore = limitBefore;
            LimitAfter = limitAfter;
            IncludeProfile = includeProfile;
        }

        [DefaultValue(DefaultLimit)]
        [JsonProperty("before_limit")]
        public int LimitBefore { get; }

        [DefaultValue(DefaultLimit)]
        [JsonProperty("after_limit")]
        public int LimitAfter { get; }

        [DefaultValue(false)]
        [JsonProperty("include_profile")]
        public bool IncludeProfile { get; }
    }
}
