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

    public abstract class FormattedMessageContent : MessageContent
    {
        protected FormattedMessageContent(string body, string messageType, string format, string formattedBody)
            : base(body, messageType)
        {
            Format = format;
            FormattedBody = formattedBody;
        }

        [JsonProperty("format")]
        public string Format { get; }

        [JsonProperty("formatted_body")]
        public string FormattedBody { get; }

        [JsonIgnore]
        public string Rendered => FormattedBody ?? base.Body;
    }
}
