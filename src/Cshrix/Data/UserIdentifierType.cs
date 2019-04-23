// <copyright file="UserIdentifierType.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.ComponentModel;
    using System.Runtime.Serialization;

    using Newtonsoft.Json;

    [JsonConverter(typeof(StringConverter))]
    public enum UserIdentifierType
    {
        [EnumMember(Value = "user")]
        User,

        [EnumMember(Value = "thirdparty")]
        ThirdParty,

        [EnumMember(Value = "phone")]
        Phone
    }
}
