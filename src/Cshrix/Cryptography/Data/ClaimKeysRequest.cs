// <copyright file="ClaimKeysRequest.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Cryptography.Data
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Cshrix.Data;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains data for a request to get encryption keys from a homeserver.
    /// </summary>
    public readonly struct ClaimKeysRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimKeysRequest" /> structure.
        /// </summary>
        /// <param name="oneTimeKeys">Dictionary specifying which keys to claim.</param>
        /// <param name="timeout">Time to wait when downloading keys from remote servers.</param>
        public ClaimKeysRequest(
            [NotNull] IDictionary<UserId, IDictionary<string, string>> oneTimeKeys,
            TimeSpan? timeout = null)
            : this(
                new ReadOnlyDictionary<UserId, IReadOnlyDictionary<string, string>>(
                    oneTimeKeys.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IReadOnlyDictionary<string, string>)new ReadOnlyDictionary<string, string>(kvp.Value))),
                timeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimKeysRequest" /> structure.
        /// </summary>
        /// <param name="oneTimeKeys">Dictionary specifying which keys to claim.</param>
        /// <param name="timeout">Time to wait when downloading keys from remote servers.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="oneTimeKeys" /> is <c>null</c>.
        /// </exception>
        [JsonConstructor]
        public ClaimKeysRequest(
            [NotNull] IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, string>> oneTimeKeys,
            TimeSpan? timeout = null)
            : this()
        {
            Timeout = timeout ?? TimeSpan.FromSeconds(10);
            OneTimeKeys = oneTimeKeys ?? throw new ArgumentNullException(nameof(oneTimeKeys));
        }

        /// <summary>
        /// Gets a dictionary specifying the keys to claim. Maps user IDs to a map from device ID to
        /// an algorithm name.
        /// </summary>
        [JsonProperty("one_time_keys")]
        public IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, string>> OneTimeKeys { get; }

        /// <summary>
        /// Gets the time to wait when downloading keys from remote servers.
        /// </summary>
        /// <remarks>
        /// 10 seconds is the recommended default.
        /// </remarks>
        [JsonProperty("timeout")]
        public TimeSpan Timeout { get; }
    }
}
