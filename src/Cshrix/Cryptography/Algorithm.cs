// <copyright file="Algorithm.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Cryptography
{
    using System.Runtime.Serialization;

    public enum Algorithm
    {
        [EnumMember(Value = "m.olm.v1.curve25519-aes-sha2")]
        Olm,

        [EnumMember(Value = "m.megolm.v1.aes-sha2")]
        Megolm
    }
}
