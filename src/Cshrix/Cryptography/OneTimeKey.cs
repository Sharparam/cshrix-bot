// <copyright file="OneTimeKey.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Cryptography
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Data;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// A one-time key for encryption.
    /// </summary>
    [JsonConverter(typeof(OneTimeKeyConverter))]
    public readonly struct OneTimeKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneTimeKey" /> structure.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="signatures">Signatures for the key, if any.</param>
        public OneTimeKey(string key, IDictionary<UserId, IDictionary<string, string>> signatures)
            : this(
                key,
                new ReadOnlyDictionary<UserId, IReadOnlyDictionary<string, string>>(
                    signatures.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IReadOnlyDictionary<string, string>)new ReadOnlyDictionary<string, string>(kvp.Value))))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OneTimeKey" /> structure.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="signatures">Signatures for the key, if any.</param>
        [JsonConstructor]
        public OneTimeKey(
            string key,
            IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, string>> signatures = null)
            : this()
        {
            Key = key;
            Signatures = signatures;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; }

        /// <summary>
        /// Gets the signatures for the key (if signed).
        /// </summary>
        /// <remarks>
        /// Will be <c>null</c> if this is an unsigned key.
        /// </remarks>
        [JsonProperty("signatures")]
        [CanBeNull]
        public IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, string>> Signatures { get; }
    }
}
