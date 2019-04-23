// <copyright file="NativeMethods.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Cryptography.Olm
{
    /// <summary>
    /// Contains native external methods from the libolm library.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// Base name of the native library providing libolm functionality.
        /// </summary>
        /// <remarks>
        /// C# interop will use this to resolve against the following names:
        ///  * olm.dll
        ///  * libolm.dll
        ///  * olm.so
        ///  * libolm.so
        ///  * olm.dylib
        ///  * libolm.dylib
        /// </remarks>
        private const string Name = "olm";
    }
}
