// <copyright file="SignedSenderData.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class SignedSenderData : SignedData
    {
        public SignedSenderData(
            UserId userId,
            IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> signatures,
            string token,
            UserId sender)
            : base(userId, signatures, token) =>
            Sender = sender;

        [JsonProperty("sender")]
        public UserId Sender { get; }
    }
}
