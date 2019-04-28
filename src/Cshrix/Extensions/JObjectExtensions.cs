// <copyright file="JObjectExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Extensions
{
    using System;

    using JetBrains.Annotations;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Contains extension methods for the <see cref="JObject" /> class.
    /// </summary>
    public static class JObjectExtensions
    {
        /// <summary>
        /// Attempts to deserialize a value of a certain type from a <see cref="JObject" /> specified by a key.
        /// </summary>
        /// <param name="jObject">The <see cref="JObject" /> to read from.</param>
        /// <param name="key">Key identifying the value to retrieve.</param>
        /// <param name="default">Default value to return if the key is not found.</param>
        /// <typeparam name="T">Type of the value being retrieved.</typeparam>
        /// <returns>
        /// An instance of <typeparamref name="T" /> if the key was found; otherwise, <paramref name="default" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="jObject" /> and/or <paramref name="key" /> is <c>null</c>.
        /// </exception>
        public static T ValueOrDefault<T>([NotNull] this JObject jObject, [NotNull] string key, T @default = default)
        {
            if (jObject == null)
            {
                throw new ArgumentNullException(nameof(jObject));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return jObject.TryGetValue<T>(key, out var value) ? value : @default;
        }

        /// <summary>
        /// Attempts to deserialize a value of a certain type from a <see cref="JObject" /> specified by a key.
        /// </summary>
        /// <param name="jObject">The <see cref="JObject" /> to read from.</param>
        /// <param name="key">Key identifying the value to retrieve.</param>
        /// <param name="value">
        /// When this method returns, contains the deserialized value, if the key was found; otherwise,
        /// the default value for <typeparamref name="T" />.
        /// </param>
        /// <typeparam name="T">Type of the value being retrieved.</typeparam>
        /// <returns>
        /// <c>true</c> if the key was found; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="jObject" /> and/or <paramref name="key" /> is <c>null</c>.
        /// </exception>
        public static bool TryGetValue<T>([NotNull] this JObject jObject, [NotNull] string key, out T value)
        {
            if (jObject == null)
            {
                throw new ArgumentNullException(nameof(jObject));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!jObject.ContainsKey(key))
            {
                value = default;
                return false;
            }

            value = jObject.Value<T>(key);
            return true;
        }

        /// <summary>
        /// Gets the <see cref="JToken" /> with the specified key converted to the specified type.
        /// </summary>
        /// <param name="jObject">The <see cref="JObject" /> to read from.</param>
        /// <param name="key">The token key.</param>
        /// <typeparam name="T">The type to convert the token to.</typeparam>
        /// <returns>The converted token value.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="jObject" /> and/or <paramref name="key" /> is <c>null</c>.
        /// </exception>
        public static T Object<T>([NotNull] this JObject jObject, [NotNull] string key)
        {
            if (jObject == null)
            {
                throw new ArgumentNullException(nameof(jObject));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return jObject[key].ToObject<T>();
        }

        /// <summary>
        /// Gets the <see cref="JToken" /> with the specified key converted to the specified type,
        /// or a default value if not found.
        /// </summary>
        /// <param name="jObject">The <see cref="JObject" /> to read from.</param>
        /// <param name="key">The token key.</param>
        /// <param name="default">Value to return if the key is not found.</param>
        /// <typeparam name="T">The type to convert the token to.</typeparam>
        /// <returns>The converted token value, or <paramref name="default" /> if it was not found.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="jObject" /> and/or <paramref name="key" /> is <c>null</c>.
        /// </exception>
        public static T ObjectOrDefault<T>([NotNull] this JObject jObject, [NotNull] string key, T @default = default)
        {
            if (jObject == null)
            {
                throw new ArgumentNullException(nameof(jObject));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return jObject.TryGetObject<T>(key, out var value) ? value : @default;
        }

        /// <summary>
        /// Attempts to get the <see cref="JToken" /> with the specified key converted to the specified type.
        /// </summary>
        /// <param name="jObject">The <see cref="JObject" /> to read from.</param>
        /// <param name="key">The token key.</param>
        /// <param name="value">
        /// When this method returns, contains the converted token value, if the key is found;
        /// otherwise, the default value for <typeparamref name="T" />.
        /// </param>
        /// <typeparam name="T">The type to convert the token to.</typeparam>
        /// <returns><c>true</c> if the token was found; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="jObject" /> and/or <paramref name="key" /> is <c>null</c>.
        /// </exception>
        public static bool TryGetObject<T>([NotNull] this JObject jObject, [NotNull] string key, out T value)
        {
            if (jObject == null)
            {
                throw new ArgumentNullException(nameof(jObject));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!jObject.ContainsKey(key))
            {
                value = default;
                return false;
            }

            value = jObject.Object<T>(key);
            return true;
        }
    }
}
