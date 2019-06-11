// <copyright file="IgnoredUsersContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains data for the <c>m.ignored_user_list</c> account data event.
    /// </summary>
    public sealed class IgnoredUsersContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoredUsersContent" /> class.
        /// </summary>
        /// <param name="users">A mapping of ignored user IDs to metadata.</param>
        public IgnoredUsersContent(IReadOnlyDictionary<UserId, object> users) => Users = users;

        /// <summary>
        /// Gets a dictionary mapping ignored user IDs to an empty object (reserved for future use).
        /// </summary>
        [JsonProperty("ignored_users")]
        public IReadOnlyDictionary<UserId, object> Users { get; }
    }
}
