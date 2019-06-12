// <copyright file="CallCandidatesContent.cs">
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
    /// Contains data for the <c>m.call.candidates</c> event.
    /// </summary>
    public sealed class CallCandidatesContent : CallContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallCandidatesContent" /> class.
        /// </summary>
        /// <param name="callId">The ID of the call.</param>
        /// <param name="version">The VoIP specification version.</param>
        /// <param name="candidates">The ICE candidates.</param>
        public CallCandidatesContent(string callId, int version, IReadOnlyCollection<Candidate> candidates)
            : base(callId, version) =>
            Candidates = candidates;

        /// <summary>
        /// Gets a collection of objects describing the candidates.
        /// </summary>
        [JsonProperty("candidates")]
        public IReadOnlyCollection<Candidate> Candidates { get; }
    }
}
