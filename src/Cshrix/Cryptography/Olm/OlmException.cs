// <copyright file="OlmException.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Cryptography.Olm
{
    using System;
    using System.Runtime.Serialization;

    using JetBrains.Annotations;

    /// <summary>
    /// Represents errors that occur during calls to the Olm library.
    /// </summary>
    [Serializable]
    public class OlmException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="OlmException" /> class.
        /// </summary>
        /// <param name="code">The result code returned by Olm.</param>
        /// <param name="message">The error message given by Olm.</param>
        public OlmException(uint code, string message)
            : base(message) =>
            Code = code;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="OlmException" /> class.
        /// </summary>
        /// <param name="code">The result code returned by Olm.</param>
        /// <param name="message">The error message given by Olm.</param>
        /// <param name="innerException">The inner exception.</param>
        public OlmException(uint code, string message, Exception innerException)
            : base(message, innerException) =>
            Code = code;

        /// <summary>
        /// Initializes a new instance of the <see cref="OlmException" /> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext" /> that contains contextual information about the source or destination.
        /// </param>
        protected OlmException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context) =>
            Code = info.GetUInt32(nameof(Code));

        /// <summary>
        /// Gets the result code returned by the Olm function.
        /// </summary>
        public uint Code { get; }

        /// <summary>
        /// Sets the <see cref="SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext" /> that contains contextual information about the source or destination.
        /// </param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Code), Code);
        }

        /// <summary>
        /// Creates and returns a string representation of the current exception.
        /// </summary>
        /// <returns>A string representation of the current exception.</returns>
        public override string ToString()
        {
            if (Message == null)
            {
                return Code.ToString();
            }

            return $"{Code}: {Message}";
        }
    }
}
