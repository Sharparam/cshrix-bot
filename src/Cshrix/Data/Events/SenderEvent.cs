// <copyright file="SenderEvent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using Content;

    using Newtonsoft.Json;

    public class SenderEvent : Event
    {
        [JsonConstructor]
        public SenderEvent(EventContent content, string type, string redacts, UserId sender)
            : base(content, type, redacts) =>
            Sender = sender;

        [JsonProperty("sender")]
        public UserId Sender { get; }
    }
}
