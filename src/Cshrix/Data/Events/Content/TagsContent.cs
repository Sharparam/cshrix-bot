// <copyright file="TagsContent.cs">
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
    /// Contains tag definitions for a room (<c>m.tag</c>).
    /// </summary>
    public sealed class TagsContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagsContent" /> class.
        /// </summary>
        /// <param name="tags">Dictionary of tag names to tag metadata.</param>
        public TagsContent(IReadOnlyDictionary<string, TagData> tags) => Tags = tags;

        /// <summary>
        /// Gets a dictionary mapping tag names to their associated metadata.
        /// </summary>
        [JsonProperty("tags")]
        public IReadOnlyDictionary<string, TagData> Tags { get; }
    }
}
