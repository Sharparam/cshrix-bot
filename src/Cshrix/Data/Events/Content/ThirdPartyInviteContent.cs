// <copyright file="ThirdPartyInviteContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains the contents for a third party invite event.
    /// </summary>
    public sealed class ThirdPartyInviteContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThirdPartyInviteContent" /> class.
        /// </summary>
        /// <param name="displayName">Display name of the invited user.</param>
        /// <param name="keyValidityUri">URI to use for validating <paramref name="publicKey" />.</param>
        /// <param name="publicKey">Key to sign token with.</param>
        /// <param name="publicKeys">Keys that can be used to sign the token with.</param>
        public ThirdPartyInviteContent(
            string displayName,
            Uri keyValidityUri,
            string publicKey,
            IReadOnlyCollection<PublicKey> publicKeys)
        {
            DisplayName = displayName;
            KeyValidityUri = keyValidityUri;
            PublicKey = publicKey;
            PublicKeys = publicKeys;
        }

        /// <summary>
        /// Gets the display name of the invited user.
        /// </summary>
        [JsonProperty("display_name")]
        public string DisplayName { get; }

        /// <summary>
        /// Gets a URI which can be fetched to validate whether <see cref="PublicKey" /> has been revoked.
        /// </summary>
        /// <remarks>
        /// <para>The URI is called with the query parameter <c>public_key</c> set to <see cref="PublicKey" />.</para>
        /// <para>The URI must return a JSON object containing a boolean property named <c>valid</c>.</para>
        /// </remarks>
        [JsonProperty("key_validity_url")]
        public Uri KeyValidityUri { get; }

        /// <summary>
        /// Gets a Base64-encoded Ed25519 key with which token must be signed (though a signature from any entry
        /// in <see cref="PublicKeys" /> is also sufficient).
        /// </summary>
        /// <remarks>This exists for backwards compatibility.</remarks>
        [JsonProperty("public_key")]
        public string PublicKey { get; }

        /// <summary>
        /// Gets a collection of keys with which the token may be signed.
        /// </summary>
        [JsonProperty("public_keys")]
        public IReadOnlyCollection<PublicKey> PublicKeys { get; }
    }
}
