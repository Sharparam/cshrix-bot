// <copyright file="AliasesContent.cs">
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
    /// Contains the content for an event of type <c>m.room.aliases</c>.
    /// </summary>
    public sealed class AliasesContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AliasesContent" /> class.
        /// </summary>
        /// <param name="aliases">Configured room aliases.</param>
        public AliasesContent(IReadOnlyCollection<RoomAlias> aliases) => Aliases = aliases;

        /// <summary>
        /// Gets all configured aliases for the room.
        /// </summary>
        [JsonProperty("aliases")]
        public IReadOnlyCollection<RoomAlias> Aliases { get; }
    }
}
