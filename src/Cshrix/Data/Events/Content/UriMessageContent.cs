// <copyright file="UriMessageContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;
    using System.Collections.Generic;

    using JetBrains.Annotations;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public abstract class UriMessageContent<T> : MessageContent
    {
        protected UriMessageContent([NotNull] IDictionary<string, object> dictionary)
            : base(dictionary)
        {
            var hasInfo = TryGetValue<JObject>("info", out var info);
            Info = hasInfo ? info.ToObject<T>() : default;

            var hasUrl = TryGetValue<string>("url", out var url);
            Uri = hasUrl ? new Uri(url) : null;

            var hasFile = TryGetValue<JObject>("file", out var file);
            EncryptedFile = hasFile ? file.ToObject<EncryptedFile?>() : null;
        }

        [JsonProperty("info")]
        public T Info { get; }

        [JsonProperty("url")]
        [CanBeNull]
        public Uri Uri { get; }

        [JsonProperty("file")]
        public EncryptedFile? EncryptedFile { get; }
    }
}
