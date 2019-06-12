// <copyright file="Candidate.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using Newtonsoft.Json;

    /// <summary>
    /// Describes an ICE candidate.
    /// </summary>
    public readonly struct Candidate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Candidate" /> structure.
        /// </summary>
        /// <param name="sdpMediaId">The SDP media type the candidate is intended for.</param>
        /// <param name="sdpMLineIndex">The index of the SDP <c>m</c> line this candidate is intended for.</param>
        /// <param name="sdpALine">The SDP <c>a</c> line of the candidate.</param>
        [JsonConstructor]
        public Candidate(string sdpMediaId, int sdpMLineIndex, string sdpALine)
            : this()
        {
            SdpMediaId = sdpMediaId;
            SdpMLineIndex = sdpMLineIndex;
            SdpALine = sdpALine;
        }

        /// <summary>
        /// Gets the SDP media type this candidate is intended for.
        /// </summary>
        [JsonProperty("sdpMid")]
        public string SdpMediaId { get; }

        /// <summary>
        /// Gets the index of the SDP <c>m</c> line this candidate is intended for.
        /// </summary>
        [JsonProperty("sdpMLineIndex")]
        public int SdpMLineIndex { get; }

        /// <summary>
        /// Gets the SDP <c>a</c> line of the candidate.
        /// </summary>
        [JsonProperty("candidate")]
        public string SdpALine { get; }
    }
}
