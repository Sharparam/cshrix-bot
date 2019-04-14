// <copyright file="JoinedRoom.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using Newtonsoft.Json;

    public readonly struct JoinedRoom
    {
        [JsonConstructor]
        public JoinedRoom(
            State state,
            Timeline timeline,
            EventsContainer ephemeral,
            EventsContainer accountData,
            UnreadCounts unreadCounts)
            : this()
        {
            State = state;
            Timeline = timeline;
            Ephemeral = ephemeral;
            AccountData = accountData;
            UnreadCounts = unreadCounts;
        }

        [JsonProperty("state")]
        public State State { get; }

        [JsonProperty("timeline")]
        public Timeline Timeline { get; }

        [JsonProperty("ephemeral")]
        public EventsContainer Ephemeral { get; }

        [JsonProperty("account_data")]
        public EventsContainer AccountData { get; }

        [JsonProperty("unread_notifications")]
        public UnreadCounts UnreadCounts { get; }
    }
}
