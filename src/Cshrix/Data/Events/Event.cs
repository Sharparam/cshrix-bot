// <copyright file="Event.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using System;

    using Content;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// Describes a basic event.
    /// </summary>
    [JsonConverter(typeof(EventConverter))]
    public sealed class Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Event" /> class.
        /// </summary>
        /// <param name="content">The content of the event.</param>
        /// <param name="id">The ID of the event.</param>
        /// <param name="type">The type of the event.</param>
        /// <param name="stateKey">The state key of the event, if any.</param>
        /// <param name="roomId">The ID of the room the event is associated with.</param>
        /// <param name="sender">The ID of the user who sent this event, if any.</param>
        /// <param name="sentAt">The date and time at which the event was sent.</param>
        /// <param name="previousContent">The previous content of the event.</param>
        /// <param name="redacts">The ID of another event to redact.</param>
        /// <param name="unsigned">Optional extra data about the event.</param>
        public Event(
            EventContent content,
            [CanBeNull] string id,
            string type,
            [CanBeNull] string stateKey,
            [CanBeNull] string roomId,
            [CanBeNull] UserId sender,
            DateTimeOffset? sentAt,
            [CanBeNull] EventContent previousContent,
            [CanBeNull] string redacts,
            UnsignedData? unsigned)
        {
            Content = content;
            Id = id;
            Type = type;
            StateKey = stateKey;
            RoomId = roomId;
            Sender = sender;
            SentAt = sentAt;
            PreviousContent = previousContent;
            Redacts = redacts;
            Unsigned = unsigned;
        }

        /// <summary>
        /// Gets the content of this event.
        /// </summary>
        [JsonProperty("content")]
        public EventContent Content { get; }

        /// <summary>
        /// Gets the ID of this event.
        /// </summary>
        [JsonProperty("event_id")]
        [CanBeNull]
        public string Id { get; }

        /// <summary>
        /// Gets the type of this event.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; }

        /// <summary>
        /// Gets the state key of this event, if any.
        /// </summary>
        [JsonProperty("state_key")]
        [CanBeNull]
        public string StateKey { get; }

        /// <summary>
        /// Gets the ID of the room this event is associated with.
        /// May be <c>null</c> if this event was retrieved from the sync API.
        /// </summary>
        [JsonProperty("room_id")]
        [CanBeNull]
        public string RoomId { get; internal set; }

        /// <summary>
        /// The ID of the user who sent this event, if any.
        /// </summary>
        [JsonProperty("sender")]
        [CanBeNull]
        public UserId Sender { get; }

        /// <summary>
        /// Gets the date and time at which this event was sent.
        /// </summary>
        [JsonProperty("origin_server_ts")]
        [JsonConverter(typeof(UnixMillisecondDateTimeConverter))]
        public DateTimeOffset? SentAt { get; }

        /// <summary>
        /// Gets the previous content of this event, if any.
        /// </summary>
        [JsonProperty("prev_content")]
        [CanBeNull]
        public EventContent PreviousContent { get; }

        /// <summary>
        /// Gets the ID of another event this event redacts, if any.
        /// </summary>
        [JsonProperty("redacts")]
        [CanBeNull]
        public string Redacts { get; }

        /// <summary>
        /// Gets optional extra information about the event.
        /// </summary>
        [JsonProperty("unsigned")]
        public UnsignedData? Unsigned { get; }

        /// <summary>
        /// Gets a value indicating whether this is a redaction event.
        /// </summary>
        [JsonIgnore]
        public bool IsRedaction => Redacts != null;

        /// <summary>
        /// Gets a value indicating whether this event is redacted.
        /// </summary>
        [JsonIgnore]
        public bool IsRedacted => Unsigned?.IsRedacted == true;
    }
}
