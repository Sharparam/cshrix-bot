// <copyright file="ClaimKeysRequest.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Newtonsoft.Json;

    public readonly struct ClaimKeysRequest
    {
        public ClaimKeysRequest(
            IDictionary<Identifier, IDictionary<string, string>> oneTimeKeys,
            TimeSpan? timeout = null)
            : this(
                new ReadOnlyDictionary<Identifier, IReadOnlyDictionary<string, string>>(
                    oneTimeKeys.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (IReadOnlyDictionary<string, string>)new ReadOnlyDictionary<string, string>(kvp.Value))),
                timeout)
        {
        }

        [JsonConstructor]
        public ClaimKeysRequest(
            IReadOnlyDictionary<Identifier, IReadOnlyDictionary<string, string>> oneTimeKeys,
            TimeSpan? timeout = null)
            : this()
        {
            Timeout = timeout ?? TimeSpan.FromSeconds(10);
            OneTimeKeys = oneTimeKeys;
        }

        [JsonProperty("timeout")]
        public TimeSpan Timeout { get; }

        [JsonProperty("one_time_keys")]
        public IReadOnlyDictionary<Identifier, IReadOnlyDictionary<string, string>> OneTimeKeys { get; }
    }
}
