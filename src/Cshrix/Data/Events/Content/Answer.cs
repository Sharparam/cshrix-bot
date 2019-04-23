// <copyright file="Answer.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using Newtonsoft.Json;

    public readonly struct Answer
    {
        [JsonConstructor]
        public Answer(string type, string sdp)
            : this()
        {
            Type = type;
            Sdp = sdp;
        }

        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("sdp")]
        public string Sdp { get; }
    }
}
