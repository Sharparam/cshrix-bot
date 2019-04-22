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

    public static class EnumHelpers
    {
        private static readonly ConcurrentDictionary<Type, IReadOnlyDictionary<string, Enum>> NamedFlagsCache =
            new ConcurrentDictionary<Type, IReadOnlyDictionary<string, Enum>>();

        public static IReadOnlyDictionary<string, Enum> GetNamedFlags<TEnum>() where TEnum : Enum =>
            GetNamedFlags(typeof(TEnum));

        public static IReadOnlyDictionary<string, Enum> GetNamedFlags(Type type)
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
