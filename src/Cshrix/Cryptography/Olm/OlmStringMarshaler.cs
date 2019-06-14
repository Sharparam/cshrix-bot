// <copyright file="OlmStringMarshaler.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Cryptography.Olm
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    using JetBrains.Annotations;

    /// <summary>
    /// A custom marshaler to convert <c>char *</c> pointers from Olm (pointers to literal strings in this case)
    /// into C# strings.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Follows MS naming")]
    internal sealed class OlmStringMarshaler : ICustomMarshaler
    {
        /// <summary>
        /// A factory to provide a singleton instance of <see cref="OlmStringMarshaler" />.
        /// </summary>
        private static readonly Lazy<OlmStringMarshaler> Instance =
            new Lazy<OlmStringMarshaler>(() => new OlmStringMarshaler());

        /// <summary>
        /// Gets an instance of <see cref="OlmStringMarshaler" /> for the specified cookie.
        /// </summary>
        /// <param name="cookie">The cookie.</param>
        /// <returns>An instance of <see cref="OlmStringMarshaler" />.</returns>
        [UsedImplicitly]
        public static ICustomMarshaler GetInstance(string cookie) => Instance.Value;

        /// <summary>
        /// Performs necessary cleanup of the managed data when it is no longer needed.
        /// </summary>
        /// <param name="ManagedObj">The managed object to be destroyed.</param>
        /// <exception cref="NotSupportedException">
        /// Thrown as managed data conversion is not supported here.
        /// </exception>
        public void CleanUpManagedData(object ManagedObj) => throw new NotSupportedException();

        /// <summary>
        /// Skips cleanup of the native data because they are literal strings and do not need nor can be freed.
        /// </summary>
        /// <param name="pNativeData">Pointer to the native data.</param>
        public void CleanUpNativeData(IntPtr pNativeData)
        {
        }

        /// <summary>
        /// Returns the size of the native data to be marshaled.
        /// </summary>
        /// <returns>The size, in bytes, of the native data.</returns>
        public int GetNativeDataSize() => IntPtr.Size;

        /// <summary>
        /// Converts the managed data to unmanaged data.
        /// </summary>
        /// <param name="ManagedObj">The managed object to be converted.</param>
        /// <returns>A pointer to the COM view of the managed object.</returns>
        /// <exception cref="NotSupportedException">
        /// Thrown as managed data conversion is not supported here.
        /// </exception>
        public IntPtr MarshalManagedToNative(object ManagedObj) => throw new NotSupportedException();

        /// <summary>
        /// Converts the unmanaged data to managed data.
        /// </summary>
        /// <param name="pNativeData">A pointer to the unmanaged data to be wrapped.</param>
        /// <returns>An object that represents the managed view of the unmanaged data.</returns>
        public object MarshalNativeToManaged(IntPtr pNativeData) => Marshal.PtrToStringAnsi(pNativeData);
    }
}
