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

    /// <summary>
    /// Contains information about a VoIP call event.
    /// </summary>
    public class CallContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallContent" /> class.
        /// </summary>
        /// <param name="callId">ID of the call.</param>
        /// <param name="version">Call version.</param>
        public CallContent(string callId, int version)
        {
            CallId = callId;
            Version = version;
        }

        /// <summary>
        /// Gets the ID of the call.
        /// </summary>
        [JsonProperty("call_id")]
        public string CallId { get; }

        /// <summary>
        /// Gets the version of the VoIP specification the call adheres to.
        /// </summary>
        [JsonProperty("version")]
        public int Version { get; }
    }
}
