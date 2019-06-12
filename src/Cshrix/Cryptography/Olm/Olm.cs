// <copyright file="Olm.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Cryptography.Olm
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Contains native external methods from the libolm library.
    /// </summary>
    /// <remarks>
    /// The following mappings apply:
    ///
    /// | Olm (C)                     | Olm (C#) |
    /// | --------------------------- | -------- |
    /// | `OlmAccount *`              | `IntPtr` |
    /// | `OlmSession *`              | `IntPtr` |
    /// | `OlmUtility *`              | `IntPtr` |
    /// | `OlmInboundGroupSession *`  | `IntPtr` |
    /// | `OlmOutboundGroupSession *` | `IntPtr` |
    /// | `OlmPkEncryption *`         | `IntPtr` |
    /// | `OlmPkDecryption *`         | `IntPtr` |
    /// | `OlmSAS *`                  | `IntPtr` |
    /// | `size_t`                    | `uint`   |
    /// | `void *`                    | `byte[]` |
    /// | `char *`                    | `string` |
    /// | `int` (on boolean funcs)    | `bool`   |
    ///
    /// `size_t` should not be mapped to `uint` if it is inside a `struct`, however.
    /// In that case it should map to something that has the correct amount of bytes.
    /// Or perhaps it can be solved with struct alignment?
    /// </remarks>
    internal static partial class Olm
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
        private const string Name = nameof(Olm);

        /// <summary>
        /// Available message types.
        /// </summary>
        internal enum MessageType : uint
        {
            /// <summary>
            /// A pre-key message.
            /// </summary>
            PreKey = 0,

            /// <summary>
            /// A normal message.
            /// </summary>
            Message = 1
        }

        /// <summary>
        /// Gets the version number of the library.
        /// </summary>
        /// <param name="major">Major version.</param>
        /// <param name="minor">Minor version.</param>
        /// <param name="patch">Patch version.</param>
        [DllImport(Name, EntryPoint = "olm_get_library_version", ExactSpelling = true)]
        internal static extern void GetVersion(out byte major, out byte minor, out byte patch);

        /// <summary>
        /// Gets the value that Olm will return from a function if there was an error.
        /// </summary>
        /// <returns>The value returned by Olm functions if there was an error.</returns>
        [DllImport(Name, EntryPoint = "olm_error", ExactSpelling = true)]
        internal static extern uint GetErrorCode();
    }
}
