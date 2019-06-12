// <copyright file="ReadMarkers.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Newtonsoft.Json;

    /// <summary>
    /// Specifies which event IDs the read marker and receipt should be set at.
    /// </summary>
    public readonly struct ReadMarkers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMarkers" /> structure.
        /// </summary>
        /// <param name="fullyReadEventId">The ID of the event that the read marker should be at.</param>
        /// <param name="readEventId">The ID of the event that the receipt location should be at.</param>
        [JsonConstructor]
        public ReadMarkers(string fullyReadEventId, string readEventId = null)
            : this()
        {
            FullyReadEventId = fullyReadEventId;
            ReadEventId = readEventId;
        }

        /// <summary>
        /// Gets the ID of the event that the read marker should be located at.
        /// </summary>
        /// <remarks>
        /// The event <em>must</em> belong to the room.
        /// </remarks>
        [JsonProperty("m.fully_read")]
        public string FullyReadEventId { get; }

        /// <summary>
        /// Gets the ID of the event that the read receipt location should be at.
        /// </summary>
        /// <remarks>
        /// This is equivalent to calling <c>/receipt/m.read/$elsewhere:domain.com</c> and is provided here to
        /// save that extra call.
        /// </remarks>
        [JsonProperty("m.read")]
        public string ReadEventId { get; }
    }
}
