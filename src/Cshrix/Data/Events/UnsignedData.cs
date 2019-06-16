// <copyright file="UnsignedData.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using System;
    using System.Collections.Generic;

    using Content;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Serialization;

    /// <summary>
    /// Contains information about an event which was not sent by the originating homeserver.
    /// </summary>
    public readonly struct UnsignedData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsignedData" /> structure.
        /// </summary>
        /// <param name="replacesStateEventId">The ID of the event being replaced.</param>
        /// <param name="previousContent">The previous contents of the event.</param>
        /// <param name="previousSender">The ID of the user who sent the previous event.</param>
        /// <param name="age">The time since the event was sent.</param>
        /// <param name="redactionEvent">An event that redacted this event, if any.</param>
        /// <param name="transactionId">The transaction ID of the event, if applicable.</param>
        /// <param name="inviteRoomState">A subset of state events of the room at the time of an invite.</param>
        [JsonConstructor]
        public UnsignedData(
            [CanBeNull] string replacesStateEventId,
            [CanBeNull] EventContent previousContent,
            [CanBeNull] UserId previousSender,
            TimeSpan? age,
            [CanBeNull] Event redactionEvent,
            [CanBeNull] string transactionId,
            [CanBeNull] IReadOnlyCollection<Event> inviteRoomState)
            : this()
        {
            Age = age;
            RedactionEvent = redactionEvent;
            TransactionId = transactionId;
            InviteRoomState = inviteRoomState;
        }

        /// <summary>
        /// Gets the ID of the state event replaced by the current event.
        /// </summary>
        [JsonProperty("replaces_state")]
        [CanBeNull]
        public string ReplacesStateEventId { get; }

        /// <summary>
        /// Gets the previous contents of the event.
        /// </summary>
        [JsonProperty("prev_content")]
        [CanBeNull]
        public EventContent PreviousContent { get; }

        /// <summary>
        /// Gets the ID of the user who sent the previous event.
        /// </summary>
        [JsonProperty("prev_sender")]
        [CanBeNull]
        public UserId PreviousSender { get; }

        /// <summary>
        /// Gets the time since the event was sent.
        /// </summary>
        [JsonProperty("age")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan? Age { get; }

        /// <summary>
        /// Gets the event that redacted this event, if any.
        /// </summary>
        [JsonProperty("redacted_because")]
        [CanBeNull]
        public Event RedactionEvent { get; }

        /// <summary>
        /// Gets the transaction ID set when this message was sent.
        /// </summary>
        /// <remarks>
        /// This key will only be non-<c>null</c> for message events sent by the device calling the API.
        /// </remarks>
        [JsonProperty("transaction_id")]
        [CanBeNull]
        public string TransactionId { get; }

        /// <summary>
        /// Gets a subset of state events of the relevant room at the time of an invite, if
        /// <c>membership</c> is <c>invite</c>.
        /// </summary>
        /// <remarks>
        /// Note that this state is informational, and <em>should not</em> be trusted; once the client has joined the
        /// room, it <em>should</em> fetch the live state from the server and discard the
        /// <see cref="InviteRoomState" />. Also, clients must not rely on any particular state being present here;
        /// they <em>should</em> behave properly (with possibly a degraded but not a broken experience) in the
        /// absence of any particular events here. If they are set on the room, at least the state for
        /// <c>m.room.avatar</c>, <c>m.room.canonical_alias</c>, <c>m.room.join_rules</c>, and <c>m.room.name</c>
        /// <em>should</em> be included.
        /// </remarks>
        [JsonProperty("invite_room_state")]
        [CanBeNull]
        public IReadOnlyCollection<Event> InviteRoomState { get; }

        /// <summary>
        /// Gets a value indicating whether the event has been redacted.
        /// </summary>
        [JsonIgnore]
        public bool IsRedacted => RedactionEvent != null;
    }
}
