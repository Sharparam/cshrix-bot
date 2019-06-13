// <copyright file="Olm.PK.cs">
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
        /// Gets the size of a decryption object in bytes.
        /// </summary>
        /// <returns>The size of a decryption object in bytes.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_decryption_size", ExactSpelling = true)]
        internal static extern uint olm_pk_decryption_size();

        /// <summary>
        /// Initialises a decryption object using the supplied memory.\
        /// The supplied memory must be at least <see cref="olm_pk_decryption_size" /> bytes.
        /// </summary>
        /// <param name="memory">
        /// A pointer to the block of memory in which the decryption object will be initialised.
        /// </param>
        /// <returns>A pointer to the initialised decryption object.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_decryption", ExactSpelling = true)]
        internal static extern IntPtr olm_pk_decryption(IntPtr memory);

        /// <summary>
        /// Gets a null terminated string describing the most recent error to happen to a decryption object.
        /// </summary>
        /// <param name="decryption">A pointer to the decryption object.</param>
        /// <returns>The most recent error.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_decryption_last_error", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        internal static extern string olm_pk_decryption_last_error(IntPtr decryption);

        /// <summary>
        /// Clears the memory used to back this decryption object.
        /// </summary>
        /// <param name="decryption">A pointer to the decryption object.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_clear_pk_decryption", ExactSpelling = true)]
        internal static extern uint olm_clear_pk_decryption(IntPtr decryption);

        /// <summary>
        /// Gets the number of bytes required to store an olm private key.
        /// </summary>
        /// <returns>The number of bytes required.</returns>
        [DllImport(Name, EntryPoint = "olm_pk_private_key_length", ExactSpelling = true)]
        internal static extern uint olm_pk_private_key_length();

        /// <summary>
        /// Initialises the key from the private part of a key as returned by <see cref="olm_pk_get_private_key" />.
        /// The associated public key will be written to the <paramref name="pubkey" /> buffer.
        /// </summary>
        /// <param name="decryption">A pointer to the decryption object.</param>
        /// <param name="pubkey">An output buffer for the public key.</param>
        /// <param name="pubkey_length">The capacity of <paramref name="pubkey" />.</param>
        /// <param name="privkey">The private key.</param>
        /// <param name="privkey_length">The length of <paramref name="privkey" />.</param>
        /// <returns>The value of <see cref="olm_error" /> on failure.</returns>
        /// <remarks>
        /// <para>
        /// If the <paramref name="pubkey" /> buffer is too small then <see cref="olm_pk_decryption_last_error" />
        /// will return  <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// If the private key was not long enough then <see cref="olm_pk_decryption_last_error" />
        /// will return  <c>OLM_INPUT_BUFFER_TOO_SMALL</c>.
        /// </para>
        /// <para>
        /// Note that the pubkey is a Base64 encoded string, but the private key is an unencoded byte array.
        /// </para>
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_pk_key_from_private", ExactSpelling = true)]
        internal static extern uint olm_pk_key_from_private(
            IntPtr decryption,
            byte[] pubkey,
            uint pubkey_length,
            byte[] privkey,
            uint privkey_length);

        /// <summary>
        /// Returns the number of bytes needed to store a decryption object.
        /// </summary>
        /// <param name="decryption">A pointer to the decryption object to store.</param>
        /// <returns>The number of bytes needed.</returns>
        [DllImport(Name, EntryPoint = "olm_pickle_pk_decryption_length", ExactSpelling = true)]
        internal static extern uint olm_pickle_pk_decryption_length(IntPtr decryption);

        /// <summary>
        /// Stores decryption object as a Base64 string.
        /// Encrypts the object using the supplied <paramref name="key" />.
        /// </summary>
        /// <param name="decryption">A pointer to the decryption object.</param>
        /// <param name="key">The encryption key to use.</param>
        /// <param name="key_length">The length of <paramref name="key" />.</param>
        /// <param name="pickled">The output buffer to store the pickled object in.</param>
        /// <param name="pickled_length">The capacity of <paramref name="pickled" />.</param>
        /// <returns>
        /// Returns the length of the pickled object on success.
        /// Returns <see cref="olm_error" /> on failure.
        /// </returns>
        /// <remarks>
        /// If the pickle output buffer is smaller than <see cref="olm_pickle_pk_decryption_length" />
        /// then <see cref="olm_pk_decryption_last_error" /> will return  <c>OUTPUT_BUFFER_TOO_SMALL</c>
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_pickle_pk_decryption", ExactSpelling = true)]
        internal static extern uint olm_pickle_pk_decryption(
            IntPtr decryption,
            byte[] key,
            uint key_length,
            byte[] pickled,
            uint pickled_length);

        /// <summary>
        /// Loads a decryption object from a pickled Base64 string.
        /// The associated public key will be written to the pubkey buffer.
        /// Decrypts the object using the supplied key.
        /// </summary>
        /// <param name="decryption">A pointer to the decryption object.</param>
        /// <param name="key">The key to decrypt with.</param>
        /// <param name="key_length">The length of <paramref name="key" />.</param>
        /// <param name="pickled">The pickled object data.</param>
        /// <param name="pickled_length">The length of <paramref name="pickled" />.</param>
        /// <param name="pubkey">An output buffer for the public key.</param>
        /// <param name="pubkey_length">The capacity of <paramref name="pubkey" />.</param>
        /// <returns>The value of <see cref="olm_error" /> on failure.</returns>
        /// <remarks>
        /// If the key doesn't match the one used to encrypt the account then
        /// <see cref="olm_pk_decryption_last_error" /> will return  <c>BAD_ACCOUNT_KEY</c>.
        /// If the base64 couldn't be decoded then <see cref="olm_pk_decryption_last_error" />
        /// will return <c>INVALID_BASE64</c>.
        /// The input pickled buffer is destroyed
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_unpickle_pk_decryption", ExactSpelling = true)]
        internal static extern uint olm_unpickle_pk_decryption(
            IntPtr decryption,
            byte[] key,
            uint key_length,
            byte[] pickled,
            uint pickled_length,
            byte[] pubkey,
            uint pubkey_length);

        /// <summary>
        /// Get the length of the plaintext that will correspond to a ciphertext of the
        /// given length.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_max_plaintext_length", ExactSpelling = true)]
        internal static extern uint olm_pk_max_plaintext_length(IntPtr decryption, uint ciphertext_length);

        /// <summary>
        /// Decrypt a ciphertext. The input ciphertext buffer is destroyed. See the
        /// olm_pk_encrypt function for descriptions of the ephemeral_key and mac
        /// arguments. Returns the length of the plaintext on success. Returns
        /// <see cref="olm_error" /> on failure. If the plaintext buffer is too small then
        /// <see cref="olm_pk_encryption_last_error" /> will be  <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_decrypt", ExactSpelling = true)]
        internal static extern uint olm_pk_decrypt(
            IntPtr decryption,
            byte[] ephemeral_key,
            uint ephemeral_key_length,
            byte[] mac,
            uint mac_length,
            byte[] ciphertext,
            uint ciphertext_length,
            byte[] plaintext,
            uint max_plaintext_length);

        /// <summary>
        /// Get the private key for an OlmDecryption object as an unencoded byte array
        /// private_key must be a pointer to a buffer of at least
        /// <see cref="olm_pk_private_key_length" /> bytes and this length must be passed in
        /// private_key_length. If the given buffer is too small, returns <see cref="olm_error" />
        /// and <see cref="olm_pk_encryption_last_error" /> will be  <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// Returns the number of bytes written.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_get_private_key", ExactSpelling = true)]
        internal static extern uint olm_pk_get_private_key(
            IntPtr decryption,
            byte[] private_key,
            uint private_key_length);

        /// <summary>
        /// The size of a signing object in bytes
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_signing_size", ExactSpelling = true)]
        internal static extern uint olm_pk_signing_size();

        /// <summary>
        /// Initialise a signing object using the supplied memory The supplied memory must be at least <see cref="olm_pk_signing_size" /> bytes
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_signing", ExactSpelling = true)]
        internal static extern IntPtr olm_pk_signing(IntPtr memory);

        /// <summary>
        /// A null terminated string describing the most recent error to happen to a
        /// signing object
        /// </summary>
        [return: MarshalAs(UnmanagedType.LPStr)]
        [DllImport(Name, EntryPoint = "olm_pk_signing_last_error", ExactSpelling = true)]
        internal static extern string olm_pk_signing_last_error(IntPtr sign);

        /// <summary>
        /// Clears the memory used to back this signing object
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_clear_pk_signing", ExactSpelling = true)]
        internal static extern uint olm_clear_pk_signing(IntPtr sign);

        /// <summary>
        /// Initialise the signing object with a public/private keypair from a seed. The
        /// associated public key will be written to the pubkey buffer. Returns
        /// <see cref="olm_error" /> on failure. If the public key buffer is too small then
        /// <see cref="olm_pk_signing_last_error" /> will be  <c>OUTPUT_BUFFER_TOO_SMALL</c>.  If the seed
        /// buffer is too small then <see cref="olm_pk_signing_last_error" /> will be
        ///  <c>INPUT_BUFFER_TOO_SMALL</c>.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_signing_key_from_seed", ExactSpelling = true)]
        internal static extern uint olm_pk_signing_key_from_seed(
            IntPtr sign,
            byte[] pubkey,
            uint pubkey_length,
            byte[] seed,
            uint seed_length);

        /// <summary>
        /// The size required for the seed for initialising a signing object.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_signing_seed_length", ExactSpelling = true)]
        internal static extern uint olm_pk_signing_seed_length();

        /// <summary>
        /// The size of the public key of a signing object.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_signing_public_key_length", ExactSpelling = true)]
        internal static extern uint olm_pk_signing_public_key_length();

        /// <summary>
        /// The size of a signature created by a signing object.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_signature_length", ExactSpelling = true)]
        internal static extern uint olm_pk_signature_length();

        /// <summary>
        /// Sign a message. The signature will be written to the signature
        /// buffer. Returns <see cref="olm_error" /> on failure. If the signature buffer is too
        /// small, <see cref="olm_pk_signing_last_error" /> will be  <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_sign", ExactSpelling = true)]
        internal static extern uint olm_pk_sign(
            IntPtr sign,
            byte[] message,
            uint message_length,
            byte[] signature,
            uint signature_length);
    }
}
