// <copyright file="FormattedMessageContent.cs">
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
    /// Describes a formatted message.
    /// </summary>
    public abstract class FormattedMessageContent : MessageContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedMessageContent" /> class.
        /// </summary>
        /// <param name="body">The body of the message.</param>
        /// <param name="messageType">The message type.</param>
        /// <param name="format">Type of text formatting used.</param>
        /// <param name="formattedBody">The formatted body.</param>
        protected FormattedMessageContent(string body, string messageType, string format, string formattedBody)
            : base(body, messageType)
        {
            Format = format;
            FormattedBody = formattedBody;
        }

        /// <summary>
        /// Gets the type of formatting used.
        /// </summary>
        [JsonProperty("format")]
        public string Format { get; }

        /// <summary>
        /// Gets the formatted body.
        /// </summary>
        [JsonProperty("formatted_body")]
        public string FormattedBody { get; }

        /// <summary>
        /// Gets the rendered message.
        /// </summary>
        /// <remarks>
        /// If <see cref="FormattedBody" /> is non-<c>null</c>, it is returned; otherwise, the base
        /// <see cref="MessageContent.Body" /> is returned.
        /// </remarks>
        [JsonIgnore]
        public string Rendered => FormattedBody ?? base.Body;
    }
}
