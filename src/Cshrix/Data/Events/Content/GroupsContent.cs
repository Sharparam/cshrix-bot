// <copyright file="GroupsContent.cs">
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
    /// Content for the <c>m.room.related_groups</c> event.
    /// </summary>
    public sealed class GroupsContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupsContent" /> class.
        /// </summary>
        /// <param name="groups">A collection of related group IDs.</param>
        public GroupsContent(IReadOnlyCollection<GroupId> groups) => Groups = groups;

        /// <summary>
        /// Gets a collection of group IDs that are related to the room.
        /// </summary>
        [JsonProperty("groups")]
        public IReadOnlyCollection<GroupId> Groups { get; }
    }
}
