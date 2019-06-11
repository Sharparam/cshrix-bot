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

    /// <summary>
    /// Contains the contents of a feedback event (<c>m.room.message.feedback</c>).
    /// </summary>
    /// <remarks>
    /// <strong>
    /// NB: Usage of this event is discouraged in favour of the receipts module.
    /// Most clients will not recognise this event.
    /// </strong>
    /// </remarks>
    public sealed class FeedbackContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackContent" /> class.
        /// </summary>
        /// <param name="targetEventId">The ID of the event the feedback relates to.</param>
        /// <param name="type">The type of feedback.</param>
        public FeedbackContent(string targetEventId, FeedbackType type)
        {
            TargetEventId = targetEventId;
            Type = type;
        }

        /// <summary>
        /// Gets the ID of the event that the feedback is related to.
        /// </summary>
        [JsonProperty("target_event_id")]
        public string TargetEventId { get; }

        /// <summary>
        /// Gets the type of feedback.
        /// </summary>
        [JsonProperty("type")]
        public FeedbackType Type { get; }
    }
}
