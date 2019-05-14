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

    /// <summary>
    /// Specifies what kind of context should be included when searching events.
    /// </summary>
    public readonly struct SearchContextConfiguration
    {
        /// <summary>
        /// The default context limit.
        /// </summary>
        private const int DefaultLimit = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchContextConfiguration" /> structure.
        /// </summary>
        /// <param name="limitBefore">Maximum number of context events before the search results.</param>
        /// <param name="limitAfter">Maximum number of context events after the search results.</param>
        /// <param name="includeProfile">Whether to include profile information of users.</param>
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

        /// <summary>
        /// Gets the maximum number of context events to include before the search results.
        /// </summary>
        [DefaultValue(DefaultLimit)]
        [JsonProperty("before_limit")]
        public int LimitBefore { get; }

        /// <summary>
        /// Gets the maximum number of context events to include after the search results.
        /// </summary>
        [DefaultValue(DefaultLimit)]
        [JsonProperty("after_limit")]
        public int LimitAfter { get; }

        /// <summary>
        /// Gets a value indicating whether profile information should be included for users.
        /// </summary>
        [DefaultValue(false)]
        [JsonProperty("include_profile")]
        public bool IncludeProfile { get; }
    }
}
