// <copyright file="ReflectionHelper.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Utilities
{
    using System;

    /// <summary>
    /// Contains helper methods for performing reflection tasks.
    /// </summary>
    internal static class ReflectionUtils
    {
        /// <summary>
        /// Checks if a type is not a value type and nullable.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns><c>true</c> if the type is nullable, otherwise <c>false</c>.</returns>
        internal static bool IsNullable(Type type) => !type.IsValueType || IsNullableType(type);

        /// <summary>
        /// Checks if a type (including value types) is nullable.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns><c>true</c> if the type is nullable, otherwise <c>false</c>.</returns>
        internal static bool IsNullableType(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
}
