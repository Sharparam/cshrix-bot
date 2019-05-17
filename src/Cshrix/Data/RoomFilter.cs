// <copyright file="RoomFilter.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains a filter to be applied to room data.
    /// </summary>
    public readonly struct RoomFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomFilter" /> structure.
        /// </summary>
        /// <param name="includeLeave">A value indicating whether left rooms should be included.</param>
        /// <param name="rooms">A collection of room IDs to include.</param>
        /// <param name="notRooms">A collection of room IDs to exclude.</param>
        /// <param name="accountData">An event filter to apply to account data.</param>
        /// <param name="ephemeral">An event filter to apply to ephemeral events.</param>
        /// <param name="state">An event filter to apply to state events.</param>
        /// <param name="timeline">An event filter to apply to timeline events.</param>
        public RoomFilter(
            bool includeLeave = false,
            [CanBeNull] IEnumerable<string> rooms = null,
            [CanBeNull] IEnumerable<string> notRooms = null,
            [CanBeNull] RoomEventFilter accountData = null,
            [CanBeNull] RoomEventFilter ephemeral = null,
            [CanBeNull] RoomEventFilter state = null,
            [CanBeNull] RoomEventFilter timeline = null)
            : this(
                includeLeave,
                rooms?.ToList().AsReadOnly(),
                notRooms?.ToList().AsReadOnly(),
                accountData,
                ephemeral,
                state,
                timeline)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomFilter" /> structure.
        /// </summary>
        /// <param name="includeLeave">A value indicating whether left rooms should be included.</param>
        /// <param name="rooms">A collection of room IDs to include.</param>
        /// <param name="notRooms">A collection of room IDs to exclude.</param>
        /// <param name="accountData">An event filter to apply to account data.</param>
        /// <param name="ephemeral">An event filter to apply to ephemeral events.</param>
        /// <param name="state">An event filter to apply to state events.</param>
        /// <param name="timeline">An event filter to apply to timeline events.</param>
        [JsonConstructor]
        public RoomFilter(
            bool includeLeave = false,
            [CanBeNull] IReadOnlyCollection<string> rooms = null,
            [CanBeNull] IReadOnlyCollection<string> notRooms = null,
            [CanBeNull] RoomEventFilter accountData = null,
            [CanBeNull] RoomEventFilter ephemeral = null,
            [CanBeNull] RoomEventFilter state = null,
            [CanBeNull] RoomEventFilter timeline = null)
            : this()
        {
            IncludeLeave = includeLeave;
            Rooms = rooms;
            NotRooms = notRooms;
            AccountData = accountData;
            Ephemeral = ephemeral;
            State = state;
            Timeline = timeline;
        }

        /// <summary>
        /// Gets a value indicating whether rooms that the user has left should be included.
        /// </summary>
        [JsonProperty("include_leave")]
        public bool IncludeLeave { get; }

        /// <summary>
        /// Gets a collection of room IDs to include.
        /// </summary>
        /// <remarks>
        /// If this collection is <c>null</c> then all rooms are included. This filter is applied before the filters
        /// in <see cref="Ephemeral" />, <see cref="State" />, <see cref="Timeline" />, and <see cref="AccountData" />.
        /// </remarks>
        [JsonProperty(
            "rooms",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<string> Rooms { get; }

        /// <summary>
        /// Gets a collection of room IDs to exclude.
        /// </summary>
        /// <remarks>
        /// If this collection is <c>null</c> then no rooms are excluded. A matching room will be excluded even if
        /// it is listed in the <see cref="Rooms" /> filter. This filter is applied before the filters in
        /// <see cref="Ephemeral" />, <see cref="State" />, <see cref="Timeline" />, and <see cref="AccountData" />.
        /// </remarks>
        [JsonProperty(
            "not_rooms",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<string> NotRooms { get; }

        /// <summary>
        /// Gets an event filter for the per-user account data to include.
        /// </summary>
        [JsonProperty(
            "account_data",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public RoomEventFilter AccountData { get; }

        /// <summary>
        /// Gets an event filter for which events to include that are not recorded in the room history, e.g. typing and
        /// receipt events.
        /// </summary>
        [JsonProperty(
            "ephemeral",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public RoomEventFilter Ephemeral { get; }

        /// <summary>
        /// Gets an event filter for state events.
        /// </summary>
        [JsonProperty(
            "state",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public RoomEventFilter State { get; }

        /// <summary>
        /// Gets an event filter for timeline events.
        /// </summary>
        [JsonProperty(
            "timeline",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public RoomEventFilter Timeline { get; }
    }
}
