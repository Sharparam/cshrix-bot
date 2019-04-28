// <copyright file="ResponseExtensions.cs">
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
    /// Contains extension methods for the <see cref="Response{T}" /> class.
    /// </summary>
    public static class ResponseExtensions
    {
        /// <summary>
        /// Deserializes a <see cref="MatrixError" /> from the response.
        /// </summary>
        /// <param name="response">The response to read from.</param>
        /// <typeparam name="T">The type contained in the response.</typeparam>
        /// <returns>An instance of <see cref="MatrixError" />.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="response" /> is <c>null</c>.</exception>
        public static MatrixError GetError<T>([NotNull] this Response<T> response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            var error = JsonConvert.DeserializeObject<MatrixError>(response.StringContent);
            return error;
        }

        /// <summary>
        /// Deserializes a <see cref="MatrixError" /> of type <typeparamref name="TError" /> from the response.
        /// </summary>
        /// <param name="response">The response to read from.</param>
        /// <typeparam name="T">The type contained in the response.</typeparam>
        /// <typeparam name="TError">The type of <see cref="MatrixError" /> to deserialize.</typeparam>
        /// <returns>An instance of <typeparamref name="TError" />.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="response" /> is <c>null</c>.</exception>
        public static TError GetError<T, TError>([NotNull] this Response<T> response) where TError : MatrixError
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            var error = JsonConvert.DeserializeObject<TError>(response.StringContent);
            return error;
        }

        /// <summary>
        /// Attempts to deserialize a <see cref="MatrixError" /> from the response.
        /// </summary>
        /// <param name="response">The response to read from.</param>
        /// <param name="error">
        /// When this method returns, contains the deserialized <see cref="MatrixError" />, if successful;
        /// otherwise, the default value for <see cref="MatrixError" />.
        /// </param>
        /// <typeparam name="T">The type contained in the response.</typeparam>
        /// <returns>
        /// <c>true</c> if an error was successfully deserialized; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="response" /> is <c>null</c>.</exception>
        public static bool TryGetError<T>([NotNull] this Response<T> response, out MatrixError error)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            try
            {
                error = response.GetError();
                return true;
            }
            catch (JsonSerializationException)
            {
                error = default;
                return false;
            }
        }

        /// <summary>
        /// Attempts to deserialize a <see cref="MatrixError" />
        /// of type <typeparamref name="TError"/> from the response.
        /// </summary>
        /// <param name="response">The response to read from.</param>
        /// <param name="error">
        /// When this method returns, contains the deserialized <typeparamref name="TError" />, if successful;
        /// otherwise, the default value for <typeparamref name="TError" />.
        /// </param>
        /// <typeparam name="T">The type contained in the response.</typeparam>
        /// <typeparam name="TError">The type of <see cref="MatrixError" /> to deserialize.</typeparam>
        /// <returns>
        /// <c>true</c> if the error was successfully deserialized; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="response" /> is <c>null</c>.</exception>
        public static bool TryGetError<T, TError>([NotNull] this Response<T> response, out TError error)
            where TError : MatrixError
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            try
            {
                error = response.GetError<T, TError>();
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
