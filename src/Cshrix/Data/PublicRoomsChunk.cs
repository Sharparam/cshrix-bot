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

    using Newtonsoft.Json;

    public class PublicRoomsChunk : Chunk<PublicRoom>
    {
        public PublicRoomsChunk(
            IReadOnlyCollection<PublicRoom> data,
            string nextBatch,
            string previousBatch,
            int estimatedCount)
            : base(data)
        {
            NextBatch = nextBatch;
            PreviousBatch = previousBatch;
            EstimatedCount = estimatedCount;
        }

        [JsonProperty("next_batch")]
        public string NextBatch { get; }

        [JsonProperty("prev_batch")]
        public string PreviousBatch { get; }

        [JsonProperty("total_room_count_estimate")]
        public int EstimatedCount { get; }
    }
}
