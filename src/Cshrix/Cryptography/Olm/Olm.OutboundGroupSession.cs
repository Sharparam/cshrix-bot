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
    using System.Runtime.InteropServices;

    internal static partial class Olm
    {
        /// <summary>
        /// get the size of an outbound group session, in bytes.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_outbound_group_session_size", ExactSpelling = true)]
        internal static extern uint olm_outbound_group_session_size();

        /// <summary>
        /// Initialise an outbound group session object using the supplied memory
        /// The supplied memory should be at least <see cref="olm_outbound_group_session_size" />
        /// bytes.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_outbound_group_session", ExactSpelling = true)]
        internal static extern IntPtr olm_outbound_group_session(IntPtr memory);

        /// <summary>
        /// A null terminated string describing the most recent error to happen to a
        /// group session
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_outbound_group_session_last_error", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        internal static extern string olm_outbound_group_session_last_error(IntPtr session);

        /// <summary>
        /// Clears the memory used to back this group session
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_clear_outbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_clear_outbound_group_session(IntPtr session);

        /// <summary>
        /// Returns the number of bytes needed to store an outbound group session
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pickle_outbound_group_session_length", ExactSpelling = true)]
        internal static extern uint olm_pickle_outbound_group_session_length(IntPtr session);

        /// <summary>
        /// Stores a group session as a base64 string. Encrypts the session using the
        /// supplied key. Returns the length of the session on success.
        /// Returns <see cref="GetErrorCode" /> on failure. If the pickle output buffer
        /// is smaller than <see cref="olm_pickle_outbound_group_session_length" /> then
        /// <see cref="olm_outbound_group_session_last_error" /> will be  <c>OUTPUT_BUFFER_TOO_SMALL</c>
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_pickle_outbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_pickle_outbound_group_session(
            IntPtr session,
            byte[] key,
            uint key_length,
            byte[] pickled,
            uint pickled_length);

        /// <summary>
        /// Loads a group session from a pickled base64 string. Decrypts the session
        /// using the supplied key.
        /// Returns <see cref="GetErrorCode" /> on failure. If the key doesn't match the one used to
        /// encrypt the account then <see cref="olm_outbound_group_session_last_error" /> will be
        ///  <c>BAD_ACCOUNT_KEY</c>. If the base64 couldn't be decoded then
        /// <see cref="olm_outbound_group_session_last_error" /> will be "INVALID_BASE64". The input
        /// pickled buffer is destroyed
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_unpickle_outbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_unpickle_outbound_group_session(
            IntPtr session,
            byte[] key,
            uint key_length,
            byte[] pickled,
            uint pickled_length);

        /// <summary>
        /// The number of random bytes needed to create an outbound group session
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_init_outbound_group_session_random_length", ExactSpelling = true)]
        internal static extern uint olm_init_outbound_group_session_random_length(IntPtr session);

        /// <summary>
        /// Start a new outbound group session. Returns <see cref="GetErrorCode" /> on failure. On
        /// failure last_error will be set with an error code. The last_error will be
        /// NOT_ENOUGH_RANDOM if the number of random bytes was too small.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_init_outbound_group_session", ExactSpelling = true)]
        internal static extern uint olm_init_outbound_group_session(IntPtr session, byte[] random, uint random_length);

        /// <summary>
        /// The number of bytes that will be created by encrypting a message
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_group_encrypt_message_length", ExactSpelling = true)]
        internal static extern uint olm_group_encrypt_message_length(IntPtr session, uint plaintext_length);

        /// <summary>
        /// Encrypt some plain-text. Returns the length of the encrypted message or
        /// <see cref="GetErrorCode" /> on failure. On failure last_error will be set with an
        /// error code. The last_error will be OUTPUT_BUFFER_TOO_SMALL if the output
        /// buffer is too small.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_group_encrypt", ExactSpelling = true)]
        internal static extern uint olm_group_encrypt(
            IntPtr session,
            byte[] plaintext,
            uint plaintext_length,
            byte[] message,
            uint message_length);

        /// <summary>
        /// Get the number of bytes returned by <see cref="olm_outbound_group_session_id" />
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_outbound_group_session_id_length", ExactSpelling = true)]
        internal static extern uint olm_outbound_group_session_id_length(IntPtr session);

        /// <summary>
        /// Get a base64-encoded identifier for this session.
        /// Returns the length of the session id on success or <see cref="GetErrorCode" /> on
        /// failure. On failure last_error will be set with an error code. The
        /// last_error will be OUTPUT_BUFFER_TOO_SMALL if the id buffer was too
        /// small.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_outbound_group_session_id", ExactSpelling = true)]
        internal static extern uint olm_outbound_group_session_id(IntPtr session, byte[] id, uint id_length);

        /// <summary>
        /// Get the current message index for this session.
        /// Each message is sent with an increasing index; this returns the index for
        /// the next message.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_outbound_group_session_message_index", ExactSpelling = true)]
        internal static extern int olm_outbound_group_session_message_index(IntPtr session);

        /// <summary>
        /// Get the number of bytes returned by <see cref="olm_outbound_group_session_key" />
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_outbound_group_session_key_length", ExactSpelling = true)]
        internal static extern uint olm_outbound_group_session_key_length(IntPtr session);

        /// <summary>
        /// Get the base64-encoded current ratchet key for this session.
        /// Each message is sent with a different ratchet key. This function returns the
        /// ratchet key that will be used for the next message.
        /// Returns the length of the ratchet key on success or <see cref="GetErrorCode" /> on
        /// failure. On failure last_error will be set with an error code. The
        /// last_error will be OUTPUT_BUFFER_TOO_SMALL if the buffer was too small.
        /// </summary>
        [DllImport(Name, EntryPoint = "olm_outbound_group_session_key", ExactSpelling = true)]
        internal static extern uint olm_outbound_group_session_key(IntPtr session, byte[] key, uint key_length);
    }
}
