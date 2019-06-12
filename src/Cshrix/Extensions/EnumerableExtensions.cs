// <copyright file="EnumerableExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using Data.Events;

    /// <summary>
    /// Contains extensions for <see cref="IEnumerable{T}" />
    /// </summary>
    internal static class EnumerableExtensions
    {
        internal static IEnumerable<T> OfEventType<T>(this IEnumerable<T> events, string type) where T : Event =>
            events.Where(e => e.Type == type);

        internal static ILookup<string, T> ToEventTypeLookup<T>(this IEnumerable<T> events) where T : Event =>
            events.ToLookup(e => e.Type);
    }
}
