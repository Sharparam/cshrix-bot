// <copyright file="ClaimKeysResponse.cs">
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

    using Newtonsoft.Json;

    public readonly struct ClaimKeysResponse
    {
        public ClaimKeysResponse(
            IDictionary<string, object> failures,
            IDictionary<Identifier, IDictionary<string, OneTimeKey>> oneTimeKeys)
            : this(
                new ReadOnlyDictionary<string, object>(failures),
                new ReadOnlyDictionary<Identifier, IReadOnlyDictionary<string, OneTimeKey>>(
                    oneTimeKeys.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IReadOnlyDictionary<string, OneTimeKey>)new ReadOnlyDictionary<string, OneTimeKey>(
                            kvp.Value))))
        {
        }

        [JsonConstructor]
        public ClaimKeysResponse(
            IReadOnlyDictionary<string, object> failures,
            IReadOnlyDictionary<Identifier, IReadOnlyDictionary<string, OneTimeKey>> oneTimeKeys)
            : this()
        {
            Failures = failures;
            OneTimeKeys = oneTimeKeys;
        }

        [JsonProperty("failures")]
        public IReadOnlyDictionary<string, object> Failures { get; }

        [JsonProperty("one_time_keys")]
        public IReadOnlyDictionary<Identifier, IReadOnlyDictionary<string, OneTimeKey>> OneTimeKeys { get; }
    }
}
