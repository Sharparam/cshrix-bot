// <copyright file="JsonWebKey.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using Newtonsoft.Json;

    /// <summary>
    /// Describes a <a href="https://tools.ietf.org/html/rfc7517#appendix-A.3">JSON Web Key</a> object.
    /// </summary>
    public readonly struct JsonWebKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonWebKey" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="keyType">The type of the key.</param>
        /// <param name="algorithm">Key algorithm.</param>
        /// <param name="keyOperations">Available key operations.</param>
        /// <param name="extractable">Whether the key is extractable.</param>
        [JsonConstructor]
        public JsonWebKey(string key, string keyType, string algorithm, string[] keyOperations, bool extractable)
            : this()
        {
            Key = key;
            KeyType = keyType;
            Algorithm = algorithm;
            KeyOperations = keyOperations;
            Extractable = extractable;
        }

        /// <summary>
        /// Gets the key, encoded as URL-safe unpadded base64.
        /// </summary>
        [JsonProperty("k")]
        public string Key { get; }

        /// <summary>
        /// Gets the type of this key.
        /// </summary>
        /// <remarks>Must be <c>oct</c>.</remarks>
        [JsonProperty("key")]
        public string KeyType { get; }

        /// <summary>
        /// Gets the algorithm.
        /// </summary>
        /// <remarks>Must be <c>A256CTR</c>.</remarks>
        [JsonProperty("alg")]
        public string Algorithm { get; }

        /// <summary>
        /// Gets available operations for this key.
        /// </summary>
        /// <remarks>
        /// Must at least contain <c>encrypt</c> and <c>decrypt</c>.
        /// </remarks>
        [JsonProperty("key_opts")]
        public string[] KeyOperations { get; }

        /// <summary>
        /// Gets a value indicating whether key is extractable.
        /// </summary>
        /// <remarks>Must be <c>true</c>.</remarks>
        [JsonProperty("ext")]
        public bool Extractable { get; }
    }
}
