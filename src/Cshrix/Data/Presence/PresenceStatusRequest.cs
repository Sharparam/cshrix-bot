// <copyright file="PresenceStatusRequest.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Presence
{
    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct PresenceStatusRequest
    {
        [JsonConstructor]
        public PresenceStatusRequest(Presence presence, [CanBeNull] string statusMessage)
            : this()
        {
            Presence = presence;
            StatusMessage = statusMessage;
        }

        [JsonProperty("presence")]
        public Presence Presence { get; }

        [JsonProperty(
            "status_msg",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string StatusMessage { get; }
    }
}
