// <copyright file="VersionsResponse.cs">
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

    public readonly struct VersionsResponse
    {
        public VersionsResponse(IEnumerable<string> versions)
            : this(versions.ToList().AsReadOnly())
        {
        }

        [JsonConstructor]
        public VersionsResponse(IReadOnlyCollection<string> versions)
            : this() =>
            Versions = versions;

        [JsonProperty("versions")]
        public IReadOnlyCollection<string> Versions { get; }
    }
}
