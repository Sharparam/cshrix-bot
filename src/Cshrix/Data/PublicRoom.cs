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

    public readonly struct PublicRoom
    {
        [JsonConstructor]
        public PublicRoom(
            [CanBeNull] IReadOnlyCollection<RoomAlias> aliases,
            [CanBeNull] Uri avatarUri,
            [CanBeNull] RoomAlias canonicalAlias,
            bool isGuestAccessEnabled,
            [CanBeNull] string name,
            int memberCount,
            string roomId,
            [CanBeNull] string topic,
            bool isWorldReadable)
            : this()
        {
            Aliases = aliases;
            AvatarUri = avatarUri;
            CanonicalAlias = canonicalAlias;
            IsGuestAccessEnabled = isGuestAccessEnabled;
            Name = name;
            MemberCount = memberCount;
            RoomId = roomId;
            Topic = topic;
            IsWorldReadable = isWorldReadable;
        }

        [JsonProperty("aliases")]
        [CanBeNull]
        public IReadOnlyCollection<RoomAlias> Aliases { get; }

        [JsonProperty("avatar_url")]
        [CanBeNull]
        public Uri AvatarUri { get; }

        [JsonProperty("canonical_alias")]
        [CanBeNull]
        public RoomAlias CanonicalAlias { get; }

        [JsonProperty("guests_can_join")]
        public bool IsGuestAccessEnabled { get; }

        [JsonProperty("name")]
        [CanBeNull]
        public string Name { get; }

        [JsonProperty("num_joined_members")]
        public int MemberCount { get; }

        [JsonProperty("room_id")]
        public string RoomId { get; }

        [JsonProperty("topic")]
        [CanBeNull]
        public string Topic { get; }

        [JsonProperty("world_readable")]
        public bool IsWorldReadable { get; }
    }
}
