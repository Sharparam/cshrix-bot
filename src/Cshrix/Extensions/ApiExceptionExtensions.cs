// <copyright file="ApiExceptionExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Extensions
{
    using System;

    using Errors;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using RestEase;

    /// <summary>
    /// Contains extension methods for the <see cref="ApiException" /> class.
    /// </summary>
    public static class ApiExceptionExtensions
    {
        /// <summary>
        /// Extracts a <see cref="MatrixError" /> from an <see cref="ApiException" />.
        /// </summary>
        /// <param name="exception">The exception instance to extract the error from.</param>
        /// <returns>An instance of <see cref="MatrixError" />.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception" /> is <c>null</c>.</exception>
        public static MatrixError GetError([NotNull] this ApiException exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            var error = JsonConvert.DeserializeObject<MatrixError>(exception.Content);
            return error;
        }

        /// <summary>
        /// Extracts a <see cref="MatrixError" /> of a specific type from an <see cref="ApiException" />.
        /// </summary>
        /// <param name="exception">The exception instance to extract the error from.</param>
        /// <typeparam name="TError">The type of error to extract.</typeparam>
        /// <returns>An instance of <typeparamref name="TError" />.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception" /> is <c>null</c>.</exception>
        public static TError GetError<TError>([NotNull] this ApiException exception) where TError : MatrixError
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            var error = JsonConvert.DeserializeObject<TError>(exception.Content);
            return error;
        }

        /// <summary>
        /// Attempts to extract a <see cref="MatrixError" /> from an <see cref="ApiException" />.
        /// </summary>
        /// <param name="exception">The exception instance to extract an error from.</param>
        /// <param name="error">
        /// When this method returns, contains the extracted error, if one was found; otherwise,
        /// the default value for <see cref="MatrixError" />.
        /// </param>
        /// <returns><c>true</c> if an error was found; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception" /> is <c>null</c>.</exception>
        public static bool TryGetError([NotNull] this ApiException exception, out MatrixError error)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            try
            {
                error = exception.GetError();
                return true;
            }
            catch (JsonSerializationException)
            {
                error = default;
                return false;
            }
        }

        /// <summary>
        /// Attempts to extract a <see cref="MatrixError" /> of a specific type from an <see cref="ApiException" />.
        /// </summary>
        /// <param name="exception">The exception instance to extract an error from.</param>
        /// <param name="error">
        /// When this method returns, contains the extracted error, if one was found; otherwise,
        /// the default value for <typeparamref name="TError" />.
        /// </param>
        /// <typeparam name="TError">The type of error to extract.</typeparam>
        /// <returns><c>true</c> if an error was found; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception" /> is <c>null</c>.</exception>
        public static bool TryGetError<TError>([NotNull] this ApiException exception, out TError error)
            where TError : MatrixError
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            try
            {
                error = exception.GetError<TError>();
                return true;
            }
            catch (JsonSerializationException)
            {
                error = default;
                return false;
            }
            catch (InvalidCastException)
            {
                error = default;
                return false;
            }
        }
    }
}
