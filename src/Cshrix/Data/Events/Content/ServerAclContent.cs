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

    /// <summary>
    /// Contains data on what servers are permitted to participate in a room.
    /// Content of the <c>m.room.server_acl</c> event.
    /// </summary>
    public sealed class ServerAclContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerAclContent" /> class.
        /// </summary>
        /// <param name="allowIpLiterals">Whether to allow server names that are IP literals.</param>
        /// <param name="allow">Collection of server names to allow in the room.</param>
        /// <param name="deny">Collection of server names to disallow in the room.</param>
        public ServerAclContent(
            bool allowIpLiterals,
            IReadOnlyCollection<string> allow,
            IReadOnlyCollection<string> deny)
        {
            AllowIpLiterals = allowIpLiterals;
            Allow = allow ?? Enumerable.Empty<string>().ToList().AsReadOnly();
            Deny = deny ?? Enumerable.Empty<string>().ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets a value indicating whether to allow server names that are IP literals.
        /// </summary>
        [JsonProperty("allow_ip_literals")]
        public bool AllowIpLiterals { get; }

        /// <summary>
        /// Gets a collection of server names to allow in the room.
        /// </summary>
        /// <remarks>
        /// Wildcards may be used to cover a wider range of hosts, where <c>*</c> matches zero or more characters
        /// and <c>?</c> matches exactly one character.
        /// </remarks>
        [JsonProperty("allow")]
        public IReadOnlyCollection<string> Allow { get; }

        /// <summary>
        /// Gets a collection of server names to disallow in the room. Wildcards may be used, following the same
        /// rules as defined on <see cref="Allow" />.
        /// </summary>
        [JsonProperty("deny")]
        public IReadOnlyCollection<string> Deny { get; }
    }
}
