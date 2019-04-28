// <copyright file="MatrixError.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Errors
{
    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// Contains basic error data returned from Matrix APIs.
    /// </summary>
    [JsonConverter(typeof(MatrixErrorConverter))]
    public class MatrixError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixError" /> class.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">A description of the error.</param>
        public MatrixError([CanBeNull] string code, [CanBeNull] string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Gets the error code of this error.
        /// </summary>
        [JsonProperty("errcode")]
        [CanBeNull]
        public string Code { get; }

        /// <summary>
        /// Gets a description of this error.
        /// </summary>
        [JsonProperty("error")]
        [CanBeNull]
        public string Message { get; }

        /// <inheritdoc />
        public override string ToString() => $"{Code}: {Message}";
    }
}
