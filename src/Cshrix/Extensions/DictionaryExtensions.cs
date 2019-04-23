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

    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dict,
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

        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, object> dict,
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

            var hasValue = dict.TryGetValue<TKey, TValue>(key, out var value);
            return hasValue ? value : @default;
        }

        public static bool TryGetValue<TKey, TValue>(
            this IDictionary<TKey, object> dict,
            [NotNull] TKey key,
            out TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var hasValue = dict.TryGetValue(key, out var objValue);

            if (hasValue && objValue is TValue t)
            {
                value = t;
                return true;
            }

            value = default;
            return false;
        }
    }
}
