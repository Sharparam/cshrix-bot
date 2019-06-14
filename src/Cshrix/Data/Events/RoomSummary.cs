// <copyright file="RoomSummary.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events
{
    using System.Collections.Generic;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains information about a room which clients may need to correctly render it to users.
    /// </summary>
    public readonly struct RoomSummary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomSummary" /> structure.
        /// </summary>
        /// <param name="heroes">
        /// The users which can be used to generate a room name if the room does not have one.
        /// </param>
        /// <param name="joinedCount">The number of users joined to the room.</param>
        /// <param name="invitedCount">The number of users invited to the room.</param>
        [JsonConstructor]
        public RoomSummary([CanBeNull] IReadOnlyCollection<UserId> heroes, int? joinedCount, int? invitedCount)
            : this()
        {
            Heroes = heroes;
            JoinedCount = joinedCount;
            InvitedCount = invitedCount;
        }

        /// <summary>
        /// Gets a collection of users which can be used to generate a room name if the room does not have one.
        /// </summary>
        /// <remarks>
        /// Required if the room's <c>m.room.name</c> or <c>m.room.canonical_alias</c> state events are unset or empty.
        /// This should be the first 5 members of the room, ordered by stream ordering, which are joined or invited.
        /// The list must never include the client's own user ID. When no joined or invited members are available,
        /// this should consist of the banned and left users. More than 5 members may be provided,
        /// however less than 5 should only be provided when there are less than 5 members to represent.
        /// When lazy-loading room members is enabled, the membership events for the heroes MUST be included
        /// in the state, unless they are redundant. When the list of users changes, the server notifies the client
        /// by sending a fresh list of heroes. If there are no changes since the last sync,
        /// this field may be <c>null</c>.
        /// </remarks>
        [JsonProperty("m.heroes")]
        [CanBeNull]
        public IReadOnlyCollection<UserId> Heroes { get; }

        /// <summary>
        /// Gets the number of users with <c>membership</c> of <c>join</c>, including the client's own user.
        /// </summary>
        /// <remarks>
        /// If this field has not changed since the last sync, it may be <c>null</c>. Required otherwise.
        /// </remarks>
        [JsonProperty("m.joined_member_count")]
        public int? JoinedCount { get; }

        /// <summary>
        /// Gets the number of users with <c>membership</c> of <c>invite</c>.
        /// </summary>
        /// <remarks>
        /// If this field has not changed since the last sync, it may be <c>null</c>. Required otherwise.
        /// </remarks>
        [JsonProperty("m.invited_member_count")]
        public int? InvitedCount { get; }
    }
}
