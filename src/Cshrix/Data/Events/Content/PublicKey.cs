// <copyright file="PublicKey.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains data on a public key in a third party invite.
    /// </summary>
    public readonly struct PublicKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicKey" /> structure.
        /// </summary>
        /// <param name="key">The public key.</param>
        /// <param name="keyValidityUri">A URI that can be used to validate <paramref name="key" />.</param>
        [JsonConstructor]
        public PublicKey(string key, [CanBeNull] Uri keyValidityUri)
            : this()
        {
            Key = key;
            KeyValidityUri = keyValidityUri;
        }

        /// <summary>
        /// Gets a Base64-encoded ED25519 key with which a token may be signed.
        /// </summary>
        [JsonProperty("public_key")]
        public string Key { get; }

        /// <summary>
        /// Gets a URI which can be called to validate whether <see cref="Key" /> has been revoked.
        /// </summary>
        /// <remarks>
        /// <para>The URI should be called with the query parameter <c>public_key</c> set to <see cref="Key" />.</para>
        /// <para>
        /// The URI <em>must</em> return a JSON object that contains a boolean property named <c>valid</c> which
        /// specifies if <see cref="Key" /> is valid. If this property is <c>null</c>, then <see cref="Key" />
        /// must be considered valid indefinitely.
        /// </para>
        /// </remarks>
        [JsonProperty("key_validity_url")]
        [CanBeNull]
        public Uri KeyValidityUri { get; }
    }
}
