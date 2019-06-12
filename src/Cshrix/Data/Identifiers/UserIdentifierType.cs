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

    /// <summary>
    /// Available identification types.
    /// </summary>
    [JsonConverter(typeof(StringConverter))]
    public enum UserIdentifierType
    {
        /// <summary>
        /// The user is identified by their Matrix ID.
        /// </summary>
        [EnumMember(Value = "user")]
        User,

        /// <summary>
        /// The user is identified by a third-party identifier in canonicalized form.
        /// </summary>
        [EnumMember(Value = "thirdparty")]
        ThirdParty,

        /// <summary>
        /// The user is identified by a phone number.
        /// </summary>
        [EnumMember(Value = "phone")]
        Phone
    }
}
