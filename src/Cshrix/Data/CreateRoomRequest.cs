// <copyright file="CreateRoomRequest.cs">
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

    using Events;
    using Events.Content;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public readonly struct CreateRoomRequest
    {
        public CreateRoomRequest(
            bool isDirect,
            RoomVisibility? visibility = null,
            [CanBeNull] string aliasLocalpart = null,
            [CanBeNull] string name = null,
            [CanBeNull] string topic = null,
            [CanBeNull] IReadOnlyCollection<Identifier> invites = null,
            [CanBeNull] IReadOnlyCollection<ThirdPartyRoomInvite> thirdPartyInvites = null,
            [CanBeNull] string version = null,
            [CanBeNull] CreationContent content = null,
            [CanBeNull] IReadOnlyCollection<StateEvent> initialState = null,
            RoomPreset? preset = null,
            [CanBeNull] PowerLevelsContent powerLevelsOverride = null)
        {
            IsDirect = isDirect;
            Visibility = visibility;
            AliasLocalpart = aliasLocalpart;
            Name = name;
            Topic = topic;
            Invites = invites;
            ThirdPartyInvites = thirdPartyInvites;
            Version = version;
            Content = content;
            InitialState = initialState;
            Preset = preset;
            PowerLevelsOverride = powerLevelsOverride;
        }

        [DefaultValue(RoomVisibility.Private)]
        [JsonProperty("visibility", NullValueHandling = NullValueHandling.Ignore)]
        public RoomVisibility? Visibility { get; }

        [JsonProperty(
            "room_alias_name",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string AliasLocalpart { get; }

        [JsonProperty(
            "name",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Name { get; }

        [JsonProperty(
            "topic",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Topic { get; }

        [JsonProperty(
            "invite",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<Identifier> Invites { get; }

        [JsonProperty(
            "invite_3pid",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<ThirdPartyRoomInvite> ThirdPartyInvites { get; }

        [JsonProperty(
            "room_version",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Version { get; }

        [JsonProperty(
            "creation_content",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public CreationContent Content { get; }

        [JsonProperty(
            "initial_state",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<StateEvent> InitialState { get; }

        [JsonProperty("preset", NullValueHandling = NullValueHandling.Ignore)]
        public RoomPreset? Preset { get; }

        [JsonProperty("is_direct")]
        public bool IsDirect { get; }

        [JsonProperty(
            "power_level_content_override",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public PowerLevelsContent PowerLevelsOverride { get; }
    }
}
