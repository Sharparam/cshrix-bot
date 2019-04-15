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
    using System.Collections.Generic;

    using JetBrains.Annotations;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class LocationMessageContent : MessageContent
    {
        public LocationMessageContent([NotNull] IDictionary<string, object> dictionary)
            : base(dictionary)
        {
            var hasUri = TryGetValue<string>("geo_uri", out var uri);
            Uri = hasUri ? new Uri(uri) : null;

            var hasInfo = TryGetValue<JObject>("info", out var info);
            Info = GetValueOrDefault<LocationInfo>("info");
        }

        [JsonProperty("geo_uri")]
        public Uri Uri { get; }

        [JsonProperty("info")]
        public LocationInfo Info { get; }
    }
}
