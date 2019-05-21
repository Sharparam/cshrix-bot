// <copyright file="TokenAuthentication.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Authentication
{
    using Newtonsoft.Json;

    /// <summary>
    /// The client submits a login token.
    /// </summary>
    public sealed class TokenAuthentication : AuthenticationData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenAuthentication" /> class.
        /// </summary>
        /// <param name="token">The token to authenticate with.</param>
        /// <param name="transactionId">A transaction ID to identify this request.</param>
        /// <param name="session">The value of the session key given by the homeserver.</param>
        public TokenAuthentication(string token, string transactionId, string session)
            : base(AuthenticationType.Token, session)
        {
            Token = token;
            TransactionId = transactionId;
        }

        /// <summary>
        /// Gets the token to authenticate with.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; }

        /// <summary>
        /// Gets a transaction ID (nonce) to identify this request. If the request is retried, the same transaction
        /// ID should be used.
        /// </summary>
        [JsonProperty("txn_id")]
        public string TransactionId { get; }
    }
}
