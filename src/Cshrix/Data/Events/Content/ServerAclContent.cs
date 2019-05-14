// <copyright file="ServerAclContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    public sealed class ServerAclContent : EventContent
    {
        public ServerAclContent(
            bool allowIpLiterals,
            IReadOnlyCollection<string> allow,
            IReadOnlyCollection<string> deny)
        {
            AllowIpLiterals = allowIpLiterals;
            Allow = allow ?? Enumerable.Empty<string>().ToList().AsReadOnly();
            Deny = deny ?? Enumerable.Empty<string>().ToList().AsReadOnly();
        }

        [JsonProperty("allow_ip_literals")]
        public bool AllowIpLiterals { get; }

        [JsonProperty("allow")]
        public IReadOnlyCollection<string> Allow { get; }

        [JsonProperty("deny")]
        public IReadOnlyCollection<string> Deny { get; }
    }
}
