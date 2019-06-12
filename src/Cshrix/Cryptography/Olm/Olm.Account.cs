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
    using System.Runtime.InteropServices;

    internal static partial class Olm
    {
        /// <summary>
        /// Gets the size of an <c>Account</c> object in bytes.
        /// </summary>
        /// <returns>The size in bytes.</returns>
        [DllImport(Name, EntryPoint = "olm_account_size", ExactSpelling = true)]
        internal static extern uint GetAccountSize();

        /// <summary>
        /// Initializes a new <c>Account</c> object using the supplied memory.
        /// The supplied memory must be at least <see cref="GetAccountSize" /> bytes.
        /// </summary>
        /// <param name="memory">Pointer to a location in memory where <c>Account</c> should be initialized.</param>
        /// <returns>A pointer to the initialized <c>Account</c>.</returns>
        [DllImport(Name, EntryPoint = "olm_account", ExactSpelling = true)]
        internal static extern IntPtr InitializeAccount(IntPtr memory);

        /// <summary>
        /// Gets a string describing the most recent error to happen to account.
        /// </summary>
        /// <param name="account">A pointer to a previously created instance of <c>OlmAccount</c>.</param>
        /// <returns>A string describing the error.</returns>
        [DllImport(Name, EntryPoint = "olm_account_last_error", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        internal static extern string GetLastAccountError(IntPtr account);

        /// <summary>
        /// Clears the memory used to back an <c>Account</c>.
        /// </summary>
        /// <param name="account">A pointer to an <c>Account</c> object.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_clear_account", ExactSpelling = true)]
        internal static extern uint ClearAccount(IntPtr account);

        /// <summary>
        /// Gets the number of bytes needed to store an <c>Account</c>.
        /// </summary>
        /// <param name="account">A pointer to the <c>Account</c> that should be stored.</param>
        /// <returns>The number of bytes required to store the <c>Account</c>.</returns>
        [DllImport(Name, EntryPoint = "olm_pickle_account_length", ExactSpelling = true)]
        internal static extern uint GetAccountPickleLength(IntPtr account);

        /// <summary>
        /// Stores an account as a Base64 string. Encrypts the account using the supplied key.
        /// Returns the length of the pickled account on success, else the value of <see cref="GetErrorCode" />.
        /// </summary>
        /// <param name="account">The <c>Account</c> to pickle.</param>
        /// <param name="key">The key to use for encrypting.</param>
        /// <param name="keyLength">The length of <paramref name="key" />.</param>
        /// <param name="pickled">The output buffer for the pickled result.</param>
        /// <param name="pickledLength">The length of <paramref name="pickled" />.</param>
        /// <returns>
        /// The length of the pickled account on success, otherwise the value of <see cref="GetErrorCode" />.
        /// </returns>
        /// <remarks>
        /// If the pickle output buffer <paramref name="pickled" /> is smaller than the value of
        /// <see cref="GetAccountPickleLength" /> then <see cref="GetLastAccountError" /> will be
        /// <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_pickle_account", ExactSpelling = true)]
        internal static extern uint PickleAccount(
            IntPtr account,
            byte[] key,
            uint keyLength,
            byte[] pickled,
            uint pickledLength);

        /// <summary>
        /// Loads an account from a pickled Base64 string. Decrypts the account using the supplied key.
        /// Returns the value of <see cref="GetErrorCode" /> on failure. If the key doesn't match the one used to
        /// encrypt the account then the value of <see cref="GetLastAccountError" /> will be <c>BAD_ACCOUNT_KEY</c>.
        /// If the Base64 couldn't be decoded then the value of <see cref="GetLastAccountError" /> will be
        /// <c>INVALID_BASE64</c>. The input pickled buffer (<paramref name="pickled" />) is destroyed.
        /// </summary>
        /// <param name="account">The pointer to load an account into.</param>
        /// <param name="key">The key used for decryption.</param>
        /// <param name="keyLength">The length of <paramref name="key" />.</param>
        /// <param name="pickled">The pickled data to load from.</param>
        /// <param name="pickledLength">The length of <paramref name="pickled" />.</param>
        /// <returns>The value of <see cref="GetErrorCode" /> on failure.</returns>
        [DllImport(Name, EntryPoint = "olm_unpickle_account", ExactSpelling = true)]
        internal static extern uint UnpickleAccount(
            IntPtr account,
            byte[] key,
            uint keyLength,
            byte[] pickled,
            uint pickledLength);

        /// <summary>
        /// Gets the number of random bytes needed to create an account.
        /// </summary>
        /// <param name="account">Pointer to an initialized <c>Account</c>.</param>
        /// <returns>The number of random bytes needed.</returns>
        [DllImport(Name, EntryPoint = "olm_create_account_random_length", ExactSpelling = true)]
        internal static extern uint GetAccountCreationRandomLength(IntPtr account);

        /// <summary>
        /// Creates a new account. Returns the value of <see cref="GetErrorCode" /> on failure.
        /// If there wasn't enough random bytes then <see cref="GetLastAccountError" /> will return
        /// <c>NOT_ENOUGH_RANDOM</c>.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="random"></param>
        /// <param name="randomLength"></param>
        /// <returns></returns>
        [DllImport(Name, EntryPoint = "olm_create_account", ExactSpelling = true)]
        internal static extern uint CreateAccount(IntPtr account, byte[] random, uint randomLength);

        /// <summary>
        /// Gets the size of the output buffer needed to hold the identity keys.
        /// </summary>
        /// <param name="account">Pointer to an account.</param>
        /// <returns>The size of the output buffer needed.</returns>
        [DllImport(Name, EntryPoint = "olm_account_identity_keys_length", ExactSpelling = true)]
        internal static extern uint GetAccountIdentityKeysLength(IntPtr account);

        /// <summary>
        /// Writes the public parts of the identity keys for the account into the <paramref name="identityKeys" />
        /// output buffer. Returns the value of <see cref="GetErrorCode" /> on failure. If the buffer was too small,
        /// then <see cref="GetLastAccountError" /> will return <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="identityKeys"></param>
        /// <param name="identityKeysLength"></param>
        /// <returns></returns>
        [DllImport(Name, EntryPoint = "olm_account_identity_keys", ExactSpelling = true)]
        internal static extern uint WriteAccountIdentityKeys(
            IntPtr account,
            byte[] identityKeys,
            uint identityKeysLength);

        /// <summary>
        /// Gets the length of an Ed25519 signature encoded as Base64.
        /// </summary>
        /// <param name="account">Pointer to an account.</param>
        /// <returns>The length of an Ed25519 signature encoded as Base64.</returns>
        [DllImport(Name, EntryPoint = "olm_account_signature_length", ExactSpelling = true)]
        internal static extern uint GetAccountSignatureLength(IntPtr account);

        /// <summary>
        /// Signs a message with the Ed25519 key for the specified account.
        /// Returns the value of <see cref="GetErrorCode" /> on failure.
        /// If the signature buffer was too small then <see cref="GetLastAccountError" /> will return
        /// <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </summary>
        /// <param name="account">Pointer to the account to sign with.</param>
        /// <param name="message">The message to sign.</param>
        /// <param name="messageLength">Length of <paramref name="message" />.</param>
        /// <param name="signature">The signature to sign with.</param>
        /// <param name="signatureLength">Length of <paramref name="signature" />.</param>
        /// <returns>The value of <see cref="GetErrorCode" /> on failure.</returns>
        [DllImport(Name, EntryPoint = "olm_account_sign", ExactSpelling = true)]
        internal static extern uint AccountSign(
            IntPtr account,
            byte[] message,
            uint messageLength,
            byte[] signature,
            uint signatureLength);

        /// <summary>
        /// Gets the size of the output buffer needed to hold the one time keys.
        /// </summary>
        /// <param name="account">Pointer to an account.</param>
        /// <returns>The size of the output buffer needed.</returns>
        [DllImport(Name, EntryPoint = "olm_account_one_time_keys_length", ExactSpelling = true)]
        internal static extern uint GetAccountOneTimeKeysLength(IntPtr account);

        /// <summary>
        /// Writes the public parts of the unpublished one time keys for the account
        /// into <paramref name="oneTimeKeys" />.
        /// </summary>
        /// <param name="account">Pointer to the account.</param>
        /// <param name="oneTimeKeys">Buffer to store the generated one time keys in.</param>
        /// <param name="oneTimeKeysLength">Length of <paramref name="oneTimeKeys" />.</param>
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
        /// Returns the value of <see cref="GetErrorCode" /> on failure.
        /// </para>
        /// <para>
        /// If <paramref name="oneTimeKeys" /> was too small then <see cref="GetLastAccountError" />
        /// will return <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </para>
        /// </remarks>
        /// <returns>The value of <see cref="GetErrorCode" /> on failure.</returns>
        [DllImport(Name, EntryPoint = "olm_account_one_time_keys", ExactSpelling = true)]
        internal static extern uint WriteAccountOneTimeKeys(
            IntPtr account,
            byte[] oneTimeKeys,
            uint oneTimeKeysLength);

        /// <summary>
        /// Marks the current set of one time keys as being published.
        /// </summary>
        /// <param name="account">Pointer to the account.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_account_mark_keys_as_published", ExactSpelling = true)]
        internal static extern uint MarkAccountKeysPublished(IntPtr account);

        /// <summary>
        /// Gets the largest number of one time keys this account can store.
        /// </summary>
        /// <param name="account">Pointer to the account.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_account_max_number_of_one_time_keys", ExactSpelling = true)]
        internal static extern uint GetAccountMaxOneTimeKeysCount(IntPtr account);

        /// <summary>
        /// Gets the number of random bytes needed to generate a given number of new one time keys.
        /// </summary>
        /// <param name="account">A pointer to an account.</param>
        /// <param name="count">The number of one time keys that are to be created.</param>
        /// <returns>The number of random bytes required to generate <paramref name="count" /> one time keys.</returns>
        [DllImport(Name, EntryPoint = "olm_account_generate_one_time_keys_random_length", ExactSpelling = true)]
        internal static extern uint GetAccountOneTimeKeysGenerationRandomLength(
            IntPtr account,
            uint count);

        /// <summary>
        /// Generates a number of new one time keys. If the total number of keys stored
        /// by the account exceeds the value returned by <see cref="GetAccountMaxOneTimeKeysCount" /> then the
        /// old keys are discarded. Returns the value of <see cref="GetErrorCode" /> on error.
        /// If the number of random bytes is too small then <see cref="GetLastAccountError" /> will return
        /// <c>NOT_ENOUGH_RANDOM</c>.
        /// </summary>
        /// <param name="account">A pointer to the account.</param>
        /// <param name="keysCount">The number of keys to generate.</param>
        /// <param name="random">Random bytes to use.</param>
        /// <param name="randomLength">The length of <paramref name="random" />.</param>
        [DllImport(Name, EntryPoint = "olm_account_generate_one_time_keys", ExactSpelling = true)]
        internal static extern uint GenerateAccountOneTimeKeys(
            IntPtr account,
            uint keysCount,
            byte[] random,
            uint randomLength);

        /// <summary>
        /// Removes the one time keys that the session used from the account.
        /// </summary>
        /// <param name="account">A pointer to the account.</param>
        /// <param name="session">A pointer to the session.</param>
        /// <returns>
        /// The value of <see cref="GetErrorCode" /> on failure.
        /// </returns>
        /// <remarks>
        /// If the account doesn't have any matching one time keys then <see cref="GetLastAccountError" />
        /// will return <c>BAD_MESSAGE_KEY_ID</c>.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_remove_one_time_keys", ExactSpelling = true)]
        internal static extern uint RemoveOneTimeKeys(IntPtr account, IntPtr session);
    }
}
