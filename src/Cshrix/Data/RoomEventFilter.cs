// <copyright file="RoomEventFilter.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Specifies a filter for room events in search APIs.
    /// </summary>
    public sealed class RoomEventFilter : EventFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomEventFilter" /> class.
        /// </summary>
        /// <param name="limit">Maximum number of events to return.</param>
        /// <param name="types">Event types to include, or <c>null</c> to include all.</param>
        /// <param name="notTypes">Event types to exclude.</param>
        /// <param name="senders">Senders to include, or <c>null</c> to include all.</param>
        /// <param name="notSenders">Senders to exclude.</param>
        /// <param name="rooms">Rooms to include, or <c>null</c> to include all.</param>
        /// <param name="notRooms">Rooms to exclude.</param>
        /// <param name="containsUrl">
        /// <c>true</c> to only include events with a URL in their content,
        /// <c>false</c> to only include events without a URL.
        /// </param>
        public RoomEventFilter(
            int? limit = null,
            [CanBeNull] IEnumerable<string> types = null,
            [CanBeNull] IEnumerable<string> notTypes = null,
            [CanBeNull] IEnumerable<UserId> senders = null,
            [CanBeNull] IEnumerable<UserId> notSenders = null,
            [CanBeNull] IEnumerable<string> rooms = null,
            [CanBeNull] IEnumerable<string> notRooms = null,
            bool containsUrl = false)
            : this(
                limit,
                types?.ToList().AsReadOnly(),
                notTypes?.ToList().AsReadOnly(),
                senders?.ToList().AsReadOnly(),
                notSenders?.ToList().AsReadOnly(),
                rooms?.ToList().AsReadOnly(),
                notRooms?.ToList().AsReadOnly(),
                containsUrl)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomEventFilter" /> class.
        /// </summary>
        /// <param name="limit">Maximum number of events to return.</param>
        /// <param name="types">Event types to include, or <c>null</c> to include all.</param>
        /// <param name="notTypes">Event types to exclude.</param>
        /// <param name="senders">Senders to include, or <c>null</c> to include all.</param>
        /// <param name="notSenders">Senders to exclude.</param>
        /// <param name="rooms">Rooms to include, or <c>null</c> to include all.</param>
        /// <param name="notRooms">Rooms to exclude.</param>
        /// <param name="containsUrl">
        /// <c>true</c> to only include events with a URL in their content,
        /// <c>false</c> to only include events without a URL.
        /// </param>
        [JsonConstructor]
        public RoomEventFilter(
            int? limit = null,
            [CanBeNull] IReadOnlyCollection<string> types = null,
            [CanBeNull] IReadOnlyCollection<string> notTypes = null,
            [CanBeNull] IReadOnlyCollection<UserId> senders = null,
            [CanBeNull] IReadOnlyCollection<UserId> notSenders = null,
            [CanBeNull] IReadOnlyCollection<string> rooms = null,
            [CanBeNull] IReadOnlyCollection<string> notRooms = null,
            bool containsUrl = false)
            : base(limit, types, notTypes, senders, notSenders)
        {
            Rooms = rooms;
            NotRooms = notRooms;
            ContainsUrl = containsUrl;
        }

        /// <summary>
        /// Gets a collection of room IDs, including only events from those rooms.
        /// </summary>
        /// <remarks>If this is <c>nulL</c>, all rooms are included.</remarks>
        [JsonProperty(
            "rooms",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<string> Rooms { get; }

        /// <summary>
        /// Gets a collection of room IDs, excluding any events sent in those rooms.
        /// </summary>
        /// <remarks>If this is <c>null</c>, no rooms are excluded.</remarks>
        [JsonProperty(
            "not_rooms",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<string> NotRooms { get; }

        /// <summary>
        /// Gets a value indicating whether only events that have a <c>url</c> key in their <c>content</c>
        /// should be returned.
        /// </summary>
        [DefaultValue(false)]
        [JsonProperty("contains_url")]
        public bool ContainsUrl { get; }
    }
}
