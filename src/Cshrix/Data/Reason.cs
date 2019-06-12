// <copyright file="Reason.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains a reason for a kick or ban.
    /// </summary>
    public readonly struct Reason
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Reason" /> structure.
        /// </summary>
        /// <param name="userId">The ID of the user affected.</param>
        /// <param name="description">An optional reason description.</param>
        [JsonConstructor]
        public Reason(UserId userId, string description = null)
            : this()
        {
            Description = description;
            UserId = userId;
        }

        /// <summary>
        /// Gets the ID of the user whom this reason (kick or ban) applies to.
        /// </summary>
        [JsonProperty("user_id")]
        public UserId UserId { get; }

        /// <summary>
        /// Gets a description of the reason.
        /// </summary>
        [JsonProperty(
            "reason",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Description { get; }
    }
}
