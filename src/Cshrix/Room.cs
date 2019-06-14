// <copyright file="Room.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using Data.Events;
    using Data.Events.Content;

    using Extensions;

    /// <summary>
    /// Describes a room joined by the client, and defines possible actions.
    /// </summary>
    public sealed class Room : IRoom
    {
        /// <summary>
        /// A set containing all known aliases for the room.
        /// </summary>
        private readonly HashSet<RoomAlias> _aliases;

        /// <summary>
        /// Initializes a new instance of the <see cref="Room" /> class.
        /// </summary>
        /// <param name="id">The ID of the room.</param>
        /// <param name="membership">The current membership of the user in the room.</param>
        public Room(string id, Membership membership)
        {
            Id = id;
            Membership = membership;
            _aliases = new HashSet<RoomAlias>();
        }

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
        public Membership Membership { get; private set; }

        /// <inheritdoc />
        public RoomAlias CanonicalAlias { get; private set; }

        /// <inheritdoc />
        public IReadOnlyCollection<RoomAlias> Aliases => _aliases;

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <inheritdoc />
        public string Topic { get; private set; }

        /// <summary>
        /// Creates a <see cref="Room" /> from the contents of an <see cref="InvitedRoom" />.
        /// </summary>
        /// <param name="id">The ID of the room the user was invited to.</param>
        /// <param name="invitedRoom">The data for the invited room.</param>
        /// <returns>An instance of <see cref="Room" />.</returns>
        internal static Room FromInvitedRoom(string id, InvitedRoom invitedRoom)
        {
            var room = new Room(id, Membership.Invited);

            var state = invitedRoom.InviteState;
            room.UpdateFromState(state);

            return room;
        }

        /// <summary>
        /// Updates room details from a collection of state events.
        /// </summary>
        /// <param name="state">The state events to update from.</param>
        private void UpdateFromState(EventsContainer state)
        {
            var aliasState = state.Events.OfEventType("m.room.canonical_alias")
                .FirstOrDefault(e => e.StateKey == string.Empty);

            var aliasContent = aliasState?.Content as CanonicalAliasContent;

            if (aliasContent?.Alias != null)
            {
                CanonicalAlias = aliasContent.Alias;
                _aliases.Add(aliasContent.Alias);
            }

            var aliasesStates = state.Events.OfEventType("m.room.aliases");
            var aliases = aliasesStates.SelectMany(e => (e.Content as AliasesContent)?.Aliases).Where(a => a != null);
            _aliases.UnionWith(aliases);

            if (state.Events.OfEventType("m.room.name").FirstOrDefault(e => e.StateKey == string.Empty)?.Content is
                RoomNameContent nameContent)
            {
                Name = nameContent.Name;
            }

            if (state.Events.OfEventType("m.room.topic").FirstOrDefault(e => e.StateKey == string.Empty)?.Content is
                RoomTopicContent topicContent)
            {
                Topic = topicContent.Topic;
            }
        }
    }
}
