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

    public sealed class TagsContent : EventContent
    {
        public TagsContent(IReadOnlyDictionary<string, TagData> tags) => Tags = tags;

        [JsonProperty("tags")]
        public IReadOnlyDictionary<string, TagData> Tags { get; }
    }
}
