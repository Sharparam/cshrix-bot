// <copyright file="EnumHelpers.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Helpers
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    /// Contains helper methods for working with enums.
    /// </summary>
    internal static class EnumHelpers
    {
        /// <summary>
        /// Contains cached dictionaries with information about enum types and their flags values.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, IReadOnlyDictionary<string, Enum>> NamedFlagsCache =
            new ConcurrentDictionary<Type, IReadOnlyDictionary<string, Enum>>();

        /// <summary>
        /// Gets all values from a flags enum that have an <see cref="EnumMemberAttribute" /> specified.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum to examine.</typeparam>
        /// <returns>A dictionary mapping EnumMember-names to their enum value.</returns>
        internal static IReadOnlyDictionary<string, Enum> GetNamedFlags<TEnum>() where TEnum : Enum =>
            GetNamedFlags(typeof(TEnum));

        /// <summary>
        /// Gets all values from a flags enum that have an <see cref="EnumMemberAttribute" /> specified.
        /// </summary>
        /// <param name="type">The type of the enum to examine.</param>
        /// <returns>A dictionary mapping EnumMember-names to their enum value.</returns>
        internal static IReadOnlyDictionary<string, Enum> GetNamedFlags(Type type)
        {
            if (NamedFlagsCache.TryGetValue(type, out var named))
            {
                return named;
            }

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

            IEnumerable<(FieldInfo Info, EnumMemberAttribute Attr)> withAttrs =
                fields.Select(f => (f, f.GetCustomAttribute<EnumMemberAttribute>(false)));

            var valid = withAttrs.Where(t => t.Attr != null);
            var dict = valid.ToDictionary(t => t.Attr.Value, t => (Enum)t.Info.GetValue(null));

            named = new ReadOnlyDictionary<string, Enum>(dict);

            NamedFlagsCache.TryAdd(type, named);
            return named;
        }
    }
}
