// <copyright file="CanonicalAliasContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Content for the <c>m.room.canonical_alias</c> event.
    /// </summary>
    public sealed class CanonicalAliasContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanonicalAliasContent" /> class.
        /// </summary>
        /// <param name="alias">The canonical alias for the room.</param>
        public CanonicalAliasContent([CanBeNull] RoomAlias alias) => Alias = alias;

        /// <summary>
        /// Gets the canonical alias for the room, if one is set.
        /// </summary>
        [JsonProperty("alias")]
        [CanBeNull]
        public RoomAlias Alias { get; }
    }
}
