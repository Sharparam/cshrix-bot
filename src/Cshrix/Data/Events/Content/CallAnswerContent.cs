// <copyright file="CallAnswerContent.cs">
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
    /// Contains data for the <c>m.call.answer</c> event.
    /// </summary>
    public sealed class CallAnswerContent : CallContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallAnswerContent" /> class.
        /// </summary>
        /// <param name="callId">The ID of the call.</param>
        /// <param name="version">Call software version.</param>
        /// <param name="answer">The session description object.</param>
        public CallAnswerContent(string callId, int version, Answer answer)
            : base(callId, version) =>
            Answer = answer;

        /// <summary>
        /// Gets an object describing the call session.
        /// </summary>
        [JsonProperty("answer")]
        public Answer Answer { get; }
    }
}
