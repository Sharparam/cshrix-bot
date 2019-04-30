// <copyright file="UnauthorizedError.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Errors
{
    using System.Collections.Generic;

    using Data.Authentication;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <inheritdoc />
    /// <summary>
    /// Represents the error returned when an API requires additional authorization.
    /// </summary>
    public sealed class UnauthorizedError : MatrixError
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedError" /> class.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">A description of the error.</param>
        /// <param name="completed">Authorization steps that have been completed.</param>
        /// <param name="flows">Available authentication flows.</param>
        /// <param name="parameters">Parameters for the flows, if any.</param>
        /// <param name="session">Session identifier.</param>
        public UnauthorizedError(
            string code,
            string message,
            [CanBeNull] IReadOnlyCollection<string> completed,
            IReadOnlyCollection<AuthenticationFlow> flows,
            IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> parameters,
            string session)
            : base(code, message)
        {
            Completed = completed;
            Flows = flows;
            Parameters = parameters;
            Session = session;
        }

        /// <summary>
        /// Gets a collection of flow identifiers that have been completed.
        /// </summary>
        [JsonProperty("completed")]
        [CanBeNull]
        public IReadOnlyCollection<string> Completed { get; }

        /// <summary>
        /// Gets a collection of available flows to use for authentication.
        /// </summary>
        [JsonProperty("flows")]
        public IReadOnlyCollection<AuthenticationFlow> Flows { get; }

        /// <summary>
        /// Gets a dictionary mapping flow identifiers to the parameters they require, if any.
        /// </summary>
        [JsonProperty("params")]
        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> Parameters { get; }

        /// <summary>
        /// Gets a session identifier for this authentication attempt.
        /// </summary>
        /// <remarks>
        /// This identifier must be sent with each authentication attempt to identify the user and attempt.
        /// </remarks>
        [JsonProperty("session")]
        public string Session { get; }

        /// <summary>
        /// Gets a value indicating whether this error is actually a failure.
        /// </summary>
        /// <remarks>
        /// Due to how the Matrix API is designed, a <c>401</c> return from the server is not necessarily
        /// a client failure. If the error is thrown with no values assigned to <see cref="MatrixError.Code" />
        /// and <see cref="MatrixError.Message" />, then the error is simply indicating the steps needed to
        /// authenticate.
        /// If this value is <c>true</c> it means the authentication attempt has actually failed and the client
        /// needs to retry from the beginning.
        /// </remarks>
        public bool IsFailure => Code != default || Message != default;
    }
}
