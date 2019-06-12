// <copyright file="Olm.Encryption.cs">
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
        /// Gets the type of the next message that <see cref="Encrypt" /> will return.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <returns>
        /// <see cref="MessageType.PreKey" /> if the message will be a <c>PRE_KEY</c> message.
        /// <see cref="MessageType.Message" /> if the message will be a normal message.
        /// The value of <see cref="GetErrorCode" /> on failure.
        /// </returns>
        [DllImport(Name, EntryPoint = "olm_encrypt_message_type", ExactSpelling = true)]
        internal static extern MessageType GetNextEncryptMessageType(IntPtr session);

        /// <summary>
        /// Gets the number of random bytes needed to encrypt the next message.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <returns>The number of random bytes needed.</returns>
        [DllImport(Name, EntryPoint = "olm_encrypt_random_length", ExactSpelling = true)]
        internal static extern uint GetEncryptRandomLength(IntPtr session);

        /// <summary>
        /// Gets the size of the next message in bytes for the given number of plain-text bytes.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="plaintextLength">The length of the plaintext in bytes.</param>
        /// <returns>The size of the next message.</returns>
        [DllImport(Name, EntryPoint = "olm_encrypt_message_length", ExactSpelling = true)]
        internal static extern uint GetNextEncryptMessageLength(IntPtr session, uint plaintextLength);

        /// <summary>
        /// Encrypts a message using the session.
        /// Writes the message as Base64 into the <paramref name="message" /> buffer.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="plaintext">The plaintext to encrypt.</param>
        /// <param name="plaintextLength">Length of <paramref name="plaintext" />.</param>
        /// <param name="random">Random bytes.</param>
        /// <param name="randomLength">Length of <paramref name="random" />.</param>
        /// <param name="message">Output buffer for the encrypted message.</param>
        /// <param name="messageLength">Length of <paramref name="message" />.</param>
        /// <returns>
        /// The length of the message in bytes on success. The value of <see cref="GetErrorCode" /> on failure.
        /// </returns>
        /// <remarks>
        /// If the message buffer is too small then <see cref="GetLastSessionError" />
        /// will return <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// If there weren't enough random bytes then <see cref="GetLastSessionError" />
        /// will return <c>NOT_ENOUGH_RANDOM</c>.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_encrypt", ExactSpelling = true)]
        internal static extern uint Encrypt(
            IntPtr session,
            byte[] plaintext,
            uint plaintextLength,
            byte[] random,
            uint randomLength,
            byte[] message,
            uint messageLength);

        /// <summary>
        /// Gets the maximum number of bytes of plain-text a given message could decode to.
        /// The actual size could be different due to padding.
        /// The <paramref name="message" /> buffer is destroyed.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="messageType">The type of message.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageLength">Length of <paramref name="message" />.</param>
        /// <returns>The value of <see cref="GetErrorCode" /> on failure.</returns>
        /// <remarks>
        /// If the message base64 couldn't be decoded then <see cref="GetLastSessionError" />
        /// will return <c>INVALID_BASE64</c>.
        /// If the message is for an unsupported version of the protocol then <see cref="GetLastSessionError" />
        /// will return <c>BAD_MESSAGE_VERSION</c>.
        /// If the message couldn't be decoded then <see cref="GetLastSessionError" />
        /// will return <c>BAD_MESSAGE_FORMAT</c>.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_decrypt_max_plaintext_length", ExactSpelling = true)]
        internal static extern uint GetDecryptMaxPlaintextLength(
            IntPtr session,
            MessageType messageType,
            byte[] message,
            uint messageLength);

        /// <summary>
        /// Decrypts a message using the session.
        /// The input <paramref name="message" /> buffer is destroyed.
        /// </summary>
        /// <param name="session">A pointer to the session.</param>
        /// <param name="messageType">The type of message being decrypted.</param>
        /// <param name="message">The message contents.</param>
        /// <param name="messageLength">Length of <paramref name="message" />.</param>
        /// <param name="plaintext">Output buffer to store decrypted plaintext in.</param>
        /// <param name="plaintextLength">The length (capacity) of <paramref name="plaintext" />.</param>
        /// <returns>
        /// The length of the plain-text on success.
        /// The value of <see cref="GetErrorCode" /> on failure.
        /// </returns>
        /// <remarks>
        /// If the plain-text buffer is smaller than the value returned by <see cref="GetDecryptMaxPlaintextLength" />
        /// then <see cref="GetLastSessionError" /> will return <c>OUTPUT_BUFFER_TOO_SMALL</c>.
        /// If the Base64 couldn't be decoded then
        /// <see cref="GetLastSessionError" /> will return <c>INVALID_BASE64</c>.
        /// If the message is for an unsupported version of the protocol then <see cref="GetLastSessionError" />
        /// will return <c>BAD_MESSAGE_VERSION</c>.
        /// If the message couldn't be decoded then <see cref="GetLastSessionError" />
        /// will return <c>BAD_MESSAGE_FORMAT</c>.
        /// If the MAC on the message was invalid then <see cref="GetLastSessionError" />
        /// will return <c>BAD_MESSAGE_MAC</c>.
        /// </remarks>
        [DllImport(Name, EntryPoint = "olm_decrypt", ExactSpelling = true)]
        internal static extern uint Decrypt(
            IntPtr session,
            MessageType messageType,
            byte[] message,
            uint messageLength,
            byte[] plaintext,
            uint plaintextLength);
    }
}
