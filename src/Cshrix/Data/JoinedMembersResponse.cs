// <copyright file="JoinedMembersResponse.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about users joined to a room.
    /// </summary>
    public readonly struct JoinedMembersResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinedMembersResponse" /> structure.
        /// </summary>
        /// <param name="joined">A dictionary mapping user IDs to their profile information.</param>
        [JsonConstructor]
        public JoinedMembersResponse(IReadOnlyDictionary<UserId, Profile> joined)
            : this() =>
            Joined = joined;

        /// <summary>
        /// Gets a dictionary mapping user IDs to their profile information.
        /// </summary>
        [JsonProperty("joined")]
        public IReadOnlyDictionary<UserId, Profile> Joined { get; }
    }
}
