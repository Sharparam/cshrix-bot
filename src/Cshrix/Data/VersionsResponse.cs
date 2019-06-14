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

    /// <summary>
    /// Contains the list of versions supported by the server.
    /// </summary>
    public readonly struct VersionsResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VersionsResponse" /> structure.
        /// </summary>
        /// <param name="versions">Supported versions.</param>
        public VersionsResponse(IEnumerable<string> versions)
            : this(versions.ToList().AsReadOnly())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionsResponse" /> structure.
        /// </summary>
        /// <param name="versions">Supported versions.</param>
        [JsonConstructor]
        public VersionsResponse(IReadOnlyCollection<string> versions)
            : this() =>
            Versions = versions;

        /// <summary>
        /// Gets a collection of versions supported by the server.
        /// </summary>
        /// <remarks>
        /// These can be used to set the <c>ApiVersion</c> property on <see cref="IMatrixClientServerApi" />.
        /// </remarks>
        [JsonProperty("versions")]
        public IReadOnlyCollection<string> Versions { get; }
    }
}
