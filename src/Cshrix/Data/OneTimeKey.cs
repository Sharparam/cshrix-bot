// <copyright file="OneTimeKey.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Serialization;

    [JsonConverter(typeof(OneTimeKeyConverter))]
    public readonly struct OneTimeKey
    {
        public OneTimeKey(string key, IDictionary<UserId, IDictionary<string, string>> signatures)
            : this(
                key,
                new ReadOnlyDictionary<UserId, IReadOnlyDictionary<string, string>>(
                    signatures.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IReadOnlyDictionary<string, string>)new ReadOnlyDictionary<string, string>(kvp.Value))))
        {
        }

        [JsonConstructor]
        public OneTimeKey(
            string key,
            IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, string>> signatures = null)
            : this()
        {
            Key = key;
            Signatures = signatures;
        }

        [JsonProperty("key")]
        public string Key { get; }

        /// <summary>
        /// Signatures for the key (if signed).
        /// </summary>
        /// <remarks>
        /// Will be <c>null</c> if this is an unsigned key.
        /// </remarks>
        [JsonProperty("signatures")]
        [CanBeNull]
        public IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, string>> Signatures { get; }
    }
}
