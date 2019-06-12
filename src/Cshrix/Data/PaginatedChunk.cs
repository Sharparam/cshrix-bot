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

    /// <summary>
    /// A paginated chunk of data.
    /// </summary>
    /// <typeparam name="T">The type of data in the chunk.</typeparam>
    public sealed class PaginatedChunk<T> : Chunk<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedChunk{T}" /> class.
        /// </summary>
        /// <param name="data">The data contained in the chunk page.</param>
        /// <param name="start">A token for the start of the pagination.</param>
        /// <param name="end">A token for the end of the pagination.</param>
        public PaginatedChunk(IReadOnlyCollection<T> data, string start, string end)
            : base(data)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Gets a token indicating the start of the pagination.
        /// </summary>
        [JsonProperty("start")]
        public string Start { get; }

        /// <summary>
        /// Gets a token indicating the end of the pagination.
        /// </summary>
        [JsonProperty("end")]
        public string End { get; }
    }
}
