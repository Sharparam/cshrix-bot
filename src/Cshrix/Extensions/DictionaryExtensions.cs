// <copyright file="DictionaryExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Extensions
{
    using System;
    using System.Collections.Generic;

    using JetBrains.Annotations;

    /// <summary>
    /// Contains extension methods for the <see cref="IDictionary{TKey, TValue}" /> interface.
    /// </summary>
    internal static class DictionaryExtensions
    {
        /// <summary>
        /// Gets the value associated with the specified key, or <paramref name="default" />
        /// if the key does not exist.
        /// </summary>
        /// <param name="dict">The dictionary to get the value from.</param>
        /// <param name="key">The key to look up.</param>
        /// <param name="default">Default value to return if the key is not found.</param>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <returns>
        /// The value specified by <paramref name="key" /> in the dictionary,
        /// or <paramref name="default" /> if it was not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="dict" /> and/or <paramref name="key" /> is <c>null</c>.
        /// </exception>
        internal static TValue GetValueOrDefault<TKey, TValue>(
            [NotNull] this IDictionary<TKey, TValue> dict,
            [NotNull] TKey key,
            TValue @default = default)
        {
            if (dict == null)
            {
                throw new ArgumentNullException(nameof(dict));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var hasValue = dict.TryGetValue(key, out var value);
            return hasValue ? value : @default;
        }
    }
}
