// <copyright file="StateEvent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using System;

    using Content;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public class StateEvent : RoomEvent
    {
        public StateEvent(
            EventContent content,
            string type,
            Identifier id,
            Identifier? roomId,
            Identifier sender,
            DateTimeOffset sentAt,
            UnsignedData? unsigned,
            [CanBeNull] EventContent previousContent,
            string stateKey)
            : base(content, type, id, sender, roomId, sentAt, unsigned)
        {
            StateKey = stateKey;
            PreviousContent = previousContent;
        }

        [JsonProperty("state_key")]
        public string StateKey { get; }

        [JsonProperty("prev_content")]
        [CanBeNull]
        public EventContent PreviousContent { get; }
    }
}
