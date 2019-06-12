// <copyright file="PublicRoom.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;
    using System.Collections.Generic;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains data on a public room.
    /// </summary>
    public readonly struct PublicRoom
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicRoom" /> structure.
        /// </summary>
        /// <param name="roomId">The ID of the room.</param>
        /// <param name="memberCount">The number of members joined to the room.</param>
        /// <param name="canonicalAlias">The canonical alias for the room.</param>
        /// <param name="aliases">Aliases for the room.</param>
        /// <param name="avatarUri">Avatar URI for the room.</param>
        /// <param name="name">The name of the room.</param>
        /// <param name="topic">The current topic of the room.</param>
        /// <param name="isGuestAccessEnabled">Whether guest access is enabled.</param>
        /// <param name="isWorldReadable">Whether the room is readable by guests without joining.</param>
        [JsonConstructor]
        public PublicRoom(
            string roomId,
            int memberCount,
            [CanBeNull] RoomAlias canonicalAlias,
            [CanBeNull] IReadOnlyCollection<RoomAlias> aliases,
            [CanBeNull] Uri avatarUri,
            [CanBeNull] string name,
            [CanBeNull] string topic,
            bool isGuestAccessEnabled,
            bool isWorldReadable)
            : this()
        {
            RoomId = roomId;
            MemberCount = memberCount;
            CanonicalAlias = canonicalAlias;
            Aliases = aliases;
            AvatarUri = avatarUri;
            Name = name;
            Topic = topic;
            IsGuestAccessEnabled = isGuestAccessEnabled;
            IsWorldReadable = isWorldReadable;
        }

        /// <summary>
        /// Gets the ID of this room.
        /// </summary>
        [JsonProperty("room_id")]
        public string RoomId { get; }

        /// <summary>
        /// Gets the number of members joined to this room.
        /// </summary>
        [JsonProperty("num_joined_members")]
        public int MemberCount { get; }

        /// <summary>
        /// Gets the canonical alias for the room, if one is set.
        /// </summary>
        [JsonProperty("canonical_alias")]
        [CanBeNull]
        public RoomAlias CanonicalAlias { get; }

        /// <summary>
        /// Gets a collection of aliases for this room, if it has any.
        /// </summary>
        [JsonProperty("aliases")]
        [CanBeNull]
        public IReadOnlyCollection<RoomAlias> Aliases { get; }

        /// <summary>
        /// Gets the URI for the room's avatar, if one is set.
        /// </summary>
        [JsonProperty("avatar_url")]
        [CanBeNull]
        public Uri AvatarUri { get; }

        /// <summary>
        /// Gets the name of this room, if one is set.
        /// </summary>
        [JsonProperty("name")]
        [CanBeNull]
        public string Name { get; }

        /// <summary>
        /// Gets the topic of this room, if one is set.
        /// </summary>
        [JsonProperty("topic")]
        [CanBeNull]
        public string Topic { get; }

        /// <summary>
        /// Gets a value indicating whether guests can join this room.
        /// </summary>
        [JsonProperty("guests_can_join")]
        public bool IsGuestAccessEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this room may be viewed by guests without joining.
        /// </summary>
        [JsonProperty("world_readable")]
        public bool IsWorldReadable { get; }
    }
}
