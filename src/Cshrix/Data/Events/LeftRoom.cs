// <copyright file="LeftRoom.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using Newtonsoft.Json;

    public readonly struct LeftRoom
    {
        [JsonProperty("state")]
        public State State { get; }

        [JsonProperty("timeline")]
        public Timeline Timeline { get; }

        [JsonProperty("account_data")]
        public EventsContainer AccountData { get; }
    }
}
