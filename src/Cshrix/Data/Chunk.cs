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

    using Newtonsoft.Json;

    /// <summary>
    /// A chunk of data.
    /// </summary>
    /// <typeparam name="T">The type of the data.</typeparam>
    public class Chunk<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Chunk{T}" /> class.
        /// </summary>
        /// <param name="data">The data to put in the chunk.</param>
        [JsonConstructor]
        public Chunk(IReadOnlyCollection<T> data) => Data = data;

        /// <summary>
        /// Gets the collection of data in this chunk.
        /// </summary>
        [JsonProperty("chunk")]
        public IReadOnlyCollection<T> Data { get; }
    }
}
