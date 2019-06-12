// <copyright file="AuthenticationFlow.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Authentication
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines an authentication flow for a homeserver.
    /// </summary>
    public readonly struct AuthenticationFlow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationFlow" /> structure.
        /// </summary>
        /// <param name="stages">The stages for this flow.</param>
        public AuthenticationFlow(IEnumerable<string> stages)
            : this(stages.ToList().AsReadOnly())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationFlow" /> structure.
        /// </summary>
        /// <param name="stages">The stages for this flow.</param>
        [JsonConstructor]
        public AuthenticationFlow(IReadOnlyCollection<string> stages)
            : this() =>
            Stages = stages;

        /// <summary>
        /// Gets the stages that are part of this authentication flow.
        /// </summary>
        /// <remarks>
        /// The authentication process takes the form of one or more "stages". At each stage the client submits
        /// a set of data for a given authentication type and awaits a response from the server, which will either
        /// be a final success or a request to perform an additional stage.
        /// This exchange continues until the final success.
        /// </remarks>
        [JsonProperty("stages")]
        public IReadOnlyCollection<string> Stages { get; }
    }
}
