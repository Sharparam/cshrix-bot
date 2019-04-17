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

    public readonly struct Reason
    {
        [JsonConstructor]
        public Reason(Identifier userId, string description = null)
            : this()
        {
            Description = description;
            UserId = userId;
        }

        [JsonProperty(
            "reason",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Description { get; }

        [JsonProperty("user_id")]
        public Identifier UserId { get; }
    }
}
