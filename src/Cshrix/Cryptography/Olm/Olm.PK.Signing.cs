// <copyright file="Olm.PK.Signing.cs">
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

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Names match those used in the native library")]
    internal static partial class Olm
    {
        /// <summary>
        /// Gets the size of a signing object in bytes.
        /// </summary>
        /// <returns>The size in bytes.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_signing_size", ExactSpelling = true)]
        internal static extern uint olm_pk_signing_size();

        /// <summary>
        /// Initialises a signing object using the supplied memory.
        /// </summary>
        /// <param name="memory">A pointer to the memory in which to initialise the object.</param>
        /// <returns>A pointer to the initialised signing object.</returns>
        /// <remarks>
        /// The supplied memory must be at least <see cref="olm_pk_signing_size" /> bytes.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_pk_signing", ExactSpelling = true)]
        internal static extern IntPtr olm_pk_signing(IntPtr memory);

        /// <summary>
        /// Gets a null terminated string describing the most recent error to happen to a signing object.
        /// </summary>
        /// <param name="sign">A pointer to the signing object.</param>
        /// <returns>The most recent error.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_signing_last_error", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        internal static extern string olm_pk_signing_last_error(IntPtr sign);

        /// <summary>
        /// Clears the memory used to back this signing object.
        /// </summary>
        /// <param name="sign">A pointer to the signing object.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_clear_pk_signing", ExactSpelling = true)]
        internal static extern uint olm_clear_pk_signing(IntPtr sign);

        /// <summary>
        /// Initialises the signing object with a public/private key pair from a seed. The associated public key
        /// will be written to the <paramref name="pubkey" /> buffer.
        /// </summary>
        /// <param name="sign">A pointer to the signing object.</param>
        /// <param name="pubkey">An output buffer where the public key will be stored.</param>
        /// <param name="pubkey_length">The capacity of <paramref name="pubkey" />.</param>
        /// <param name="seed">The seed.</param>
        /// <param name="seed_length">The length of <paramref name="seed_length" />.</param>
        /// <returns>The value of <see cref="olm_error" /> on failure.</returns>
        /// <remarks>
        /// If the public key buffer is too small then <see cref="olm_pk_signing_last_error" />
        /// will return <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// If the <paramref name="seed" /> buffer is too small then <see cref="olm_pk_signing_last_error" />
        /// will return <c>INPUT_BUFFER_TOO_SMALL</c>.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_pk_signing_key_from_seed", ExactSpelling = true)]
        internal static extern uint olm_pk_signing_key_from_seed(
            IntPtr sign,
            byte[] pubkey,
            uint pubkey_length,
            byte[] seed,
            uint seed_length);

        /// <summary>
        /// Gets the size required for the seed for initialising a signing object.
        /// </summary>
        /// <returns>The size required.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_signing_seed_length", ExactSpelling = true)]
        internal static extern uint olm_pk_signing_seed_length();

        /// <summary>
        /// Gets the size of the public key of a signing object.
        /// </summary>
        /// <returns>The size of the public key.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_signing_public_key_length", ExactSpelling = true)]
        internal static extern uint olm_pk_signing_public_key_length();

        /// <summary>
        /// Gets the size of a signature created by a signing object.
        /// </summary>
        /// <returns>The size of a signature.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_signature_length", ExactSpelling = true)]
        internal static extern uint olm_pk_signature_length();

        /// <summary>
        /// Signs a message. The signature will be written to the <paramref name="signature" /> buffer.
        /// </summary>
        /// <param name="sign">A pointer to the signing object.</param>
        /// <param name="message">The message to sign.</param>
        /// <param name="message_length">The length of <paramref name="message" />.</param>
        /// <param name="signature">An output buffer for the signature.</param>
        /// <param name="signature_length">The capacity of <paramref name="signature" />.</param>
        /// <returns>The value of <see cref="olm_error" /> on failure.</returns>
        /// <remarks>
        /// If the signature buffer is too small, <see cref="olm_pk_signing_last_error" />
        /// will return <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_pk_sign", ExactSpelling = true)]
        internal static extern uint olm_pk_sign(
            IntPtr sign,
            byte[] message,
            uint message_length,
            byte[] signature,
            uint signature_length);
    }
}
