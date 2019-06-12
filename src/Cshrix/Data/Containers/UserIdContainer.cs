// <copyright file="UserIdContainer.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Newtonsoft.Json;

    /// <summary>
    /// A wrapper containing a <see cref="UserId" />.
    /// </summary>
    public readonly struct UserIdContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserIdContainer" /> structure.
        /// </summary>
        /// <param name="userId">The <see cref="UserId" /> object.</param>
        [JsonConstructor]
        public UserIdContainer(UserId userId)
            : this() =>
            UserId = userId;

        /// <summary>
        /// The wrapped <see cref="UserId" /> object.
        /// </summary>
        [JsonProperty("user_id")]
        public UserId UserId { get; }
    }
}
