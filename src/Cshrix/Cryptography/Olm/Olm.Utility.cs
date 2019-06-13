// <copyright file="Olm.Session.cs">
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
        /// Gets the size of a <c>Utility</c> object in bytes.
        /// </summary>
        /// <returns>The size in bytes.</returns>
        [DllImport(Name, EntryPoint = "olm_utility_size", ExactSpelling = true)]
        internal static extern uint olm_utility_size();

        /// <summary>
        /// Initializes a new <c>Utility</c> object using the supplied memory.
        /// The supplied memory must be at least <see cref="olm_utility_size" /> bytes.
        /// </summary>
        /// <param name="memory">Pointer to a location in memory where <c>Utility</c> should be initialized.</param>
        /// <returns>A pointer to the initialized <c>Utility</c>.</returns>
        [DllImport(Name, EntryPoint = "olm_utility", ExactSpelling = true)]
        internal static extern IntPtr olm_utility(IntPtr memory);

        /// <summary>
        /// Gets a string describing the most recent error to happen to utility.
        /// </summary>
        /// <param name="utility">A pointer to a previously created instance of <c>OlmUtility</c>.</param>
        /// <returns>A string describing the error.</returns>
        [DllImport(Name, EntryPoint = "olm_utility_last_error", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        internal static extern string olm_utility_last_error(IntPtr utility);

        /// <summary>
        /// Clears the memory used to back a <c>Utility</c>.
        /// </summary>
        /// <param name="utility">A pointer to a <c>Utility</c> object.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_clear_session", ExactSpelling = true)]
        internal static extern uint olm_clear_utility(IntPtr utility);

        /// <summary>
        /// Gets the length of the buffer needed to hold the SHA-256 hash.
        /// </summary>
        /// <param name="utility">A pointer to the utility.</param>
        /// <returns>The length of the buffer needed.</returns>
        [DllImport(Name, EntryPoint = "olm_sha256_length", ExactSpelling = true)]
        internal static extern uint olm_sha256_length(IntPtr utility);

        /// <summary>
        /// Calculates the SHA-256 hash of <paramref name="input" /> and encodes it as Base64.
        /// If <paramref name="output" /> is smaller than the value of <see cref="olm_sha256_length" /> then
        /// <see cref="olm_utility_last_error" /> will return <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </summary>
        /// <param name="utility">A pointer to the utility.</param>
        /// <param name="input">The input to hash.</param>
        /// <param name="input_length">Length of <paramref name="input" />.</param>
        /// <param name="output">Buffer to store output hash in.</param>
        /// <param name="output_length">Length of <paramref name="output" />.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_sha256", ExactSpelling = true)]
        internal static extern uint olm_sha256(
            IntPtr utility,
            byte[] input,
            uint input_length,
            byte[] output,
            uint output_length);

        /// <summary>
        /// Verify an ed25519 signature. If <paramref name="key" /> was too small then
        /// <see cref="olm_session_last_error" /> will return <c>INVALID_BASE64</c>.
        /// If the signature was invalid then <see cref="olm_utility_last_error" /> will return <c>BAD_MESSAGE_MAC</c>.
        /// </summary>
        /// <param name="utility">A pointer to the utility.</param>
        /// <param name="key">The key.</param>
        /// <param name="key_length">Length of <paramref name="key" />.</param>
        /// <param name="message">The message.</param>
        /// <param name="message_length">Length of <paramref name="message" />.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="signature_length">Length of <paramref name="signature" />.</param>
        [DllImport(Name, EntryPoint = "olm_ed25519_verify", ExactSpelling = true)]
        internal static extern uint olm_ed25519_verify(
            IntPtr utility,
            byte[] key,
            uint key_length,
            byte[] message,
            uint message_length,
            byte[] signature,
            uint signature_length);
    }
}
