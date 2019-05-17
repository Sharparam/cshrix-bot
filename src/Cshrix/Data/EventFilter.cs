// <copyright file="EventFilter.cs">
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
    public class EventFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventFilter" /> class.
        /// </summary>
        /// <param name="limit">Maximum number of events to return.</param>
        /// <param name="types">Event types to include, or <c>null</c> to include all.</param>
        /// <param name="notTypes">Event types to exclude.</param>
        /// <param name="senders">Senders to include, or <c>null</c> to include all.</param>
        /// <param name="notSenders">Senders to exclude.</param>
        public EventFilter(
            int? limit = null,
            [CanBeNull] IEnumerable<string> types = null,
            [CanBeNull] IEnumerable<string> notTypes = null,
            [CanBeNull] IEnumerable<UserId> senders = null,
            [CanBeNull] IEnumerable<UserId> notSenders = null)
            : this(
                limit,
                types?.ToList().AsReadOnly(),
                notTypes?.ToList().AsReadOnly(),
                senders?.ToList().AsReadOnly(),
                notSenders?.ToList().AsReadOnly())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFilter" /> class.
        /// </summary>
        /// <param name="limit">Maximum number of events to return.</param>
        /// <param name="types">Event types to include, or <c>null</c> to include all.</param>
        /// <param name="notTypes">Event types to exclude.</param>
        /// <param name="senders">Senders to include, or <c>null</c> to include all.</param>
        /// <param name="notSenders">Senders to exclude.</param>
        [JsonConstructor]
        public EventFilter(
            int? limit = null,
            [CanBeNull] IReadOnlyCollection<string> types = null,
            [CanBeNull] IReadOnlyCollection<string> notTypes = null,
            [CanBeNull] IReadOnlyCollection<UserId> senders = null,
            [CanBeNull] IReadOnlyCollection<UserId> notSenders = null)
        {
            Limit = limit;
            Types = types;
            NotTypes = notTypes;
            Senders = senders;
            NotSenders = notSenders;
        }

        /// <summary>
        /// Gets the maximum number of events to return.
        /// </summary>
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        [CanBeNull]
        public int? Limit { get; }

        /// <summary>
        /// Gets a collection of types, including only events whose type is present in this collection.
        /// </summary>
        /// <remarks>
        /// If this is <c>null</c>, all types are included.
        /// An asterisk (<c>*</c>) can be used as a wildcard to match any sequence of characters.
        /// </remarks>
        [JsonProperty(
            "types",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<string> Types { get; }

        /// <summary>
        /// Gets a collection of event types, excluding any events with those types.
        /// </summary>
        /// <remarks>
        /// If this is <c>null</c>, no types are excluded.
        /// An asterisk (<c>*</c>) can be used as a wildcard to match any sequence of characters.
        /// </remarks>
        [JsonProperty(
            "not_types",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<string> NotTypes { get; }

        /// <summary>
        /// Gets a collection of user IDs, including only events sent by those user IDs.
        /// </summary>
        /// <remarks>If this is <c>null</c>, all senders are included.</remarks>
        [JsonProperty(
            "senders",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<UserId> Senders { get; }

        /// <summary>
        /// Gets a collection of user IDs, excluding any events sent by those user IDs.
        /// </summary>
        /// <remarks>If this is <c>null</c>, no senders are excluded.</remarks>
        [JsonProperty(
            "not_senders",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<UserId> NotSenders { get; }
    }
}
