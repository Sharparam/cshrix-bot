// <copyright file="PublicRoomsChunk.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains a list of public rooms on a server, along with pagination data.
    /// </summary>
    public sealed class PublicRoomsChunk : Chunk<PublicRoom>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicRoomsChunk" /> class.
        /// </summary>
        /// <param name="data">A collection of public rooms.</param>
        /// <param name="nextBatch">A token for the next batch of rooms, if any.</param>
        /// <param name="previousBatch">A token for the previous batch of rooms, if any.</param>
        /// <param name="estimatedCount">An estimated total count of public rooms.</param>
        public PublicRoomsChunk(
            IReadOnlyCollection<PublicRoom> data,
            [CanBeNull] string nextBatch,
            [CanBeNull] string previousBatch,
            int? estimatedCount)
            : base(data)
        {
            NextBatch = nextBatch;
            PreviousBatch = previousBatch;
            EstimatedCount = estimatedCount;
        }

        /// <summary>
        /// Gets a pagination token for getting the next chunk of rooms.
        /// </summary>
        /// <remarks>
        /// The absence of this token means there are no more results to fetch and the client should stop paginating.
        /// </remarks>
        [JsonProperty("next_batch")]
        [CanBeNull]
        public string NextBatch { get; }

        /// <summary>
        /// Gets a pagination token for getting the previous chunk of rooms.
        /// </summary>
        /// <remarks>
        /// The absence of this token means there are no results before this batch, i.e. this is the first batch.
        /// </remarks>
        [JsonProperty("prev_batch")]
        [CanBeNull]
        public string PreviousBatch { get; }

        /// <summary>
        /// Gets an estimated count on the total number of public rooms, if the server has an estimate.
        /// </summary>
        [JsonProperty("total_room_count_estimate")]
        public int? EstimatedCount { get; }
    }
}
