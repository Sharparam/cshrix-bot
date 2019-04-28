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

    /// <summary>
    /// An encryption algorithm.
    /// </summary>
    public enum Algorithm
    {
        /// <summary>
        /// Version 1 of the Olm ratchet messaging algorithm.
        /// </summary>
        /// <remarks>
        /// <a href="http://matrix.org/docs/spec/olm.html">As defined by the Olm specification.</a>
        /// </remarks>
        [EnumMember(Value = "m.olm.v1.curve25519-aes-sha2")]
        Olm,

        /// <summary>
        /// Version 1 of the Megolm ratchet messaging algorithm.
        /// </summary>
        /// <remarks>
        /// <a href="http://matrix.org/docs/spec/megolm.html">As defined by the Megolm specification.</a>
        /// </remarks>
        [EnumMember(Value = "m.megolm.v1.aes-sha2")]
        Megolm
    }
}
