// <copyright file="TagsContainer.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains tags for a room.
    /// </summary>
    public readonly struct TagsResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagsResponse" /> structure.
        /// </summary>
        /// <param name="tags">A dictionary of tags on the room and their data.</param>
        public TagsResponse(IDictionary<string, TagData> tags)
            : this((IReadOnlyDictionary<string, TagData>)new ReadOnlyDictionary<string, TagData>(tags))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagsResponse" /> structure.
        /// </summary>
        /// <param name="tags">A dictionary of tags on the room and their data.</param>
        [JsonConstructor]
        public TagsResponse(IReadOnlyDictionary<string, TagData> tags)
            : this() =>
            Tags = tags;

        /// <summary>
        /// Gets the tags set on the room. The key is the tag name and the value is additional tag data.
        /// </summary>
        [JsonProperty("tags")]
        public IReadOnlyDictionary<string, TagData> Tags { get; }
    }
}
