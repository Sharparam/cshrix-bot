// <copyright file="HttpResponseMessageExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Extensions
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Errors;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains extension methods for the <see cref="HttpResponseMessage" /> class.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Deserializes a <see cref="MatrixError" /> from the response message.
        /// </summary>
        /// <param name="message">The <see cref="HttpResponseMessage" /> to read from.</param>
        /// <returns>An instance of <see cref="MatrixError" />.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="message" /> is <c>null</c>.</exception>
        public static async Task<MatrixError> GetErrorAsync([NotNull] this HttpResponseMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var stringContent = await message.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<MatrixError>(stringContent);
            return error;
        }

        /// <summary>
        /// Deserializes a <see cref="MatrixError" /> of type <typeparamref name="T" /> from the response message.
        /// </summary>
        /// <param name="message">The <see cref="HttpResponseMessage" /> to read from.</param>
        /// <typeparam name="T">The type of <see cref="MatrixError" /> to deserialize.</typeparam>
        /// <returns>An instance of <typeparamref name="T" />.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="message" /> is <c>null</c>.</exception>
        public static async Task<T> GetErrorAsync<T>([NotNull] this HttpResponseMessage message) where T : MatrixError
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var stringContent = await message.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<T>(stringContent);
            return error;
        }

        /// <summary>
        /// Attempts to deserialize a <see cref="MatrixError" /> from the response message.
        /// </summary>
        /// <param name="message">The <see cref="HttpResponseMessage" /> to read from.</param>
        /// <returns>
        /// A tuple containing a boolean value indicating success,
        /// and the deserialized <see cref="MatrixError" />, if any.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="message" /> is <c>null</c>.</exception>
        public static async Task<(bool Success, MatrixError Value)> TryGetErrorAsync(
            [NotNull] this HttpResponseMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            try
            {
                var error = await message.GetErrorAsync();
                return (true, error);
            }
            catch (JsonSerializationException)
            {
                return (false, default);
            }
        }

        /// <summary>
        /// Attempts to deserialize a <see cref="MatrixError" /> of type <typeparamref name="T" />
        /// from the response message.
        /// </summary>
        /// <param name="message">The <see cref="HttpResponseMessage" /> to read from.</param>
        /// <typeparam name="T">The type of <see cref="MatrixError" /> to deserialize.</typeparam>
        /// <returns>
        /// A tuple containing a boolean value indicating success,
        /// and the deserialized <typeparamref name="T" />, if any.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="message" /> is <c>null</c>.</exception>
        public static async Task<(bool Success, T Value)> TryGetErrorAsync<T>(
            [NotNull] this HttpResponseMessage message)
            where T : MatrixError
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            try
            {
                var error = await message.GetErrorAsync<T>();
                return (true, error);
            }
            catch (JsonSerializationException)
            {
                return (false, default);
            }
            catch (InvalidCastException)
            {
                return (false, default);
            }
        }
    }
}
