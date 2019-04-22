// <copyright file="ThirdPartyIdentifier.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.ThirdParty
{
    using System;

    using Newtonsoft.Json;

    using Serialization;

    public readonly struct ThirdPartyIdentifier
    {
        [JsonConstructor]
        public ThirdPartyIdentifier(DateTimeOffset addedAt, string address, string medium, DateTimeOffset validatedAt)
            : this()
        {
            AddedAt = addedAt;
            Address = address;
            Medium = medium;
            ValidatedAt = validatedAt;
        }

        [JsonProperty("added_at")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTimeOffset AddedAt { get; }

        [JsonProperty("address")]
        public string Address { get; }

        [JsonProperty("medium")]
        public string Medium { get; }

        [JsonProperty("validated_at")]
        public DateTimeOffset ValidatedAt { get; }
    }
}
