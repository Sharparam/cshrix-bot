// <copyright file="TokenAuthentication.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Authentication;

    using Newtonsoft.Json;

    public sealed class TokenAuthentication : AuthenticationData
    {
        public TokenAuthentication(string token, string transactionId, string session)
            : base(AuthenticationType.Token, session)
        {
            Token = token;
            TransactionId = transactionId;
        }

        [JsonProperty("token")]
        public string Token { get; }

        [JsonProperty("txn_id")]
        public string TransactionId { get; }
    }
}
