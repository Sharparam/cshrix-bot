// <copyright file="AvatarContent.cs">
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

    public class AvatarContent : EventContent
    {
        public AvatarContent([NotNull] IDictionary<string, object> dictionary)
            : base(dictionary)
        {
            var hasInfo = TryGetValue<JObject>("info", out var info);
            Info = hasInfo ? info.ToObject<ImageInfo>() : default;

            Uri = GetValueOrDefault<Uri>("url");
        }

        [JsonProperty("info")]
        public ImageInfo Info { get; }

        [JsonProperty("url")]
        public Uri Uri { get; }
    }
}
