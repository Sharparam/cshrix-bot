// <copyright file="TypingContent.cs">
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

    public class TypingContent : EventContent
    {
        public TypingContent(IReadOnlyCollection<Identifier> userIds) => UserIds = userIds;

        [JsonProperty("user_ids")]
        public IReadOnlyCollection<Identifier> UserIds { get; }
    }
}