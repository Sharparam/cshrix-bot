// <copyright file="Olm.PK.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Cryptography.Olm
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Names match those used in the native library")]
    internal static partial class Olm
    {
        /// <summary>
        /// Gets the number of bytes required to store an olm private key.
        /// </summary>
        /// <returns>The number of bytes required.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_private_key_length", ExactSpelling = true)]
        internal static extern uint olm_pk_private_key_length();
    }
}
