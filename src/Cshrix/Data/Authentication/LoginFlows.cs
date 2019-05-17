// <copyright file="LoginFlows.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Authentication
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public readonly struct LoginFlows
    {
        [JsonConstructor]
        public LoginFlows(IReadOnlyCollection<LoginFlow> flows)
            : this() =>
            Flows = flows;

        [JsonProperty("flows")]
        public IReadOnlyCollection<LoginFlow> Flows { get; }
    }
}
