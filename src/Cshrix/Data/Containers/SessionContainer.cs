// <copyright file="SessionContainer.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Newtonsoft.Json;

    /// <summary>
    /// Contains additional data for the
    /// <a href="https://matrix.org/docs/spec/client_server/r0.4.0.html#user-interactive-authentication-api">
    /// User-Interactive Authentication API
    /// </a>.
    /// </summary>
    public class SessionContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionContainer" /> class.
        /// </summary>
        /// <param name="session">The value of the session key given by the homeserver.</param>
        public SessionContainer(string session) => Session = session;

        /// <summary>
        /// Gets the value of the session key given by the homeserver.
        /// </summary>
        [JsonProperty(
            "session",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Session { get; }
    }
}
