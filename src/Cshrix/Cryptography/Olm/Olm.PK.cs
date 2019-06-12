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
    using System.Runtime.InteropServices;

    internal static partial class Olm
    {
        /// <summary>
        /// The size of an encryption object in bytes
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_encryption_size", ExactSpelling = true)]
        internal static extern uint olm_pk_encryption_size();

        /// <summary>
        /// Initialise an encryption object using the supplied memory The supplied memory must be at least <see cref="olm_pk_encryption_size" /> bytes
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_encryption", ExactSpelling = true)]
        internal static extern IntPtr olm_pk_encryption(IntPtr memory);

        /// <summary>
        /// A null terminated string describing the most recent error to happen to an
        /// encryption object
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_encryption_last_error", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        internal static extern string olm_pk_encryption_last_error(IntPtr encryption);

        /// <summary>
        /// Clears the memory used to back this encryption object
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_clear_pk_encryption", ExactSpelling = true)]
        internal static extern uint olm_clear_pk_encryption(IntPtr encryption);

        /// <summary>
        /// Set the recipient's public key for encrypting to
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_encryption_set_recipient_key", ExactSpelling = true)]
        internal static extern uint olm_pk_encryption_set_recipient_key(
            IntPtr encryption,
            byte[] public_key,
            uint public_key_length);

        /// <summary>
        /// Get the length of the ciphertext that will correspond to a plaintext of the
        /// given length.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_ciphertext_length", ExactSpelling = true)]
        internal static extern uint olm_pk_ciphertext_length(IntPtr encryption, uint plaintext_length);

        /// <summary>
        /// Get the length of the message authentication code.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_mac_length", ExactSpelling = true)]
        internal static extern uint olm_pk_mac_length(IntPtr encryption);

        /// <summary>
        /// Get the length of a public or ephemeral key
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_key_length", ExactSpelling = true)]
        internal static extern uint olm_pk_key_length();

        /// <summary>
        /// The number of random bytes needed to encrypt a message.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_encrypt_random_length", ExactSpelling = true)]
        internal static extern uint olm_pk_encrypt_random_length(IntPtr encryption);

        /// <summary>
        /// Encrypt a plaintext for the recipient set using
        /// olm_pk_encryption_set_recipient_key. Writes to the ciphertext, mac, and
        /// ephemeral_key buffers, whose values should be sent to the recipient. mac is
        /// a Message Authentication Code to ensure that the data is received and
        /// decrypted properly. ephemeral_key is the public part of the ephemeral key
        /// used (together with the recipient's key) to generate a symmetric encryption
        /// key. Returns <see cref="GetErrorCode" /> on failure. If the ciphertext, mac, or
        /// ephemeral_key buffers were too small then <see cref="olm_pk_encryption_last_error" />
        /// will be  <c>OUTPUT_BUFFER_TOO_SMALL</c>. If there weren't enough random bytes then
        /// <see cref="olm_pk_encryption_last_error" /> will be  <c>OLM_INPUT_BUFFER_TOO_SMALL</c>.
        /// </summary>
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

        /// <summary>
        /// The size of a decryption object in bytes
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_decryption_size", ExactSpelling = true)]
        internal static extern uint olm_pk_decryption_size();

        /// <summary>
        /// Initialise a decryption object using the supplied memory The supplied memory must be at least <see cref="olm_pk_decryption_size" /> bytes
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_decryption", ExactSpelling = true)]
        internal static extern IntPtr olm_pk_decryption(byte[] memory);

        /// <summary>
        /// A null terminated string describing the most recent error to happen to a
        /// decription object
        /// </summary>
        [return: MarshalAs(UnmanagedType.LPStr)]
        [DllImport(Name, EntryPoint = "olm_pk_decryption_last_error", ExactSpelling = true)]
        internal static extern string olm_pk_decryption_last_error(IntPtr decryption);

        /// <summary>
        /// Clears the memory used to back this decryption object
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_clear_pk_decryption", ExactSpelling = true)]
        internal static extern uint olm_clear_pk_decryption(IntPtr decryption);

        /// <summary>
        /// Get the number of bytes required to store an olm private key
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_private_key_length", ExactSpelling = true)]
        internal static extern uint olm_pk_private_key_length();

        /// <summary>
        /// DEPRECATED: Use <see cref="olm_pk_private_key_length" />
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_generate_key_random_length", ExactSpelling = true)]
        internal static extern uint olm_pk_generate_key_random_length();

        /// <summary>
        /// Initialise the key from the private part of a key as returned by
        /// <see cref="olm_pk_get_private_key" />. The associated public key will be written to the
        /// pubkey buffer. Returns <see cref="GetErrorCode" /> on failure. If the pubkey buffer is too
        /// small then <see cref="olm_pk_decryption_last_error" /> will be  <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// If the private key was not long enough then <see cref="olm_pk_decryption_last_error" />
        /// will be  <c>OLM_INPUT_BUFFER_TOO_SMALL</c>.
        /// Note that the pubkey is a base64 encoded string, but the private key is
        /// an unencoded byte array
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_key_from_private", ExactSpelling = true)]
        internal static extern uint olm_pk_key_from_private(
            IntPtr decryption,
            byte[] pubkey,
            uint pubkey_length,
            byte[] privkey,
            uint privkey_length);

        /// <summary>
        /// DEPRECATED: Use olm_pk_key_from_private
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pk_generate_key", ExactSpelling = true)]
        internal static extern uint olm_pk_generate_key(
            IntPtr decryption,
            byte[] pubkey,
            uint pubkey_length,
            byte[] privkey,
            uint privkey_length);

        /// <summary>
        /// Returns the number of bytes needed to store a decryption object.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pickle_pk_decryption_length", ExactSpelling = true)]
        internal static extern uint olm_pickle_pk_decryption_length(IntPtr decryption);

        /// <summary>
        /// Stores decryption object as a base64 string. Encrypts the object using the
        /// supplied key. Returns the length of the pickled object on success.
        /// Returns <see cref="GetErrorCode" /> on failure. If the pickle output buffer
        /// is smaller than <see cref="olm_pickle_account_length" /> then
        /// <see cref="olm_pk_decryption_last_error" /> will be  <c>OUTPUT_BUFFER_TOO_SMALL</c>
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pickle_pk_decryption", ExactSpelling = true)]
        internal static extern uint olm_pickle_pk_decryption(
            IntPtr decryption,
            byte[] key,
            uint key_length,
            byte[] pickled,
            uint pickled_length);

        /// <summary>
        /// Loads a decryption object from a pickled base64 string. The associated
        /// public key will be written to the pubkey buffer. Decrypts the object using
        /// the supplied key. Returns <see cref="GetErrorCode" /> on failure. If the key doesn't
        /// match the one used to encrypt the account then <see cref="olm_pk_decryption_last_error" />
        /// will be  <c>BAD_ACCOUNT_KEY</c>. If the base64 couldn't be decoded then
        /// <see cref="olm_pk_decryption_last_error" /> will be "INVALID_BASE64". The input pickled
        /// buffer is destroyed
        /// </summary>
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
        /// <see cref="GetErrorCode" /> on failure. If the plaintext buffer is too small then
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
        /// private_key_length. If the given buffer is too small, returns <see cref="GetErrorCode" />
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
        /// <see cref="GetErrorCode" /> on failure. If the public key buffer is too small then
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
        /// buffer. Returns <see cref="GetErrorCode" /> on failure. If the signature buffer is too
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
