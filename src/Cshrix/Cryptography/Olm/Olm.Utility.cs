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
    using System.Runtime.InteropServices;

    internal static partial class Olm
    {
        /// <summary>
        /// Gets the size of a <c>Utility</c> object in bytes.
        /// </summary>
        /// <returns>The size in bytes.</returns>
        [DllImport(Name, EntryPoint = "olm_utility_size", ExactSpelling = true)]
        internal static extern uint GetUtilitySize();

        /// <summary>
        /// Initializes a new <c>Utility</c> object using the supplied memory.
        /// The supplied memory must be at least <see cref="GetUtilitySize" /> bytes.
        /// </summary>
        /// <param name="memory">Pointer to a location in memory where <c>Utility</c> should be initialized.</param>
        /// <returns>A pointer to the initialized <c>Utility</c>.</returns>
        [DllImport(Name, EntryPoint = "olm_utility", ExactSpelling = true)]
        internal static extern IntPtr InitializeUtility(IntPtr memory);

        /// <summary>
        /// Gets a string describing the most recent error to happen to utility.
        /// </summary>
        /// <param name="utility">A pointer to a previously created instance of <c>OlmUtility</c>.</param>
        /// <returns>A string describing the error.</returns>
        [DllImport(Name, EntryPoint = "olm_utility_last_error", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        internal static extern string GetLastUtilityError(IntPtr utility);

        /// <summary>
        /// Clears the memory used to back a <c>Utility</c>.
        /// </summary>
        /// <param name="utility">A pointer to a <c>Utility</c> object.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_clear_session", ExactSpelling = true)]
        internal static extern uint ClearUtility(IntPtr utility);

        /// <summary>
        /// Gets the length of the buffer needed to hold the SHA-256 hash.
        /// </summary>
        /// <param name="utility">A pointer to the utility.</param>
        /// <returns>The length of the buffer needed.</returns>
        [DllImport(Name, EntryPoint = "olm_sha256_length", ExactSpelling = true)]
        internal static extern uint GetSha256Length(
            IntPtr utility);

        /// <summary>
        /// Calculates the SHA-256 hash of <paramref name="input" /> and encodes it as Base64.
        /// If <paramref name="output" /> is smaller than the value of <see cref="GetSha256Length" /> then
        /// <see cref="GetLastUtilityError" /> will return <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </summary>
        /// <param name="utility">A pointer to the utility.</param>
        /// <param name="input">The input to hash.</param>
        /// <param name="inputLength">Length of <paramref name="input" />.</param>
        /// <param name="output">Buffer to store output hash in.</param>
        /// <param name="outputLength">Length of <paramref name="output" />.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_sha256", ExactSpelling = true)]
        internal static extern uint CalculateSha256(
            IntPtr utility,
            byte[] input,
            uint inputLength,
            byte[] output,
            uint outputLength);

        /// <summary>
        /// Verify an ed25519 signature. If <paramref name="key" /> was too small then
        /// <see cref="GetLastSessionError" /> will return <c>INVALID_BASE64</c>.
        /// If the signature was invalid then <see cref="GetLastUtilityError" /> will return <c>BAD_MESSAGE_MAC</c>.
        /// </summary>
        /// <param name="utility">A pointer to the utility.</param>
        /// <param name="key">The key.</param>
        /// <param name="keyLength">Length of <paramref name="key" />.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageLength">Length of <paramref name="message" />.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="signatureLength">Length of <paramref name="signature" />.</param>
        [DllImport(Name, EntryPoint = "olm_ed25519_verify", ExactSpelling = true)]
        internal static extern uint VerifyEd25519(
            IntPtr utility,
            byte[] key,
            uint keyLength,
            byte[] message,
            uint messageLength,
            byte[] signature,
            uint signatureLength);
    }
}
