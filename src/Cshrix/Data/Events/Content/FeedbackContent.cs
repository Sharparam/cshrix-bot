// <copyright file="FeedbackContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using Newtonsoft.Json;

    public class FeedbackContent : EventContent
    {
        public FeedbackContent(string targetEventId, FeedbackType type)
        {
            TargetEventId = targetEventId;
            Type = type;
        }

        [JsonProperty("target_event_id")]
        public string TargetEventId { get; }

        [JsonProperty("type")]
        public FeedbackType Type { get; }
    }
}
