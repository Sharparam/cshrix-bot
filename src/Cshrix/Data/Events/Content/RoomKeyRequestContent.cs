// <copyright file="RoomKeyRequestContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System.Collections.Generic;

    using Cryptography;

    using JetBrains.Annotations;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class RoomKeyRequestContent : EventContent
    {
        public RoomKeyRequestContent([NotNull] IDictionary<string, object> dictionary)
            : base(dictionary)
        {
            var body = GetValueOrDefault<JObject>("body");
            Body = body?.ToObject<RequestedKeyInfo>() ?? default;
            Action = GetValueOrDefault<RequestAction>("action");
            RequestingDeviceId = GetValueOrDefault<string>("requesting_device_id");
            RequestId = GetValueOrDefault<string>("request_id");
        }

        [JsonProperty("body")]
        public RequestedKeyInfo Body { get; }

        [JsonProperty("action")]
        public RequestAction Action { get; }

        [JsonProperty("requesting_device_id")]
        public string RequestingDeviceId { get; }

        [JsonProperty("request_id")]
        public string RequestId { get; }
    }
}
