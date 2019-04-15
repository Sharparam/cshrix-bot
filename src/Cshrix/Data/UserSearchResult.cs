// <copyright file="UserSearchResult.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    public readonly struct UserSearchResult
    {
        public UserSearchResult(bool limited, IEnumerable<User> results)
            : this(limited, results.ToList().AsReadOnly())
        {
        }

        [JsonConstructor]
        public UserSearchResult(bool limited, IReadOnlyCollection<User> results)
            : this()
        {
            Limited = limited;
            Results = results;
        }

        [JsonProperty("limited")]
        public bool Limited { get; }

        [JsonProperty("results")]
        public IReadOnlyCollection<User> Results { get; }
    }
}
