// <copyright file="Olm.PK.Encryption.cs">
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
        /// Gets the size of an encryption object in bytes.
        /// </summary>
        /// <returns>The size in bytes.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_encryption_size", ExactSpelling = true)]
        internal static extern uint olm_pk_encryption_size();

        /// <summary>
        /// Initialises an encryption object using the supplied memory.
        /// The supplied memory must be at least <see cref="olm_pk_encryption_size" /> bytes.
        /// </summary>
        /// <param name="memory">A pointer to the block of memory in which to initialise the object.</param>
        /// <returns>A pointer to the initialised object.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_encryption", ExactSpelling = true)]
        internal static extern IntPtr olm_pk_encryption(IntPtr memory);

        /// <summary>
        /// Gets a null terminated string describing the most recent error to happen to an encryption object.
        /// </summary>
        /// <param name="encryption">A pointer to the encryption object.</param>
        /// <returns>The most recent error.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_encryption_last_error", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(OlmStringMarshaler))]
        internal static extern string olm_pk_encryption_last_error(IntPtr encryption);

        /// <summary>
        /// Clears the memory used to back this encryption object.
        /// </summary>
        /// <param name="encryption">A pointer to the encryption object.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_clear_pk_encryption", ExactSpelling = true)]
        internal static extern uint olm_clear_pk_encryption(IntPtr encryption);

        /// <summary>
        /// Sets the recipient's public key for encrypting to.
        /// </summary>
        /// <param name="encryption">A pointer to the encryption object.</param>
        /// <param name="public_key">The public key to set.</param>
        /// <param name="public_key_length">The length of <paramref name="public_key" />.</param>
        [DllImport(Name, EntryPoint = "olm_pk_encryption_set_recipient_key", ExactSpelling = true)]
        internal static extern uint olm_pk_encryption_set_recipient_key(
            IntPtr encryption,
            byte[] public_key,
            uint public_key_length);

        /// <summary>
        /// Gets the length of the ciphertext that will correspond to a plaintext of the given length.
        /// </summary>
        /// <param name="encryption">A pointer to the encryption object.</param>
        /// <param name="plaintext_length">The length of the plaintext.</param>
        /// <returns>The length of the ciphertext.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_ciphertext_length", ExactSpelling = true)]
        internal static extern uint olm_pk_ciphertext_length(IntPtr encryption, uint plaintext_length);

        /// <summary>
        /// Gets the length of the message authentication code (MAC).
        /// </summary>
        /// <param name="encryption">A pointer to the encryption object.</param>
        /// <returns>The length of the MAC.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_mac_length", ExactSpelling = true)]
        internal static extern uint olm_pk_mac_length(IntPtr encryption);

        /// <summary>
        /// Gets the length of a public or ephemeral key.
        /// </summary>
        /// <returns>The length of the key.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_key_length", ExactSpelling = true)]
        internal static extern uint olm_pk_key_length();

        /// <summary>
        /// Gets the number of random bytes needed to encrypt a message.
        /// </summary>
        /// <param name="encryption">A pointer to the encryption object.</param>
        /// <returns>The number of random bytes needed.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_encrypt_random_length", ExactSpelling = true)]
        internal static extern uint olm_pk_encrypt_random_length(IntPtr encryption);

        /// <summary>
        /// Encrypt a plaintext for the recipient set using <see cref="olm_pk_encryption_set_recipient_key" />.
        /// </summary>
        /// <param name="encryption">A pointer to the encryption object.</param>
        /// <param name="plaintext">The plaintext to encrypt.</param>
        /// <param name="plaintext_length">The length of <paramref name="plaintext" />.</param>
        /// <param name="ciphertext">An output buffer for the generated ciphertext.</param>
        /// <param name="ciphertext_length">The capacity of <paramref name="ciphertext" />.</param>
        /// <param name="mac">An output buffer for the generated Message Authentication Code (MAC).</param>
        /// <param name="mac_length">The capacity of <paramref name="mac" />.</param>
        /// <param name="ephemeral_key">An output buffer for the generated ephemeral key.</param>
        /// <param name="ephemeral_key_size">The capacity of <paramref name="ephemeral_key" />.</param>
        /// <param name="random">Random bytes.</param>
        /// <param name="random_length">The length of <paramref name="random" />.</param>
        /// <returns>The value of <see cref="olm_error" /> on failure.</returns>
        /// <remarks>
        /// <para>
        /// Writes to the <paramref name="ciphertext" />, <paramref name="mac" />, and
        /// <paramref name="ephemeral_key" /> buffers, whose values should be sent to the recipient.
        /// <paramref name="mac" /> is a Message Authentication Code to ensure that the data is received and
        /// decrypted properly.
        /// <paramref name="ephemeral_key" /> is the public part of the ephemeral key used (together with
        /// the recipient's key) to generate a symmetric encryption key.
        /// </para>
        /// <para>
        /// If the <paramref name="ciphertext" />, <paramref name="mac" />, or <paramref name="ephemeral_key" />
        /// buffers were too small then <see cref="olm_pk_encryption_last_error" />
        /// will return <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// If there weren't enough random bytes then <see cref="olm_pk_encryption_last_error" />
        /// will return <c>OLM_INPUT_BUFFER_TOO_SMALL</c>.
        /// </para>
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_pk_encrypt", ExactSpelling = true)]
        internal static extern uint olm_pk_encrypt(
            IntPtr encryption,
            byte[] plaintext,
            uint plaintext_length,
            byte[] ciphertext,
            uint ciphertext_length,
            byte[] mac,
            uint mac_length,
            byte[] ephemeral_key,
            uint ephemeral_key_size,
            byte[] random,
            uint random_length);
    }
}
