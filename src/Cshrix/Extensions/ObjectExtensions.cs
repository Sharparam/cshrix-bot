// <copyright file="ObjectExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Extensions
{
    /// <summary>
    /// Contains extensions to <see cref="object" />.
    /// </summary>
    internal static class ObjectExtensions
    {
        /// <summary>
        /// Performs a static (compile-time checked) cast.
        /// </summary>
        /// <param name="o">The object to cast.</param>
        /// <typeparam name="T">The type to cast to.</typeparam>
        /// <returns>The cast object.</returns>
        internal static T StaticCast<T>(this T o) => o;
    }
}
