// <copyright file="ClaimKeysResponse.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Cryptography.Data
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Cshrix.Data;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains a response with claimed keys from a previous call to claim keys from a homeserver.
    /// </summary>
    public readonly struct ClaimKeysResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimKeysResponse" /> structure.
        /// </summary>
        /// <param name="oneTimeKeys">A dictionary containing the claimed keys.</param>
        /// <param name="failures">A dictionary of failures that occurred.</param>
        public ClaimKeysResponse(
            IDictionary<UserId, IDictionary<string, OneTimeKey>> oneTimeKeys,
            IDictionary<string, object> failures)
            : this(
                new ReadOnlyDictionary<UserId, IReadOnlyDictionary<string, OneTimeKey>>(
                    oneTimeKeys.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IReadOnlyDictionary<string, OneTimeKey>)new ReadOnlyDictionary<string, OneTimeKey>(
                            kvp.Value))),
                new ReadOnlyDictionary<string, object>(failures))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimKeysResponse" /> structure.
        /// </summary>
        /// <param name="oneTimeKeys">A dictionary containing the claimed keys.</param>
        /// <param name="failures">A dictionary of failures that occurred.</param>
        [JsonConstructor]
        public ClaimKeysResponse(
            IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, OneTimeKey>> oneTimeKeys,
            IReadOnlyDictionary<string, object> failures)
            : this()
        {
            Failures = failures;
            OneTimeKeys = oneTimeKeys;
        }

        /// <summary>
        /// Gets a dictionary with the claimed keys for queried users/devices.
        /// </summary>
        [JsonProperty("one_time_keys")]
        public IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, OneTimeKey>> OneTimeKeys { get; }

        /// <summary>
        /// Gets any failures that occured.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If any remote homeservers could not be reached, they are recorded here. The keys are the names of the
        /// unreachable servers.
        /// </para>
        /// <para>
        /// If the homeserver could be reached, but the user or device was unknown, no failure is recorded.
        /// Instead, the corresponding user or device is missing from the <see cref="OneTimeKeys" /> dictionary.
        /// </para>
        /// </remarks>
        [JsonProperty("failures")]
        public IReadOnlyDictionary<string, object> Failures { get; }
    }
}
