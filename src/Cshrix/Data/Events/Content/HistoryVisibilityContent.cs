// <copyright file="HistoryVisibilityContent.cs">
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
    /// Contains data for the <c>m.room.history_visibility</c> event.
    /// </summary>
    public sealed class HistoryVisibilityContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryVisibilityContent" /> class.
        /// </summary>
        /// <param name="visibility">The history visibility of the room.</param>
        public HistoryVisibilityContent(HistoryVisibility visibility) => Visibility = visibility;

        /// <summary>
        /// Gets the current history visibility of the room.
        /// </summary>
        [JsonProperty("history_visibility")]
        public HistoryVisibility Visibility { get; }
    }
}
