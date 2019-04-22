// <copyright file="RoomKeyRequestContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using Cryptography;

    using Newtonsoft.Json;

    public class RoomKeyRequestContent : EventContent
    {
        public RoomKeyRequestContent(
            RequestedKeyInfo body,
            KeyRequestAction action,
            string requestingDeviceId,
            string requestId)
        {
            Body = body;
            Action = action;
            RequestingDeviceId = requestingDeviceId;
            RequestId = requestId;
        }

        [JsonProperty("body")]
        public RequestedKeyInfo Body { get; }

        [JsonProperty("action")]
        public KeyRequestAction Action { get; }

        [JsonProperty("requesting_device_id")]
        public string RequestingDeviceId { get; }

        [JsonProperty("request_id")]
        public string RequestId { get; }
    }
}
