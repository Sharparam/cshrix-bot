// <copyright file="Olm.InboundGroupSession.cs">
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
        /// Gets the size of an inbound group session, in bytes.
        /// </summary>
        /// <returns>The size in bytes.</returns>
        [DllImport(Name, EntryPoint = "olm_inbound_group_session_size", ExactSpelling = true)]
        internal static extern uint olm_inbound_group_session_size();

        /// <summary>
        /// Initialise an inbound group session object using the supplied memory.
        /// The supplied memory should be at least <see cref="olm_inbound_group_session_size" />
        /// bytes.
        /// </summary>
        /// <param name="memory">A pointer to the memory area.</param>
        /// <returns>A pointer to the initialised inbound group session object.</returns>
        [DllImport(Name, EntryPoint = "olm_inbound_group_session", ExactSpelling = true)]
        internal static extern IntPtr olm_inbound_group_session(IntPtr memory);

        /// <summary>
        /// Gets a null terminated string describing the most recent error to happen to a group session.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <returns>The most recent error.</returns>
        [DllImport(Name, EntryPoint = "olm_inbound_group_session_last_error", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(OlmStringMarshaler))]
        internal static extern string olm_inbound_group_session_last_error(IntPtr session);

        /// <summary>
        /// Clears the memory used to back this group session.
        /// </summary>
        /// <param name="session">A pointer to the session to clear.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_clear_inbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_clear_inbound_group_session(IntPtr session);

        /// <summary>
        /// Returns the number of bytes needed to store an inbound group session.
        /// </summary>
        /// <param name="session">A pointer to the session that should be stored.</param>
        /// <returns>The number of bytes needed to store the session.</returns>
        [DllImport(Name, EntryPoint = "olm_pickle_inbound_group_session_length", ExactSpelling = true)]
        internal static extern uint olm_pickle_inbound_group_session_length(IntPtr session);

        /// <summary>
        /// Stores a group session as a Base64 string.
        /// Encrypts the session using the supplied <paramref name="key" />.
        /// </summary>
        /// <param name="session">A pointer to the session to store.</param>
        /// <param name="key">The key to encrypt with.</param>
        /// <param name="key_length">Length of <paramref name="key" />.</param>
        /// <param name="pickled">Output buffer to store the pickled session in.</param>
        /// <param name="pickled_length">Length of <paramref name="pickled" />.</param>
        /// <returns>
        /// The length of the session on success.
        /// The value of <see cref="olm_error" /> on failure.
        /// </returns>
        /// <remarks>
        /// If the pickle output buffer is smaller than <see cref="olm_pickle_inbound_group_session_length" />
        /// then <see cref="olm_inbound_group_session_last_error" /> will return <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_pickle_inbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_pickle_inbound_group_session(
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
        /// <param name="pickled">The pickled data to load from.</param>
        /// <param name="pickled_length">Length of <paramref name="pickled" />.</param>
        /// <returns>The value of <see cref="olm_error" /> on failure.</returns>
        /// <remarks>
        /// If the key doesn't match the one used to encrypt the account then
        /// <see cref="olm_inbound_group_session_last_error" /> will return <c>BAD_ACCOUNT_KEY</c>.
        /// If the base64 couldn't be decoded then <see cref="olm_inbound_group_session_last_error" />
        /// will return <c>INVALID_BASE64</c>.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_unpickle_inbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_unpickle_inbound_group_session(
            IntPtr session,
            byte[] key,
            uint key_length,
            byte[] pickled,
            uint pickled_length);

        /// <summary>
        /// Start a new inbound group session from a key exported from <see cref="olm_outbound_group_session_key" />.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="session_key">A session key.</param>
        /// <param name="session_key_length">Length of <paramref name="session_key" />.</param>
        /// <returns>The value of <see cref="olm_error" /> on failure.</returns>
        /// <remarks>
        /// On failure <c>last_error</c> will be set with an error code.
        /// The value returned from <see cref="olm_inbound_group_session_last_error" /> will be:
        ///  * <c>OLM_INVALID_BASE64</c> if the session_key is not valid base64
        ///  * <c>OLM_BAD_SESSION_KEY</c> if the session_key is invalid
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_init_inbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_init_inbound_group_session(
            IntPtr session,
            byte[] session_key,
            uint session_key_length);

        /// <summary>
        /// Import an inbound group session from a previous export.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="session_key">
        /// A session key. Note that it will be overwritten with the Base64-decoded data.
        /// </param>
        /// <param name="session_key_length">Length of <paramref name="session_key" />.</param>
        /// <returns>The value of <see cref="olm_error" /> on failure.</returns>
        /// <remarks>
        /// On failure <c>last_error</c> will be set with an error code.
        /// The value returned from <see cref="olm_inbound_group_session_last_error" /> will be:
        ///  * <c>OLM_INVALID_BASE64</c> if the session_key is not valid base64
        ///  * <c>OLM_BAD_SESSION_KEY</c> if the session_key is invalid
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_import_inbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_import_inbound_group_session(
            IntPtr session,
            byte[] session_key,
            uint session_key_length);

        /// <summary>
        /// Gets an upper bound on the number of bytes of plain-text the decrypt method will write
        /// for a given input message length.
        /// The actual size could be different due to padding.
        /// The input message buffer is destroyed.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="message">The message to analyze.</param>
        /// <param name="message_length">Length of <paramref name="message" />.</param>
        /// <returns>The value of <see cref="olm_error" /> on failure.</returns>
        [DllImport(Name, EntryPoint = "olm_group_decrypt_max_plaintext_length", ExactSpelling = true)]
        internal static extern uint olm_group_decrypt_max_plaintext_length(
            IntPtr session,
            byte[] message,
            uint message_length);

        /// <summary>
        /// Decrypts a message.
        /// The input message buffer is destroyed.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="message">
        /// The input message to decrypt. Note that it will be overwritten with the Base64-decoded message.
        /// </param>
        /// <param name="message_length">Length of <paramref name="message" />.</param>
        /// <param name="plaintext">Output buffer for the decrypted message.</param>
        /// <param name="max_plaintext_length">Maximum length for <paramref name="plaintext" />.</param>
        /// <param name="message_index">A pointer to the message index.</param>
        /// <returns>
        /// The length of the decrypted plain-text, or the value of <see cref="olm_error" /> on failure.
        /// </returns>
        /// <remarks>
        /// On failure <c>last_error</c> will be set with an error code.
        /// The value returned from <see cref="olm_inbound_group_session_last_error" /> will be:
        ///  * <c>OLM_OUTPUT_BUFFER_TOO_SMALL</c> if the plain-text buffer is too small
        ///  * <c>OLM_INVALID_BASE64</c> if the message is not valid base-64
        ///  * <c>OLM_BAD_MESSAGE_VERSION</c> if the message was encrypted with an unsupported version of the protocol
        ///  * <c>OLM_BAD_MESSAGE_FORMAT</c> if the message headers could not be decoded
        ///  * <c>OLM_BAD_MESSAGE_MAC</c> if the message could not be verified
        ///  * <c>OLM_UNKNOWN_MESSAGE_INDEX</c> if we do not have a session key corresponding to the message's index
        ///    (ie, it was sent before the session key was shared with us)
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_group_decrypt", ExactSpelling = true)]
        internal static extern uint olm_group_decrypt(
            IntPtr session,
            byte[] message,
            uint message_length,
            byte[] plaintext,
            uint max_plaintext_length,
            IntPtr message_index);

        /// <summary>
        /// Gets the number of bytes returned by <see cref="olm_inbound_group_session_id" />.
        /// </summary>
        /// <returns>The number of bytes.</returns>
        [DllImport(Name, EntryPoint = "olm_inbound_group_session_id_length", ExactSpelling = true)]
        internal static extern uint olm_inbound_group_session_id_length(IntPtr session);

        /// <summary>
        /// Gets a Base64-encoded identifier for this session.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="id">The output buffer to write the ID to.</param>
        /// <param name="id_length">Length of <paramref name="id" />.</param>
        /// <returns>
        /// The length of the session id on success
        /// or the value of <see cref="olm_error" /> on failure.
        /// </returns>
        /// <remarks>
        /// On failure <c>last_error</c> will be set with an error code.
        /// The return value of <see cref="olm_inbound_group_session_last_error" />
        /// will be <c>OUTPUT_BUFFER_TOO_SMALL</c> if the ID buffer was too small.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_inbound_group_session_id", ExactSpelling = true)]
        internal static extern uint olm_inbound_group_session_id(IntPtr session, byte[] id, uint id_length);

        /// <summary>
        /// Gets the first message index we know how to decrypt.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <returns>The first message index.</returns>
        [DllImport(Name, EntryPoint = "olm_inbound_group_session_first_known_index", ExactSpelling = true)]
        internal static extern int olm_inbound_group_session_first_known_index(IntPtr session);

        /// <summary>
        /// Checks if the session has been verified as a valid session.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <returns><c>true</c> if the session has been verified; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// A session is verified either because the original session share was signed,
        /// or because we have subsequently successfully decrypted a message.
        /// This is mainly intended for the unit tests, currently.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_inbound_group_session_is_verified", ExactSpelling = true)]
        internal static extern bool olm_inbound_group_session_is_verified(IntPtr session);

        /// <summary>
        /// Gets the number of bytes returned by <see cref="olm_export_inbound_group_session" />.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <returns>The number of bytes.</returns>
        [DllImport(Name, EntryPoint = "olm_export_inbound_group_session_length", ExactSpelling = true)]
        internal static extern uint olm_export_inbound_group_session_length(IntPtr session);

        /// <summary>
        /// Exports the Base64-encoded ratchet key for this session, at the given index,
        /// in a format which can be used by <see cref="olm_import_inbound_group_session" />.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="key">The key to export.</param>
        /// <param name="key_length">Length of <paramref name="key" />.</param>
        /// <param name="message_index">Message index.</param>
        /// <returns>
        /// The length of the ratchet key on success or the value of <see cref="olm_error" /> on failure.
        /// </returns>
        /// <remarks>
        /// On failure <c>last_error</c> will be set with an error code.
        /// The value returned by <see cref="olm_inbound_group_session_last_error" /> will be:
        ///  * <c>OUTPUT_BUFFER_TOO_SMALL</c> if the buffer was too small
        ///  * <c>OLM_UNKNOWN_MESSAGE_INDEX</c> if we do not have a session key corresponding to the given index
        ///    (ie, it was sent before the session key was shared with us)
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_export_inbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_export_inbound_group_session(
            IntPtr session,
            byte[] key,
            uint key_length,
            int message_index);
    }
}
