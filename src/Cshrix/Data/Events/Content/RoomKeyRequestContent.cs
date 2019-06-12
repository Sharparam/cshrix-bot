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

    /// <summary>
    /// Contains the contents of a room key request event (<c>m.room_key_request</c>).
    /// </summary>
    public sealed class RoomKeyRequestContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomKeyRequestContent" /> class.
        /// </summary>
        /// <param name="body">Information about the requested key.</param>
        /// <param name="action">Request action.</param>
        /// <param name="requestingDeviceId">The ID of the device requesting the key.</param>
        /// <param name="requestId">ID identifying the request.</param>
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

        /// <summary>
        /// Gets information about the requested key.
        /// </summary>
        /// <remarks>Required when <see cref="Action" /> is <see cref="KeyRequestAction.Request" />.</remarks>
        [JsonProperty("body")]
        public RequestedKeyInfo Body { get; }

        /// <summary>
        /// Gets the request action.
        /// </summary>
        [JsonProperty("action")]
        public KeyRequestAction Action { get; }

        /// <summary>
        /// Gets the ID of the device requesting the key.
        /// </summary>
        [JsonProperty("requesting_device_id")]
        public string RequestingDeviceId { get; }

        /// <summary>
        /// Gets a random string uniquely identifying the request for a key.
        /// </summary>
        /// <remarks>
        /// If the key is requested multiple times, the string ID should be reused. It should also be reused in order
        /// to cancel a request.
        /// </remarks>
        [JsonProperty("request_id")]
        public string RequestId { get; }
    }
}
