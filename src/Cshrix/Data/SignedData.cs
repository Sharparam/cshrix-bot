// <copyright file="SignedData.cs">
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

    public class SignedData
    {
        public SignedData(
            UserId userId,
            IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> signatures,
            string token)
        {
            UserId = userId;
            Signatures = signatures;
            Token = token;
        }

        [JsonProperty("mxid")]
        public UserId UserId { get; }

        [JsonProperty("signatures")]
        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> Signatures { get; }

        [JsonProperty("token")]
        public string Token { get; }
    }
}
