// <copyright file="SetExtensions.cs">
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
    /// Contains extensions for the <see cref="ISet{T}" /> interface.
    /// </summary>
    internal static class SetExtensions
    {
        /// <summary>
        /// Adds a range of values to a set.
        /// </summary>
        /// <param name="set">The set to add values to.</param>
        /// <param name="values">The values to add.</param>
        /// <typeparam name="T">The type of the values.</typeparam>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="set" /> is <c>null</c>.</exception>
        internal static void AddRange<T>([NotNull] this ISet<T> set, IEnumerable<T> values)
        {
            if (set == null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            foreach (var value in values)
            {
                set.Add(value);
            }
        }
    }
}
