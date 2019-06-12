// <copyright file="TypingContent.cs">
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
    /// Contains information about users currently typing (for the <c>m.typing</c> event).
    /// </summary>
    public sealed class TypingContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypingContent" /> class.
        /// </summary>
        /// <param name="userIds">A collection of user IDs currently typing.</param>
        public TypingContent(IReadOnlyCollection<UserId> userIds) => UserIds = userIds;

        /// <summary>
        /// Gets a collection of user IDs that are currently typing.
        /// </summary>
        [JsonProperty("user_ids")]
        public IReadOnlyCollection<UserId> UserIds { get; }
    }
}
