// <copyright file="Filter.cs">
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
    /// Describes a filter for events.
    /// </summary>
    public readonly struct Filter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Filter" /> structure.
        /// </summary>
        /// <param name="eventFields">Event fields to include in the result.</param>
        /// <param name="eventFormat">How to format the results.</param>
        /// <param name="accountData">A filter for account data.</param>
        /// <param name="presence">A filter for presence events.</param>
        /// <param name="room">A filter for room data.</param>
        public Filter(
            [CanBeNull] IEnumerable<string> eventFields = null,
            EventFormat? eventFormat = null,
            [CanBeNull] EventFilter accountData = null,
            [CanBeNull] EventFilter presence = null,
            RoomFilter? room = null)
            : this(eventFields?.ToList().AsReadOnly(), eventFormat, accountData, presence, room)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Filter" /> structure.
        /// </summary>
        /// <param name="eventFields">Event fields to include in the result.</param>
        /// <param name="eventFormat">How to format the results.</param>
        /// <param name="accountData">A filter for account data.</param>
        /// <param name="presence">A filter for presence events.</param>
        /// <param name="room">A filter for room data.</param>
        [JsonConstructor]
        public Filter(
            [CanBeNull] IReadOnlyCollection<string> eventFields = null,
            EventFormat? eventFormat = null,
            [CanBeNull] EventFilter accountData = null,
            [CanBeNull] EventFilter presence = null,
            RoomFilter? room = null)
            : this()
        {
            EventFields = eventFields;
            EventFormat = eventFormat;
            AccountData = accountData;
            Presence = presence;
            Room = room;
        }

        /// <summary>
        /// Gets a collection of event fields to include.
        /// </summary>
        /// <remarks>
        /// If this collection is <c>null</c> then all fields are included. The entries may include <c>.</c>
        /// characters to indicate sub-fields. So <c>['content.body']</c> will include the <c>body</c> field of the
        /// <c>content</c> object. A literal <c>.</c> character in a field name may be escaped by using a <c>\.</c>.
        /// A server may include more fields than were requested.
        /// </remarks>
        [JsonProperty(
            "event_fields",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<string> EventFields { get; }

        /// <summary>
        /// Gets a format to use for events.
        /// </summary>
        /// <remarks>
        /// <see cref="Cshrix.Data.EventFormat.Client" /> will return the events in a format suitable for clients.
        /// <see cref="Cshrix.Data.EventFormat.Federation" /> will return the raw event as received over federation.
        /// The default is <see cref="Cshrix.Data.EventFormat.Client" />.
        /// </remarks>
        [JsonProperty("event_format", NullValueHandling = NullValueHandling.Ignore)]
        public EventFormat? EventFormat { get; }

        /// <summary>
        /// Gets an event filter for account data.
        /// </summary>
        [JsonProperty(
            "account_data",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public EventFilter AccountData { get; }

        /// <summary>
        /// Gets an event filter for presence events.
        /// </summary>
        [JsonProperty(
            "presence",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public EventFilter Presence { get; }

        /// <summary>
        /// Gets a filter to be applied to room data.
        /// </summary>
        [JsonProperty(
            "room",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        public RoomFilter? Room { get; }
    }
}
