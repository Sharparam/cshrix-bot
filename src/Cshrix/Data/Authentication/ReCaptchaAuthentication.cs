// <copyright file="ReCaptchaAuthentication.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Authentication
{
    using Newtonsoft.Json;

    /// <summary>
    /// The user completes a Google ReCaptcha 2.0 challenge.
    /// </summary>
    public sealed class ReCaptchaAuthentication : AuthenticationData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReCaptchaAuthentication" /> class.
        /// </summary>
        /// <param name="response">The response to the ReCaptcha challenge.</param>
        /// <param name="session">The value of the session key given by the homeserver.</param>
        public ReCaptchaAuthentication(string response, string session)
            : base(AuthenticationType.ReCaptcha, session) =>
            Response = response;

        /// <summary>
        /// Gets the ReCaptcha response value.
        /// </summary>
        [JsonProperty("response")]
        public string Response { get; }
    }
}
