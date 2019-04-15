// <copyright file="FormattedMessageContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System.Collections.Generic;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public abstract class FormattedMessageContent : MessageContent
    {
        protected FormattedMessageContent([NotNull] IDictionary<string, object> dictionary)
            : base(dictionary)
        {
            Format = GetValueOrDefault<string>("format");
            FormattedBody = GetValueOrDefault<string>("formatted_body");
        }

        public override string Body => FormattedBody ?? base.Body;

        [JsonProperty("format")]
        public string Format { get; }

        [JsonProperty("formatted_body")]
        public string FormattedBody { get; }
    }
}
