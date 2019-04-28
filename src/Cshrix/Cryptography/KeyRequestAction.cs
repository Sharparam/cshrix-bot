// <copyright file="RequestAction.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Cryptography
{
    using System.Runtime.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Type of key request in end-to-end encryption.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum KeyRequestAction
    {
        /// <summary>
        /// Request a key.
        /// </summary>
        [EnumMember(Value = "request")]
        Request,

        /// <summary>
        /// Cancel a pending request for a key.
        /// </summary>
        [EnumMember(Value = "cancel_request")]
        CancelRequest
    }
}
