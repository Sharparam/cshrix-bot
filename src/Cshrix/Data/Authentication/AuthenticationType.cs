// <copyright file="AuthenticationType.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Authentication
{
    using System.Runtime.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Available authentication types.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AuthenticationType
    {
        /// <summary>
        /// The client submits an identifier and secret password, both sent in plain-text.
        /// </summary>
        [EnumMember(Value = "m.login.password")]
        Password,

        /// <summary>
        /// The user completes a Google ReCaptcha 2.0 challenge.
        /// </summary>
        [EnumMember(Value = "m.login.recaptcha")]
        ReCaptcha,

        /// <summary>
        /// Authentication is supported via OAuth2 URLs. This login consists of multiple requests.
        /// </summary>
        [EnumMember(Value = "m.login.oauth2")]
        Oauth2,

        /// <summary>
        /// Authentication is supported by authorising an email address with an identity server.
        /// </summary>
        [EnumMember(Value = "m.login.email.identity")]
        Email,

        /// <summary>
        /// The client submits a login token.
        /// </summary>
        [EnumMember(Value = "m.login.token")]
        Token,

        /// <summary>
        /// Dummy authentication always succeeds and requires no extra parameters.
        /// Its purpose is to allow servers to not require any form of User-Interactive Authentication
        /// to perform a request.
        /// </summary>
        [EnumMember(Value = "m.login.dummy")]
        Dummy
    }
}
