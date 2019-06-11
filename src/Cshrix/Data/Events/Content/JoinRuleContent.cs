// <copyright file="JoinRuleContent.cs">
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
    /// Contains data for the <c>m.room.join_rules</c> event.
    /// </summary>
    public sealed class JoinRuleContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JoinRuleContent" /> class.
        /// </summary>
        /// <param name="rule">The current join rule set on the room.</param>
        public JoinRuleContent(JoinRule rule) => Rule = rule;

        /// <summary>
        /// Gets the <see cref="JoinRule" /> configured for the room.
        /// </summary>
        [JsonProperty("join_rule")]
        public JoinRule Rule { get; }
    }
}
