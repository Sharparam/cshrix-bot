// <copyright file="EncryptedFile.cs">
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
    /// Describes an encrypted file.
    /// </summary>
    public readonly struct EncryptedFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EncryptedFile" /> structure.
        /// </summary>
        /// <param name="uri">The URI to the file.</param>
        /// <param name="key">The JSON Web Key object for the file.</param>
        /// <param name="iv">Initialization vector used by AES-CTR.</param>
        /// <param name="hashes">Dictionary mapping algorithms to ciphertext hashes.</param>
        /// <param name="version">Version of the encrypted attachments protocol.</param>
        [JsonConstructor]
        public EncryptedFile(
            Uri uri,
            JsonWebKey key,
            string iv,
            IReadOnlyDictionary<string, string> hashes,
            string version)
            : this()
        {
            Uri = uri;
            Key = key;
            IV = iv;
            Hashes = hashes;
            Version = version;
        }

        /// <summary>
        /// Gets the URI to the file.
        /// </summary>
        [JsonProperty("url")]
        public Uri Uri { get; }

        /// <summary>
        /// Gets the JSON Web Key object for the file.
        /// </summary>
        [JsonProperty("key")]
        public JsonWebKey Key { get; }

        /// <summary>
        /// Gets the initialization vector used by AES-CTR, encoded as unpadded Base64.b
        /// </summary>
        [JsonProperty("iv")]
        public string IV { get; }

        /// <summary>
        /// Gets a dictionary mapping algorithm names to a hash of the ciphertext, encoded as unpadded Base64.
        /// </summary>
        /// <remarks>Clients should support the SHA-256 hash, which uses the key <c>sha256</c>.</remarks>
        [JsonProperty("hashes")]
        public IReadOnlyDictionary<string, string> Hashes { get; }

        /// <summary>
        /// Gets the version of the encrypted attachments protocol.
        /// </summary>
        /// <remarks>Must be <c>v2</c>.</remarks>
        [JsonProperty("v")]
        public string Version { get; }
    }
}
