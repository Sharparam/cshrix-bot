// <copyright file="PaginatedChunk.cs">
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

    public class PaginatedChunk<T> : Chunk<T>
    {
        public PaginatedChunk(IReadOnlyCollection<T> data, string end, string start)
            : base(data)
        {
            End = end;
            Start = start;
        }

        [JsonProperty("end")]
        public string End { get; }

        [JsonProperty("start")]
        public string Start { get; }
    }
}
