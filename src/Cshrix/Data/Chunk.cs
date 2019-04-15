// <copyright file="Chunk.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;

    using Events;

    using Newtonsoft.Json;

    public class Chunk<T>
    {
        [JsonConstructor]
        public Chunk(IReadOnlyCollection<T> data) => Data = data;

        [JsonProperty("chunk")]
        public IReadOnlyCollection<T> Data { get; }
    }
}
