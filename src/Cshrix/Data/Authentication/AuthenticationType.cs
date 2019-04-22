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

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AuthenticationType
    {
        [EnumMember(Value = "m.login.password")]
        Password,

        [EnumMember(Value = "m.login.recaptcha")]
        ReCaptcha,

        [EnumMember(Value = "m.login.oauth2")]
        Oauth2,

        [EnumMember(Value = "m.login.email.identity")]
        Email,

        [EnumMember(Value = "m.login.token")]
        Token,

        [EnumMember(Value = "m.login.dummy")]
        Dummy
    }
}
