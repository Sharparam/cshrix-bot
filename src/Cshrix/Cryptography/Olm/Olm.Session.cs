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
        /// Gets the size of a <c>Session</c> object in bytes.
        /// </summary>
        /// <returns>The size in bytes.</returns>
        [DllImport(Name, EntryPoint = "olm_session_size", ExactSpelling = true)]
        internal static extern uint GetSessionSize();

        /// <summary>
        /// Initializes a new <c>Session</c> object using the supplied memory.
        /// The supplied memory must be at least <see cref="GetSessionSize" /> bytes.
        /// </summary>
        /// <param name="memory">Pointer to a location in memory where <c>Session</c> should be initialized.</param>
        /// <returns>A pointer to the initialized <c>Session</c>.</returns>
        [DllImport(Name, EntryPoint = "olm_session", ExactSpelling = true)]
        internal static extern IntPtr InitializeSession(IntPtr memory);

        /// <summary>
        /// Gets a string describing the most recent error to happen to session.
        /// </summary>
        /// <param name="session">A pointer to a previously created instance of <c>OlmSession</c>.</param>
        /// <returns>A string describing the error.</returns>
        [DllImport(Name, EntryPoint = "olm_session_last_error", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        internal static extern string GetLastSessionError(IntPtr session);

        /// <summary>
        /// Clears the memory used to back a <c>Session</c>.
        /// </summary>
        /// <param name="session">A pointer to a <c>Session</c> object.</param>
        /// <returns>Result code.</returns>
        [DllImport(Name, EntryPoint = "olm_clear_session", ExactSpelling = true)]
        internal static extern uint ClearSession(IntPtr session);

        /// <summary>
        /// Gets the number of bytes needed to store a <c>Session</c>.
        /// </summary>
        /// <param name="session">A pointer to the <c>Session</c> that should be stored.</param>
        /// <returns>The number of bytes required to store the <c>Session</c>.</returns>
        [DllImport(Name, EntryPoint = "olm_pickle_session_length", ExactSpelling = true)]
        internal static extern uint GetSessionPickleLength(IntPtr session);

        /// <summary>
        /// Gets the number of random bytes needed to create an outbound session.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <returns>The number of random bytes needed.</returns>
        [DllImport(Name, EntryPoint = "olm_create_outbound_session_random_length", ExactSpelling = true)]
        internal static extern uint GetOutboundSessionRandomLength(IntPtr session);

        /// <summary>
        /// Creates a new out-bound session for sending messages to a given identity key
        /// and one time key.
        /// Returns the value of <see cref="GetErrorCode" /> on failure.
        /// If the keys couldn't be decoded as Base64 then <see cref="GetLastSessionError" /> will return
        /// <c>INVALID_BASE64</c>.
        /// If there weren't enough random bytes then <see cref="GetLastSessionError" /> will return
        /// <c>NOT_ENOUGH_RANDOM</c>.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="account">A pointer to the account.</param>
        /// <param name="identityKey">The identity key.</param>
        /// <param name="identityKeyLength">Length of <paramref name="identityKey" />.</param>
        /// <param name="oneTimeKey">The one time key.</param>
        /// <param name="oneTimeKeyLength">Length of <paramref name="oneTimeKey" />.</param>
        /// <param name="random">Random bytes.</param>
        /// <param name="randomLength">Length of <paramref name="random" />.</param>
        /// <returns>The value of <see cref="GetErrorCode" /> on failure.</returns>
        [DllImport(Name, EntryPoint = "olm_create_outbound_session", ExactSpelling = true)]
        internal static extern uint CreateOutboundSession(
            IntPtr session,
            IntPtr account,
            byte[] identityKey,
            uint identityKeyLength,
            byte[] oneTimeKey,
            uint oneTimeKeyLength,
            byte[] random,
            uint randomLength);

        /// <summary>
        /// Create a new in-bound session for sending/receiving messages from an incoming <c>PRE_KEY</c> message.
        /// Returns the value of <see cref="GetErrorCode" /> on failure.
        /// If the Base64 couldn't be decoded then <see cref="GetLastSessionError" /> will return <c>INVALID_BASE64</c>.
        /// If the message was for an unsupported protocol version then <see cref="GetLastSessionError" />
        /// will return <c>BAD_MESSAGE_VERSION</c>.
        /// If the message couldn't be decoded then then <see cref="GetLastSessionError" /> will return
        /// <c>BAD_MESSAGE_FORMAT</c>.
        /// If the message refers to an unknown one time key then <see cref="GetLastSessionError" /> will
        /// return <c>BAD_MESSAGE_KEY_ID</c>.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="account">A pointer to the account.</param>
        /// <param name="oneTimeKeyMessage">The pre-key message.</param>
        /// <param name="oneTimeKeyMessageLength">Length of <paramref name="oneTimeKeyMessage" />.</param>
        /// <returns>The value of <see cref="GetErrorCode" /> on failure.</returns>
        [DllImport(Name, EntryPoint = "olm_create_inbound_session", ExactSpelling = true)]
        internal static extern uint CreateInboundSession(
            IntPtr session,
            IntPtr account,
            byte[] oneTimeKeyMessage,
            uint oneTimeKeyMessageLength);

        /// <summary>
        /// Create a new in-bound session for sending/receiving messages from an incoming <c>PRE_KEY</c> message.
        /// Returns the value of <see cref="GetErrorCode" /> on failure.
        /// If the base64 couldn't be decoded then <see cref="GetLastSessionError" /> will return <c>INVALID_BASE64</c>.
        /// If the message was for an unsupported protocol version then <see cref="GetLastSessionError" /> will
        /// return <c>BAD_MESSAGE_VERSION</c>.
        /// If the message couldn't be decoded then then <see cref="GetLastSessionError" /> will return
        /// <c>BAD_MESSAGE_FORMAT</c>.
        /// If the message refers to an unknown one time key then <see cref="GetLastSessionError" /> will
        /// return <c>BAD_MESSAGE_KEY_ID</c>.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="account">A pointer to the account.</param>
        /// <param name="identityKey">The identity key.</param>
        /// <param name="identityKeyLength">Length of <paramref name="identityKey" />.</param>
        /// <param name="oneTimeKeyMessage">The pre-key message.</param>
        /// <param name="oneTimeKeyMessageLength">Length of <paramref name="oneTimeKeyMessage" />.</param>
        /// <returns>The value of <see cref="GetErrorCode" /> on failure.</returns>
        [DllImport(Name, EntryPoint = "olm_create_inbound_session_from", ExactSpelling = true)]
        internal static extern uint CreateInboundSessionFrom(
            IntPtr session,
            IntPtr account,
            byte[] identityKey,
            uint identityKeyLength,
            byte[] oneTimeKeyMessage,
            uint oneTimeKeyMessageLength);

        /// <summary>
        /// Gets the length of the buffer needed to return the ID for this session.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <returns>The length of the buffer needed.</returns>
        [DllImport(Name, EntryPoint = "olm_session_id_length", ExactSpelling = true)]
        internal static extern uint GetSessionIdLength(IntPtr session);

        /// <summary>
        /// Gets an identifier for this session. Will be the same for both ends of the
        /// conversation. If the ID buffer is too small then <see cref="GetLastSessionError" />
        /// will return <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="id">A buffer to contain the ID.</param>
        /// <param name="idLength">Length of <paramref name="id" />.</param>
        [DllImport(Name, EntryPoint = "olm_session_id", ExactSpelling = true)]
        internal static extern uint GetSessionId(IntPtr session, byte[] id, uint idLength);

        /// <summary>
        /// Gets a value indicating whether a session has received a message.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <returns><c>true</c> if there is a message; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// The type for the return value on the C side is <c>int</c>, hence we can use the default C#
        /// behaviour that uses 4-byte <c>bool</c>s.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_session_has_received_message", ExactSpelling = true)]
        internal static extern bool HasSessionReceivedMessage(IntPtr session);

        /// <summary>
        /// Checks if the <c>PRE_KEY</c> message is for this in-bound session. This can happen if multiple messages
        /// are sent to this account before this account sends a message in reply.
        /// <paramref name="oneTimeKeyMessage" /> buffer is destroyed.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="oneTimeKeyMessage">The pre-key message.</param>
        /// <param name="oneTimeKeyMessageLength">Length of <paramref name="oneTimeKeyMessage" />.</param>
        /// <returns>
        /// <c>1</c> if the session matches.
        /// <c>0</c> if the session does not match.
        /// The value of <see cref="GetErrorCode" /> on failure.
        /// </returns>
        /// <remarks>
        /// If the Base64 couldn't be decoded then <see cref="GetLastSessionError" /> will return <c>INVALID_BASE64</c>.
        /// If the message was for an unsupported protocol version then <see cref="GetLastSessionError" /> will
        /// return <c>BAD_MESSAGE_VERSION</c>.
        /// If the message couldn't be decoded then then <see cref="GetLastSessionError" /> will
        /// return <c>BAD_MESSAGE_FORMAT</c>.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_matches_inbound_session", ExactSpelling = true)]
        internal static extern uint MessageMatchesInboundSession(
            IntPtr session,
            byte[] oneTimeKeyMessage,
            uint oneTimeKeyMessageLength);

        /// <summary>
        /// Checks if the <c>PRE_KEY</c> message is for this in-bound session. This can happen if multiple messages
        /// are sent to this account before this account sends a message in reply.
        /// The <paramref name="oneTimeKeyMessage" /> buffer is destroyed.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="identityKey">The identity key.</param>
        /// <param name="identityKeyLength">Length of <paramref name="identityKey" />.</param>
        /// <param name="oneTimeKeyMessage">The pre-key message.</param>
        /// <param name="oneTimeKeyMessageLength">Length of <paramref name="oneTimeKeyMessage" />.</param>
        /// <returns>
        /// <c>1</c> if the session matches.
        /// <c>0</c> if the session does not match.
        /// The value of <see cref="GetErrorCode" /> on failure.
        /// </returns>
        /// <remarks>
        /// If the Base64 couldn't be decoded then <see cref="GetLastSessionError" /> will return <c>INVALID_BASE64</c>.
        /// If the message was for an unsupported protocol version then <see cref="GetLastSessionError" /> will
        /// return <c>BAD_MESSAGE_VERSION</c>.
        /// If the message couldn't be decoded then then <see cref="GetLastSessionError" /> will
        /// return <c>BAD_MESSAGE_FORMAT</c>.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_matches_inbound_session_from", ExactSpelling = true)]
        internal static extern uint MessageMatchesInboundSessionFrom(
            IntPtr session,
            byte[] identityKey,
            uint identityKeyLength,
            byte[] oneTimeKeyMessage,
            uint oneTimeKeyMessageLength);
    }
}
