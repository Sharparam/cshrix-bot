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
    using System.Runtime.InteropServices;

    internal static partial class Olm
    {
        /// <summary>
        /// A null terminated string describing the most recent error to happen to an
        /// SAS object.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_sas_last_error", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        internal static extern string olm_sas_last_error(IntPtr sas);

        /// <summary>
        /// The size of an SAS object in bytes.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_sas_size", ExactSpelling = true)]
        internal static extern uint olm_sas_size();

        /// <summary>
        /// Initialize an SAS object using the supplied memory.
        /// The supplied memory must be at least <c><see cref="olm_sas_size" /></c> bytes.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_sas", ExactSpelling = true)]
        internal static extern IntPtr olm_sas(IntPtr memory);

        /// <summary>
        /// Clears the memory used to back an SAS object.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_clear_sas", ExactSpelling = true)]
        internal static extern uint olm_clear_sas(IntPtr sas);

        /// <summary>
        /// The number of random bytes needed to create an SAS object.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_create_sas_random_length", ExactSpelling = true)]
        internal static extern uint olm_create_sas_random_length(IntPtr sas);

        /// <summary>
        /// Creates a new SAS object.
        /// </summary>
        /// <param name="sas"> [in] the SAS object to create, initialized by <c><see cref="olm_sas" /></c>.</param>
        /// <param name="random"> [in] array of random bytes.  The contents of the buffer may be overwritten.</param>
        /// <param name="random_length"> [in] the number of random bytes provided.  Must be at least <c><see cref="olm_create_sas_random_length" /></c>.</param>
        /// <returns>
        /// <see cref="GetErrorCode" /> on failure.  If there weren't enough random bytes then <c><see cref="olm_sas_last_error" /></c> will be <c>NOT_ENOUGH_RANDOM</c>.
        /// </returns>
        [DllImport(Name, EntryPoint = "olm_create_sas", ExactSpelling = true)]
        internal static extern uint olm_create_sas(IntPtr sas, byte[] random, uint random_length);

        /// <summary>
        /// The size of a public key in bytes.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_sas_pubkey_length", ExactSpelling = true)]
        internal static extern uint olm_sas_pubkey_length(IntPtr sas);

        /// <summary>
        /// Get the public key for the SAS object.
        /// </summary>
        /// <param name="sas"> [in] the SAS object.</param>
        /// <param name="pubkey"> [out] buffer to store the public key.</param>
        /// <param name="pubkey_length"> [in] the size of the <c>pubkey</c> buffer.  Must be at least <c><see cref="olm_sas_pubkey_length" /></c>.</param>
        /// <returns>
        /// <see cref="GetErrorCode" /> on failure.  If the <c>pubkey</c> buffer is too small, then <c><see cref="olm_sas_last_error" /></c> will be <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </returns>
        [DllImport(Name, EntryPoint = "olm_sas_get_pubkey", ExactSpelling = true)]
        internal static extern uint olm_sas_get_pubkey(IntPtr sas, byte[] pubkey, uint pubkey_length);

        /// <summary>
        /// Sets the public key of other user.
        /// </summary>
        /// <param name="sas"> [in] the SAS object.</param>
        /// <param name="their_key"> [in] the other user's public key.  The contents of the buffer will be overwritten.</param>
        /// <param name="their_key_length"> [in] the size of the <c>their_key</c> buffer.</param>
        /// <returns>
        /// <see cref="GetErrorCode" /> on failure.  If the <c>their_key</c> buffer is too small, then <c><see cref="olm_sas_last_error" /></c> will be <c>INPUT_BUFFER_TOO_SMALL</c>.
        /// </returns>
        [DllImport(Name, EntryPoint = "olm_sas_set_their_key", ExactSpelling = true)]
        internal static extern uint olm_sas_set_their_key(IntPtr sas, byte[] their_key, uint their_key_length);

        /// <summary>
        /// Generate bytes to use for the short authentication string.
        /// </summary>
        /// <param name="sas"> [in] the SAS object.</param>
        /// <param name="info"> [in] extra information to mix in when generating the bytes, as per the Matrix spec.</param>
        /// <param name="info_length"> [in] the length of the <c>info</c> parameter.</param>
        /// <param name="output"> [out] the output buffer.</param>
        /// <param name="output_length"> [in] the size of the output buffer.  For hex-based SAS as in the Matrix spec, this will be 5.</param>
        [DllImport(Name, EntryPoint = "olm_sas_generate_bytes", ExactSpelling = true)]
        internal static extern uint olm_sas_generate_bytes(
            IntPtr sas,
            byte[] info,
            uint info_length,
            byte[] output,
            uint output_length);

        /// <summary>
        /// The size of the message authentication code generated by <c><see cref="olm_sas_calculate_mac" /></c>.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_sas_mac_length", ExactSpelling = true)]
        internal static extern uint olm_sas_mac_length(IntPtr sas);

        /// <summary>
        /// Generate a message authentication code (MAC) based on the shared secret.
        /// </summary>
        /// <param name="sas"> [in] the SAS object.</param>
        /// <param name="input"> [in] the message to produce the authentication code for.</param>
        /// <param name="input_length"> [in] the length of the message.</param>
        /// <param name="info"> [in] extra information to mix in when generating the MAC, as per the Matrix spec.</param>
        /// <param name="info_length"> [in] the length of the <c>info</c> parameter.</param>
        /// <param name="mac"> [out] the buffer in which to store the MAC.</param>
        /// <param name="mac_length"> [in] the size of the <c>mac</c> buffer.  Must be at least <c><see cref="olm_sas_mac_length" /></c></param>
        /// <returns>
        /// <see cref="GetErrorCode" /> on failure.  If the <c>mac</c> buffer is too small, then <c><see cref="olm_sas_last_error" /></c> will be <c>OUTPUT_BUFFER_TOO_SMALL</c>.
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

        /// <summary>
        /// for compatibility with an old version of Riot
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_sas_calculate_mac_long_kdf", ExactSpelling = true)]
        internal static extern uint olm_sas_calculate_mac_long_kdf(
            IntPtr sas,
            byte[] input,
            uint input_length,
            byte[] info,
            uint info_length,
            byte[] mac,
            uint mac_length);
    }
}
