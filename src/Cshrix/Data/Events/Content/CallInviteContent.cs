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

    public sealed class CallInviteContent : CallContent
    {
        public CallInviteContent(string callId, Offer offer, int version, TimeSpan lifetime)
            : base(callId, version)
        {
            Offer = offer;
            Lifetime = lifetime;
        }

        [JsonProperty("offer")]
        public Offer Offer { get; }

        [JsonProperty("lifetime")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan Lifetime { get; }
    }
}
