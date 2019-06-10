// <copyright file="CallInviteContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// Event content sent by a caller wishing to establish a call.
    /// </summary>
    public sealed class CallInviteContent : CallContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallInviteContent" /> class.
        /// </summary>
        /// <param name="callId">The ID of the call.</param>
        /// <param name="offer">The session description object.</param>
        /// <param name="version">Call version.</param>
        /// <param name="lifetime">The duration the invite is valid for.</param>
        public CallInviteContent(string callId, Offer offer, int version, TimeSpan lifetime)
            : base(callId, version)
        {
            Offer = offer;
            Lifetime = lifetime;
        }

        /// <summary>
        /// Gets the session description object.
        /// </summary>
        [JsonProperty("offer")]
        public Offer Offer { get; }

        /// <summary>
        /// Gets the duration that the invite is valid for.
        /// </summary>
        /// <remarks>
        /// Once the invite age exceeds this value, clients should discard it. They should also no longer show the
        /// call as awaiting an answer in any UI.
        /// </remarks>
        [JsonProperty("lifetime")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan Lifetime { get; }
    }
}
