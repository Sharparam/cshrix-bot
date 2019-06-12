// <copyright file="RedactionContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains the contents of a redaction event (<c>m.room.redaction</c>).
    /// </summary>
    public sealed class RedactionContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedactionContent" /> class.
        /// </summary>
        /// <param name="reason">The reason for redacting the event.</param>
        public RedactionContent(string reason) => Reason = reason;

        /// <summary>
        /// Gets the reason the target event was redacted.
        /// </summary>
        [JsonProperty("reason")]
        [CanBeNull]
        public string Reason { get; }
    }
}
