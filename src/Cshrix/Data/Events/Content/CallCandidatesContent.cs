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

    public sealed class CallCandidatesContent : CallContent
    {
        public CallCandidatesContent(string callId, int version, IReadOnlyCollection<Candidate> candidates)
            : base(callId, version) =>
            Candidates = candidates;

        [JsonProperty("candidates")]
        public IReadOnlyCollection<Candidate> Candidates { get; }
    }
}
