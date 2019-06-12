// <copyright file="LocationMessageContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// Describes the content of a message with attached location information.
    /// </summary>
    public sealed class LocationMessageContent : MessageContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationMessageContent" /> class.
        /// </summary>
        /// <param name="body">The body text of the message.</param>
        /// <param name="messageType">The type of the message.</param>
        /// <param name="uri">Geo URI for the location.</param>
        /// <param name="info">Additional location information</param>
        public LocationMessageContent(string body, string messageType, Uri uri, LocationInfo info)
            : base(body, messageType)
        {
            Uri = uri;
            Info = info;
        }

        /// <summary>
        /// Gets the Geo URI for the location.
        /// </summary>
        [JsonProperty("geo_uri")]
        public Uri Uri { get; }

        /// <summary>
        /// Gets additional information about the location (thumbnail).
        /// </summary>
        [JsonProperty("info")]
        public LocationInfo Info { get; }
    }
}
