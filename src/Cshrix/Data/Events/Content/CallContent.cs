// <copyright file="CallContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using Newtonsoft.Json;

    public class CallContent : EventContent
    {
        public CallContent(string callId, int version)
        {
            CallId = callId;
            Version = version;
        }

        [JsonProperty("call_id")]
        public string CallId { get; }

        [JsonProperty("version")]
        public int Version { get; }
    }
}
