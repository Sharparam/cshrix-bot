// <copyright file="TypingState.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;

    using Newtonsoft.Json;

    using Serialization;

    public readonly struct TypingState
    {
        [JsonConstructor]
        public TypingState(TimeSpan timeout, bool isTyping)
            : this()
        {
            Timeout = timeout;
            IsTyping = isTyping;
        }

        [JsonProperty("timeout")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan Timeout { get; }

        [JsonProperty("typing")]
        public bool IsTyping { get; }
    }
}
