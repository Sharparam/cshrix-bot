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

    public class CallAnswerContent : CallContent
    {
        public CallAnswerContent(string callId, int version, Answer answer)
            : base(callId, version) =>
            Answer = answer;

        [JsonProperty("answer")]
        public Answer Answer { get; }
    }
}