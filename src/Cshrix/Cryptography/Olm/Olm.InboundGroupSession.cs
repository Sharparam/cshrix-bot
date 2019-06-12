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
    using System.Runtime.InteropServices;

    internal static partial class Olm
    {
        /// <summary>
        /// get the size of an inbound group session, in bytes.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_inbound_group_session_size", ExactSpelling = true)]
        internal static extern uint olm_inbound_group_session_size();

        /// <summary>
        /// Initialise an inbound group session object using the supplied memory
        /// The supplied memory should be at least <see cref="olm_inbound_group_session_size" />
        /// bytes.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_inbound_group_session", ExactSpelling = true)]
        internal static extern IntPtr olm_inbound_group_session(IntPtr memory);

        /// <summary>
        /// A null terminated string describing the most recent error to happen to a
        /// group session
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_inbound_group_session_last_error", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        internal static extern string olm_inbound_group_session_last_error(IntPtr session);

        /// <summary>
        /// Clears the memory used to back this group session
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_clear_inbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_clear_inbound_group_session(IntPtr session);

        /// <summary>
        /// Returns the number of bytes needed to store an inbound group session
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pickle_inbound_group_session_length", ExactSpelling = true)]
        internal static extern uint olm_pickle_inbound_group_session_length(IntPtr session);

        /// <summary>
        /// Stores a group session as a base64 string. Encrypts the session using the
        /// supplied key. Returns the length of the session on success.
        /// Returns <see cref="GetErrorCode" /> on failure. If the pickle output buffer
        /// is smaller than <see cref="olm_pickle_inbound_group_session_length" /> then
        /// <see cref="olm_inbound_group_session_last_error" /> will be  <c>OUTPUT_BUFFER_TOO_SMALL</c>
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pickle_inbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_pickle_inbound_group_session(
            IntPtr session,
            byte[] key,
            uint key_length,
            byte[] pickled,
            uint pickled_length);

        /// <summary>
        /// Loads a group session from a pickled base64 string. Decrypts the session
        /// using the supplied key.
        /// Returns <see cref="GetErrorCode" /> on failure. If the key doesn't match the one used to
        /// encrypt the account then <see cref="olm_inbound_group_session_last_error" /> will be
        ///  <c>BAD_ACCOUNT_KEY</c>. If the base64 couldn't be decoded then
        /// <see cref="olm_inbound_group_session_last_error" /> will be "INVALID_BASE64". The input
        /// pickled buffer is destroyed
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_unpickle_inbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_unpickle_inbound_group_session(
            IntPtr session,
            byte[] key,
            uint key_length,
            byte[] pickled,
            uint pickled_length);

        /// <summary>
        /// Start a new inbound group session, from a key exported from
        /// olm_outbound_group_session_key
        /// Returns <see cref="GetErrorCode" /> on failure. On failure last_error will be set with an
        /// error code. The last_error will be:
        ///  * OLM_INVALID_BASE64  if the session_key is not valid base64
        ///  * OLM_BAD_SESSION_KEY if the session_key is invalid
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_init_inbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_init_inbound_group_session(
            IntPtr session, /* base64-encoded keys */
            byte[] session_key,
            uint session_key_length);

        /// <summary>
        /// Import an inbound group session, from a previous export.
        /// Returns <see cref="GetErrorCode" /> on failure. On failure last_error will be set with an
        /// error code. The last_error will be:
        ///  * OLM_INVALID_BASE64  if the session_key is not valid base64
        ///  * OLM_BAD_SESSION_KEY if the session_key is invalid
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_import_inbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_import_inbound_group_session(
            IntPtr session,
            /* base64-encoded keys; note that it will be overwritten with the base64-decoded data. */
            byte[] session_key,
            uint session_key_length);

        /// <summary>
        /// Get an upper bound on the number of bytes of plain-text the decrypt method
        /// will write for a given input message length. The actual size could be
        /// different due to padding.
        /// The input message buffer is destroyed.
        /// Returns <see cref="GetErrorCode" /> on failure.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_group_decrypt_max_plaintext_length", ExactSpelling = true)]
        internal static extern uint olm_group_decrypt_max_plaintext_length(
            IntPtr session,
            byte[] message,
            uint message_length);

        /// <summary>
        /// Decrypt a message.
        /// The input message buffer is destroyed.
        /// Returns the length of the decrypted plain-text, or <see cref="GetErrorCode" /> on failure.
        /// On failure last_error will be set with an error code. The last_error will
        /// be:
        ///  * OLM_OUTPUT_BUFFER_TOO_SMALL if the plain-text buffer is too small
        ///  * OLM_INVALID_BASE64 if the message is not valid base-64
        ///  * OLM_BAD_MESSAGE_VERSION if the message was encrypted with an unsupported version of the protocol
        ///  * OLM_BAD_MESSAGE_FORMAT if the message headers could not be decoded
        ///  * OLM_BAD_MESSAGE_MAC    if the message could not be verified
        ///  * OLM_UNKNOWN_MESSAGE_INDEX  if we do not have a session key corresponding to the message's index (ie, it was sent before the session key was shared with us)
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_group_decrypt", ExactSpelling = true)]
        internal static extern uint olm_group_decrypt(
            IntPtr session,
            /* input; note that it will be overwritten with the base64-decoded message. */
            byte[] message,
            uint message_length,
            /* output */
            byte[] plaintext,
            uint max_plaintext_length,
            /* uint32_t * */
            IntPtr message_index);

        /// <summary>
        /// Get the number of bytes returned by <see cref="olm_inbound_group_session_id" />
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_inbound_group_session_id_length", ExactSpelling = true)]
        internal static extern uint olm_inbound_group_session_id_length(IntPtr session);

        /// <summary>
        /// Get a base64-encoded identifier for this session.
        /// Returns the length of the session id on success or <see cref="GetErrorCode" /> on
        /// failure. On failure last_error will be set with an error code. The
        /// last_error will be OUTPUT_BUFFER_TOO_SMALL if the id buffer was too
        /// small.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_inbound_group_session_id", ExactSpelling = true)]
        internal static extern uint olm_inbound_group_session_id(IntPtr session, byte[] id, uint id_length);

        /// <summary>
        /// Get the first message index we know how to decrypt.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_inbound_group_session_first_known_index", ExactSpelling = true)]
        internal static extern int olm_inbound_group_session_first_known_index(IntPtr session);

        /// <summary>
        /// Check if the session has been verified as a valid session.
        /// (A session is verified either because the original session share was signed,
        /// or because we have subsequently successfully decrypted a message.)
        /// This is mainly intended for the unit tests, currently.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_inbound_group_session_is_verified", ExactSpelling = true)]
        internal static extern bool olm_inbound_group_session_is_verified(IntPtr session);

        /// <summary>
        /// Get the number of bytes returned by <see cref="olm_export_inbound_group_session" />
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_export_inbound_group_session_length", ExactSpelling = true)]
        internal static extern uint olm_export_inbound_group_session_length(IntPtr session);

        /// <summary>
        /// Export the base64-encoded ratchet key for this session, at the given index,
        /// in a format which can be used by olm_import_inbound_group_session
        /// Returns the length of the ratchet key on success or <see cref="GetErrorCode" /> on
        /// failure. On failure last_error will be set with an error code. The
        /// last_error will be:
        ///  * OUTPUT_BUFFER_TOO_SMALL if the buffer was too small
        ///  * OLM_UNKNOWN_MESSAGE_INDEX  if we do not have a session key corresponding to the given index (ie, it was sent before the session key was shared with us)
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_export_inbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_export_inbound_group_session(
            IntPtr session,
            byte[] key,
            uint key_length,
            int message_index);
    }
}
