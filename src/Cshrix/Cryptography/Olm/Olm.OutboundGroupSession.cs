// <copyright file="Olm.OutboundGroupSession.cs">
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
        /// Gets the size of an outbound group session in bytes.
        /// </summary>
        /// <returns>The size of an outbound group session in bytes.</returns>
        [DllImport(Name, EntryPoint = "olm_outbound_group_session_size", ExactSpelling = true)]
        internal static extern uint olm_outbound_group_session_size();

        /// <summary>
        /// Initialises an outbound group session object using the supplied memory
        /// The supplied memory should be at least <see cref="olm_outbound_group_session_size" /> bytes.
        /// </summary>
        /// <param name="memory">A pointer to the area in memory in which to initialise the object.</param>
        /// <returns>A pointer to the initialised object.</returns>
        [DllImport(Name, EntryPoint = "olm_outbound_group_session", ExactSpelling = true)]
        internal static extern IntPtr olm_outbound_group_session(IntPtr memory);

        /// <summary>
        /// A null terminated string describing the most recent error to happen to a group session.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <returns>The most recent error.</returns>
        [DllImport(Name, EntryPoint = "olm_outbound_group_session_last_error", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(OlmStringMarshaler))]
        internal static extern string olm_outbound_group_session_last_error(IntPtr session);

        /// <summary>
        /// Clears the memory used to back this group session.
        /// </summary>
        /// <param name="session">A pointer to the session to clear.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_clear_outbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_clear_outbound_group_session(IntPtr session);

        /// <summary>
        /// Returns the number of bytes needed to store an outbound group session.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <returns>The number of bytes needed.</returns>
        [DllImport(Name, EntryPoint = "olm_pickle_outbound_group_session_length", ExactSpelling = true)]
        internal static extern uint olm_pickle_outbound_group_session_length(IntPtr session);

        /// <summary>
        /// Stores a group session as a Base64 string.
        /// Encrypts the session using the supplied <paramref name="key" />.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="key">The key to encrypt with.</param>
        /// <param name="key_length">Length of <paramref name="key" />.</param>
        /// <param name="pickled">An output buffer to store the pickled session in.</param>
        /// <param name="pickled_length">Length of <paramref name="pickled" />.</param>
        /// <returns>
        /// The length of the session on success.
        /// The value of <see cref="olm_error" /> on failure.
        /// </returns>
        /// <remarks>
        /// If the pickle output buffer is smaller than <see cref="olm_pickle_outbound_group_session_length" />
        /// then <see cref="olm_outbound_group_session_last_error" /> will return <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_pickle_outbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_pickle_outbound_group_session(
            IntPtr session,
            byte[] key,
            uint key_length,
            byte[] pickled,
            uint pickled_length);

        /// <summary>
        /// Loads a group session from a pickled Base64 string.
        /// Decrypts the session using the supplied <paramref name="key" />.
        /// The input pickled buffer is destroyed
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="key">The key to decrypt with.</param>
        /// <param name="key_length">Length of <paramref name="key" />.</param>
        /// <param name="pickled">The pickled session.</param>
        /// <param name="pickled_length">Length of <paramref name="pickled" />.</param>
        /// <returns>The value of <see cref="olm_error" /> on failure.</returns>
        /// <remarks>
        /// If the key doesn't match the one used to encrypt the account then
        /// <see cref="olm_outbound_group_session_last_error" /> will return <c>BAD_ACCOUNT_KEY</c>.
        /// If the base64 couldn't be decoded then <see cref="olm_outbound_group_session_last_error" />
        /// will return <c>INVALID_BASE64</c>.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_unpickle_outbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_unpickle_outbound_group_session(
            IntPtr session,
            byte[] key,
            uint key_length,
            byte[] pickled,
            uint pickled_length);

        /// <summary>
        /// Gets the number of random bytes needed to create an outbound group session.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <returns>The number of bytes needed.</returns>
        [DllImport(Name, EntryPoint = "olm_init_outbound_group_session_random_length", ExactSpelling = true)]
        internal static extern uint olm_init_outbound_group_session_random_length(IntPtr session);

        /// <summary>
        /// Starts a new outbound group session.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="random">Random bytes.</param>
        /// <param name="random_length">Length of <paramref name="random" />.</param>
        /// <returns>The value of <see cref="olm_error" /> on failure.</returns>
        /// <remarks>
        /// On failure <c>last_error</c> will be set with an error code.
        /// <see cref="olm_inbound_group_session_last_error" /> will return <c>NOT_ENOUGH_RANDOM</c>
        /// if the number of random bytes was too small.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_init_outbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_init_outbound_group_session(IntPtr session, byte[] random, uint random_length);

        /// <summary>
        /// Gets the number of bytes that will be created by encrypting a message.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="plaintext_length">The length of the plaintext message.</param>
        /// <returns>The length of the encrypted message.</returns>
        [DllImport(Name, EntryPoint = "olm_group_encrypt_message_length", ExactSpelling = true)]
        internal static extern uint olm_group_encrypt_message_length(IntPtr session, uint plaintext_length);

        /// <summary>
        /// Encrypt some plaintext.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="plaintext">The plaintext to encrypt.</param>
        /// <param name="plaintext_length">Length of <paramref name="plaintext" />.</param>
        /// <param name="message">Output buffer that will contain the encrypted plaintext.</param>
        /// <param name="message_length">Length of <paramref name="message" />.</param>
        /// <returns>The length of the encrypted message or the value of <see cref="olm_error" /> on failure.</returns>
        /// <remarks>
        /// On failure <c>last_error</c> will be set with an error code.
        /// <see cref="olm_outbound_group_session_last_error" /> will return <c>OUTPUT_BUFFER_TOO_SMALL</c>
        /// if the output buffer is too small.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_group_encrypt", ExactSpelling = true)]
        internal static extern uint olm_group_encrypt(
            IntPtr session,
            byte[] plaintext,
            uint plaintext_length,
            byte[] message,
            uint message_length);

        /// <summary>
        /// Gets the number of bytes returned by <see cref="olm_outbound_group_session_id" />.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <returns>The number of bytes.</returns>
        [DllImport(Name, EntryPoint = "olm_outbound_group_session_id_length", ExactSpelling = true)]
        internal static extern uint olm_outbound_group_session_id_length(IntPtr session);

        /// <summary>
        /// Gets a Base64-encoded identifier for this session.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="id">An output buffer to contain the ID.</param>
        /// <param name="id_length">The length of <paramref name="id" />.</param>
        /// <returns>
        /// The length of the session ID on success or the value of <see cref="olm_error" /> on failure.
        /// </returns>
        /// <remarks>
        /// On failure <c>last_error</c> will be set with an error code.
        /// <see cref="olm_outbound_group_session_last_error" />
        /// will return <c>OUTPUT_BUFFER_TOO_SMALL</c> if the id buffer was too small.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_outbound_group_session_id", ExactSpelling = true)]
        internal static extern uint olm_outbound_group_session_id(IntPtr session, byte[] id, uint id_length);

        /// <summary>
        /// Gets the current message index for this session.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <returns>The current message index.</returns>
        /// <remarks>
        /// Each message is sent with an increasing index; this returns the index for the next message.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_outbound_group_session_message_index", ExactSpelling = true)]
        internal static extern int olm_outbound_group_session_message_index(IntPtr session);

        /// <summary>
        /// Gets the number of bytes returned by <see cref="olm_outbound_group_session_key" />.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <returns>The number of bytes.</returns>
        [DllImport(Name, EntryPoint = "olm_outbound_group_session_key_length", ExactSpelling = true)]
        internal static extern uint olm_outbound_group_session_key_length(IntPtr session);

        /// <summary>
        /// Get the Base64-encoded current ratchet key for this session.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="key">An output buffer to contain the key.</param>
        /// <param name="key_length">Length of <paramref name="key" />.</param>
        /// <returns>The length of the ratchet key on success or <see cref="olm_error" /> on failure.</returns>
        /// <remarks>
        /// <para>
        /// Each message is sent with a different ratchet key.
        /// This function returns the ratchet key that will be used for the next message.
        /// </para>
        /// <para>
        /// On failure <c>last_error</c> will be set with an error code.
        /// <see cref="olm_outbound_group_session_last_error" /> will return
        /// <c>OUTPUT_BUFFER_TOO_SMALL</c> if the buffer was too small.
        /// </para>
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_outbound_group_session_key", ExactSpelling = true)]
        internal static extern uint olm_outbound_group_session_key(IntPtr session, byte[] key, uint key_length);
    }
}
