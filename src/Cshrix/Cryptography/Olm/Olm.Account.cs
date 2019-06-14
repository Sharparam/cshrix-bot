// <copyright file="Olm.Account.cs">
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
        /// Gets the size of an <c>Account</c> object in bytes.
        /// </summary>
        /// <returns>The size in bytes.</returns>
        [DllImport(Name, EntryPoint = "olm_account_size", ExactSpelling = true)]
        internal static extern uint olm_account_size();

        /// <summary>
        /// Initializes a new <c>Account</c> object using the supplied memory.
        /// The supplied memory must be at least <see cref="olm_account_size" /> bytes.
        /// </summary>
        /// <param name="memory">Pointer to a location in memory where <c>Account</c> should be initialized.</param>
        /// <returns>A pointer to the initialized <c>Account</c>.</returns>
        [DllImport(Name, EntryPoint = "olm_account", ExactSpelling = true)]
        internal static extern IntPtr olm_account(IntPtr memory);

        /// <summary>
        /// Gets a string describing the most recent error to happen to account.
        /// </summary>
        /// <param name="account">A pointer to a previously created instance of <c>OlmAccount</c>.</param>
        /// <returns>A string describing the error.</returns>
        [DllImport(Name, EntryPoint = "olm_account_last_error", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(OlmStringMarshaler))]
        internal static extern string olm_account_last_error(IntPtr account);

        /// <summary>
        /// Clears the memory used to back an <c>Account</c>.
        /// </summary>
        /// <param name="account">A pointer to an <c>Account</c> object.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_clear_account", ExactSpelling = true)]
        internal static extern uint olm_clear_account(IntPtr account);

        /// <summary>
        /// Gets the number of bytes needed to store an <c>Account</c>.
        /// </summary>
        /// <param name="account">A pointer to the <c>Account</c> that should be stored.</param>
        /// <returns>The number of bytes required to store the <c>Account</c>.</returns>
        [DllImport(Name, EntryPoint = "olm_pickle_account_length", ExactSpelling = true)]
        internal static extern uint olm_pickle_account_length(IntPtr account);

        /// <summary>
        /// Stores an account as a Base64 string. Encrypts the account using the supplied key.
        /// Returns the length of the pickled account on success, else the value of <see cref="olm_error" />.
        /// </summary>
        /// <param name="account">The <c>Account</c> to pickle.</param>
        /// <param name="key">The key to use for encrypting.</param>
        /// <param name="key_length">The length of <paramref name="key" />.</param>
        /// <param name="pickled">The output buffer for the pickled result.</param>
        /// <param name="pickled_length">The length of <paramref name="pickled" />.</param>
        /// <returns>
        /// The length of the pickled account on success, otherwise the value of <see cref="olm_error" />.
        /// </returns>
        /// <remarks>
        /// If the pickle output buffer <paramref name="pickled" /> is smaller than the value of
        /// <see cref="olm_pickle_account_length" /> then <see cref="olm_account_last_error" /> will be
        /// <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_pickle_account", ExactSpelling = true)]
        internal static extern uint olm_pickle_account(
            IntPtr account,
            byte[] key,
            uint key_length,
            byte[] pickled,
            uint pickled_length);

        /// <summary>
        /// Loads an account from a pickled Base64 string. Decrypts the account using the supplied key.
        /// Returns the value of <see cref="olm_error" /> on failure. If the key doesn't match the one used to
        /// encrypt the account then the value of <see cref="olm_account_last_error" /> will be <c>BAD_ACCOUNT_KEY</c>.
        /// If the Base64 couldn't be decoded then the value of <see cref="olm_account_last_error" /> will be
        /// <c>INVALID_BASE64</c>. The input pickled buffer (<paramref name="pickled" />) is destroyed.
        /// </summary>
        /// <param name="account">The pointer to load an account into.</param>
        /// <param name="key">The key used for decryption.</param>
        /// <param name="key_length">The length of <paramref name="key" />.</param>
        /// <param name="pickled">The pickled data to load from.</param>
        /// <param name="pickled_length">The length of <paramref name="pickled" />.</param>
        /// <returns>The value of <see cref="olm_error" /> on failure.</returns>
        [DllImport(Name, EntryPoint = "olm_unpickle_account", ExactSpelling = true)]
        internal static extern uint olm_unpickle_account(
            IntPtr account,
            byte[] key,
            uint key_length,
            byte[] pickled,
            uint pickled_length);

        /// <summary>
        /// Gets the number of random bytes needed to create an account.
        /// </summary>
        /// <param name="account">Pointer to an initialized <c>Account</c>.</param>
        /// <returns>The number of random bytes needed.</returns>
        [DllImport(Name, EntryPoint = "olm_create_account_random_length", ExactSpelling = true)]
        internal static extern uint olm_create_account_random_length(IntPtr account);

        /// <summary>
        /// Creates a new account. Returns the value of <see cref="olm_error" /> on failure.
        /// If there wasn't enough random bytes then <see cref="olm_account_last_error" /> will return
        /// <c>NOT_ENOUGH_RANDOM</c>.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="random"></param>
        /// <param name="random_length"></param>
        /// <returns></returns>
        [DllImport(Name, EntryPoint = "olm_create_account", ExactSpelling = true)]
        internal static extern uint olm_create_account(IntPtr account, byte[] random, uint random_length);

        /// <summary>
        /// Gets the size of the output buffer needed to hold the identity keys.
        /// </summary>
        /// <param name="account">Pointer to an account.</param>
        /// <returns>The size of the output buffer needed.</returns>
        [DllImport(Name, EntryPoint = "olm_account_identity_keys_length", ExactSpelling = true)]
        internal static extern uint olm_account_identity_keys_length(IntPtr account);

        /// <summary>
        /// Writes the public parts of the identity keys for the account into the <paramref name="identity_keys" />
        /// output buffer. Returns the value of <see cref="olm_error" /> on failure. If the buffer was too small,
        /// then <see cref="olm_account_last_error" /> will return <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="identity_keys"></param>
        /// <param name="identity_keys_length"></param>
        /// <returns></returns>
        [DllImport(Name, EntryPoint = "olm_account_identity_keys", ExactSpelling = true)]
        internal static extern uint olm_account_identity_keys(
            IntPtr account,
            byte[] identity_keys,
            uint identity_keys_length);

        /// <summary>
        /// Gets the length of an Ed25519 signature encoded as Base64.
        /// </summary>
        /// <param name="account">Pointer to an account.</param>
        /// <returns>The length of an Ed25519 signature encoded as Base64.</returns>
        [DllImport(Name, EntryPoint = "olm_account_signature_length", ExactSpelling = true)]
        internal static extern uint olm_account_signature_length(IntPtr account);

        /// <summary>
        /// Signs a message with the Ed25519 key for the specified account.
        /// Returns the value of <see cref="olm_error" /> on failure.
        /// If the signature buffer was too small then <see cref="olm_account_last_error" /> will return
        /// <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </summary>
        /// <param name="account">Pointer to the account to sign with.</param>
        /// <param name="message">The message to sign.</param>
        /// <param name="message_length">Length of <paramref name="message" />.</param>
        /// <param name="signature">The signature to sign with.</param>
        /// <param name="signature_length">Length of <paramref name="signature" />.</param>
        /// <returns>The value of <see cref="olm_error" /> on failure.</returns>
        [DllImport(Name, EntryPoint = "olm_account_sign", ExactSpelling = true)]
        internal static extern uint olm_account_sign(
            IntPtr account,
            byte[] message,
            uint message_length,
            byte[] signature,
            uint signature_length);

        /// <summary>
        /// Gets the size of the output buffer needed to hold the one time keys.
        /// </summary>
        /// <param name="account">Pointer to an account.</param>
        /// <returns>The size of the output buffer needed.</returns>
        [DllImport(Name, EntryPoint = "olm_account_one_time_keys_length", ExactSpelling = true)]
        internal static extern uint olm_account_one_time_keys_length(IntPtr account);

        /// <summary>
        /// Writes the public parts of the unpublished one time keys for the account
        /// into <paramref name="one_time_keys" />.
        /// </summary>
        /// <param name="account">Pointer to the account.</param>
        /// <param name="one_time_keys">Buffer to store the generated one time keys in.</param>
        /// <param name="one_time_keys_length">Length of <paramref name="one_time_keys" />.</param>
        /// <remarks>
        /// <para>
        /// The returned data is a JSON-formatted object with the single property
        /// <c>curve25519</c>, which is itself an object mapping key IDs to
        /// Base64-encoded Curve25519 keys. For example:
        /// <code>
        /// {
        ///   curve25519: {
        ///     "AAAAAA": "wo76WcYtb0Vk/pBOdmduiGJ0wIEjW4IBMbbQn7aSnTo",
        ///     "AAAAAB": "LRvjo46L1X2vx69sS9QNFD29HWulxrmW11Up5AfAjgU"
        ///   }
        /// }
        /// </code>
        /// Returns the value of <see cref="olm_error" /> on failure.
        /// </para>
        /// <para>
        /// If <paramref name="one_time_keys" /> was too small then <see cref="olm_account_last_error" />
        /// will return <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </para>
        /// </remarks>
        /// <returns>The value of <see cref="olm_error" /> on failure.</returns>
        [DllImport(Name, EntryPoint = "olm_account_one_time_keys", ExactSpelling = true)]
        internal static extern uint olm_account_one_time_keys(
            IntPtr account,
            byte[] one_time_keys,
            uint one_time_keys_length);

        /// <summary>
        /// Marks the current set of one time keys as being published.
        /// </summary>
        /// <param name="account">Pointer to the account.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_account_mark_keys_as_published", ExactSpelling = true)]
        internal static extern uint olm_account_mark_keys_as_published(IntPtr account);

        /// <summary>
        /// Gets the largest number of one time keys this account can store.
        /// </summary>
        /// <param name="account">Pointer to the account.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_account_max_number_of_one_time_keys", ExactSpelling = true)]
        internal static extern uint olm_account_max_number_of_one_time_keys(IntPtr account);

        /// <summary>
        /// Gets the number of random bytes needed to generate a given number of new one time keys.
        /// </summary>
        /// <param name="account">A pointer to an account.</param>
        /// <param name="number_of_keys">The number of one time keys that are to be created.</param>
        /// <returns>
        /// The number of random bytes required to generate <paramref name="number_of_keys" /> one time keys.
        /// </returns>
        [DllImport(Name, EntryPoint = "olm_account_generate_one_time_keys_random_length", ExactSpelling = true)]
        internal static extern uint olm_account_generate_one_time_keys_random_length(
            IntPtr account,
            uint number_of_keys);

        /// <summary>
        /// Generates a number of new one time keys. If the total number of keys stored
        /// by the account exceeds the value returned by <see cref="olm_account_max_number_of_one_time_keys" /> then the
        /// old keys are discarded. Returns the value of <see cref="olm_error" /> on error.
        /// If the number of random bytes is too small then <see cref="olm_account_last_error" /> will return
        /// <c>NOT_ENOUGH_RANDOM</c>.
        /// </summary>
        /// <param name="account">A pointer to the account.</param>
        /// <param name="number_of_keys">The number of keys to generate.</param>
        /// <param name="random">Random bytes to use.</param>
        /// <param name="random_length">The length of <paramref name="random" />.</param>
        [DllImport(Name, EntryPoint = "olm_account_generate_one_time_keys", ExactSpelling = true)]
        internal static extern uint olm_account_generate_one_time_keys(
            IntPtr account,
            uint number_of_keys,
            byte[] random,
            uint random_length);

        /// <summary>
        /// Removes the one time keys that the session used from the account.
        /// </summary>
        /// <param name="account">A pointer to the account.</param>
        /// <param name="session">A pointer to the session.</param>
        /// <returns>
        /// The value of <see cref="olm_error" /> on failure.
        /// </returns>
        /// <remarks>
        /// If the account doesn't have any matching one time keys then <see cref="olm_account_last_error" />
        /// will return <c>BAD_MESSAGE_KEY_ID</c>.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_remove_one_time_keys", ExactSpelling = true)]
        internal static extern uint olm_remove_one_time_keys(IntPtr account, IntPtr session);
    }
}
