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
    using Data.Events.Content;

    /// <summary>
    /// Contains extensions for <see cref="IEnumerable{T}" />
    /// </summary>
    internal static class EnumerableExtensions
    {
        internal static IEnumerable<Event> OfEventType(this IEnumerable<Event> events, string type) =>
            events.Where(e => e.Type == type);

        internal static IEnumerable<Event> OfEventType(this EventsContainer events, string type) =>
            events.Events.OfEventType(type);

        internal static EventContent GetStateEventContentOrDefault(this IEnumerable<Event> events, string type) =>
            events.OfEventType(type).FirstOrDefault(e => e.StateKey == string.Empty)?.Content;

        internal static TContent GetStateEventContentOrDefault<TContent>(this IEnumerable<Event> events, string type)
            where TContent : EventContent =>
            events.GetStateEventContentOrDefault(type) as TContent;

        internal static EventContent GetStateEventContentOrDefault(this EventsContainer events, string type) =>
            events.Events.GetStateEventContentOrDefault(type);

        internal static TContent GetStateEventContentOrDefault<TContent>(this EventsContainer events, string type)
            where TContent : EventContent =>
            events.Events.GetStateEventContentOrDefault<TContent>(type);

        internal static ILookup<string, Event> ToEventTypeLookup(this IEnumerable<Event> events) =>
            events.ToLookup(e => e.Type);
    }
}
