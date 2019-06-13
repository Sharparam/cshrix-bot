// <copyright file="Olm.SAS.cs">
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
        /// Gets a null terminated string describing the most recent error to happen to an SAS object.
        /// </summary>
        /// <param name="sas">A pointer to the SAS object.</param>
        /// <returns>The most recent error.</returns>
        [DllImport(Name, EntryPoint = "olm_sas_last_error", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        internal static extern string olm_sas_last_error(IntPtr sas);

        /// <summary>
        /// Gets the size of an SAS object in bytes.
        /// </summary>
        /// <returns>The size in bytes.</returns>
        [DllImport(Name, EntryPoint = "olm_sas_size", ExactSpelling = true)]
        internal static extern uint olm_sas_size();

        /// <summary>
        /// Initialize an SAS object using the supplied memory.
        /// The supplied memory must be at least <see cref="olm_sas_size" /> bytes.
        /// </summary>
        /// <param name="memory">A pointer to the block of memory in which to initialize the SAS object.</param>
        /// <returns>A pointer to the initialized SAS object.</returns>
        [DllImport(Name, EntryPoint = "olm_sas", ExactSpelling = true)]
        internal static extern IntPtr olm_sas(IntPtr memory);

        /// <summary>
        /// Clears the memory used to back an SAS object.
        /// </summary>
        /// <param name="sas">A pointer to the SAS object.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_clear_sas", ExactSpelling = true)]
        internal static extern uint olm_clear_sas(IntPtr sas);

        /// <summary>
        /// Gets the number of random bytes needed to create an SAS object.
        /// </summary>
        /// <param name="sas">A pointer to the SAS object.</param>
        /// <returns>The number of bytes needed.</returns>
        [DllImport(Name, EntryPoint = "olm_create_sas_random_length", ExactSpelling = true)]
        internal static extern uint olm_create_sas_random_length(IntPtr sas);

        /// <summary>
        /// Creates a new SAS object.
        /// </summary>
        /// <param name="sas">A pointer to the SAS object to create, initialized by <see cref="olm_sas" />.</param>
        /// <param name="random">An array of random bytes. The contents of the buffer may be overwritten.</param>
        /// <param name="random_length">
        /// The number of random bytes provided.
        /// Must be at least <see cref="olm_create_sas_random_length" />.
        /// </param>
        /// <returns>
        /// The value of <see cref="olm_error" /> on failure.
        /// If there weren't enough random bytes then <see cref="olm_sas_last_error" />
        /// will return <c>NOT_ENOUGH_RANDOM</c>.
        /// </returns>
        [DllImport(Name, EntryPoint = "olm_create_sas", ExactSpelling = true)]
        internal static extern uint olm_create_sas(IntPtr sas, byte[] random, uint random_length);

        /// <summary>
        /// Gets the size of a public key in bytes.
        /// </summary>
        /// <param name="sas">A pointer to the SAS object.</param>
        /// <returns>The size in bytes.</returns>
        [DllImport(Name, EntryPoint = "olm_sas_pubkey_length", ExactSpelling = true)]
        internal static extern uint olm_sas_pubkey_length(IntPtr sas);

        /// <summary>
        /// Gets the public key for the SAS object.
        /// </summary>
        /// <param name="sas">A pointer to the SAS object.</param>
        /// <param name="pubkey">Buffer in which to store the public key.</param>
        /// <param name="pubkey_length">
        /// The size of the <paramref name="pubkey" /> buffer.
        /// Must be at least <see cref="olm_sas_pubkey_length" />.</param>
        /// <returns>
        /// The value of <see cref="olm_error" /> on failure.
        /// If the <paramref name="pubkey" /> buffer is too small, then <see cref="olm_sas_last_error" />
        /// will return <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </returns>
        [DllImport(Name, EntryPoint = "olm_sas_get_pubkey", ExactSpelling = true)]
        internal static extern uint olm_sas_get_pubkey(IntPtr sas, byte[] pubkey, uint pubkey_length);

        /// <summary>
        /// Sets the public key of other user.
        /// </summary>
        /// <param name="sas">A pointer to the SAS object.</param>
        /// <param name="their_key">The other user's public key. The contents of the buffer will be overwritten.</param>
        /// <param name="their_key_length">The size of the <paramref name="their_key" /> buffer.</param>
        /// <returns>
        /// The value of <see cref="olm_error" /> on failure.
        /// If the <paramref name="their_key" /> buffer is too small, then <see cref="olm_sas_last_error" />
        /// will return <c>INPUT_BUFFER_TOO_SMALL</c>.
        /// </returns>
        [DllImport(Name, EntryPoint = "olm_sas_set_their_key", ExactSpelling = true)]
        internal static extern uint olm_sas_set_their_key(IntPtr sas, byte[] their_key, uint their_key_length);

        /// <summary>
        /// Generates bytes to use for the short authentication string (SAS).
        /// </summary>
        /// <param name="sas">A pointer to the SAS object.</param>
        /// <param name="info">Extra information to mix in when generating the bytes, as per the Matrix spec.</param>
        /// <param name="info_length">The length of the <paramref name="info" /> parameter.</param>
        /// <param name="output">The output buffer.</param>
        /// <param name="output_length">
        /// The size of the output buffer.
        /// For hex-based SAS as in the Matrix spec, this will be <c>5</c>.
        /// </param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_sas_generate_bytes", ExactSpelling = true)]
        internal static extern uint olm_sas_generate_bytes(
            IntPtr sas,
            byte[] info,
            uint info_length,
            byte[] output,
            uint output_length);

        /// <summary>
        /// Gets the size of the message authentication code generated by <see cref="olm_sas_calculate_mac" />.
        /// </summary>
        /// <param name="sas">A pointer to the SAS object.</param>
        /// <returns>The size of the code.</returns>
        [DllImport(Name, EntryPoint = "olm_sas_mac_length", ExactSpelling = true)]
        internal static extern uint olm_sas_mac_length(IntPtr sas);

        /// <summary>
        /// Generates a message authentication code (MAC) based on the shared secret.
        /// </summary>
        /// <param name="sas">A pointer to the SAS object.</param>
        /// <param name="input">The message to produce the authentication code for.</param>
        /// <param name="input_length">The length of the message.</param>
        /// <param name="info">Extra information to mix in when generating the MAC, as per the Matrix spec.</param>
        /// <param name="info_length">The length of the <paramref name="info" /> parameter.</param>
        /// <param name="mac">The buffer in which to store the MAC.</param>
        /// <param name="mac_length">
        /// The size of the <paramref name="mac" /> buffer.
        /// Must be at least <see cref="olm_sas_mac_length" />.
        /// </param>
        /// <returns>
        /// The value of <see cref="olm_error" /> on failure.
        /// If the <paramref name="mac" /> buffer is too small, then <see cref="olm_sas_last_error" />
        /// will return <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </returns>
        [DllImport(Name, EntryPoint = "olm_sas_calculate_mac", ExactSpelling = true)]
        internal static extern uint olm_sas_calculate_mac(
            IntPtr sas,
            byte[] input,
            uint input_length,
            byte[] info,
            uint info_length,
            byte[] mac,
            uint mac_length);
    }
}
