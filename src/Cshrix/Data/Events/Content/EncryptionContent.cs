// <copyright file="EncryptionContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using System;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// Contains content for an event describing how messages sent in a room should be encrypted
    /// (<c>m.room.encryption</c>).
    /// </summary>
    public sealed class EncryptionContent : EventContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EncryptedContent" /> class.
        /// </summary>
        /// <param name="algorithm">Algorithm to use.</param>
        /// <param name="rotationPeriod">Period of time sessions should last before changing.</param>
        /// <param name="rotationPeriodMessages">
        /// Number of messages that can be sent before a session needs changing.
        /// </param>
        public EncryptionContent(string algorithm, TimeSpan rotationPeriod, int rotationPeriodMessages)
        {
            Algorithm = algorithm;
            RotationPeriod = rotationPeriod;
            RotationPeriodMessages = rotationPeriodMessages;
        }

        /// <summary>
        /// Gets the encryption algorithm to be used to encrypt messages sent in the room.
        /// </summary>
        /// <remarks>Must be <c>m.megolm.v1.aes-sha2</c>.</remarks>
        [JsonProperty("algorithm")]
        public string Algorithm { get; }

        /// <summary>
        /// Gets the duration for which sessions should be used before changing.
        /// </summary>
        /// <remarks>A week is the recommended default.</remarks>
        [JsonProperty("rotation_period_ms")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan RotationPeriod { get; }

        /// <summary>
        /// Gets the number of messages that should be sent before changing the session.
        /// </summary>
        /// <remarks><c>100</c> is the recommended default.</remarks>
        [JsonProperty("rotation_period_msgs")]
        public int RotationPeriodMessages { get; }
    }
}
