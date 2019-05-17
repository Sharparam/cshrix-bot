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

    using ThirdParty;

    /// <summary>
    /// Specifies the data used for creating a new room.
    /// </summary>
    public readonly struct CreateRoomRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRoomRequest" /> structure.
        /// </summary>
        /// <param name="visibility">The visibility of the room.</param>
        /// <param name="aliasLocalpart">The localpart of an alias to create.</param>
        /// <param name="version">The room version to set.</param>
        /// <param name="name">The name of the room.</param>
        /// <param name="topic">The topic of the room.</param>
        /// <param name="invites">A collection of user IDs to invite.</param>
        /// <param name="thirdPartyInvites">A collection of third party invites to send.</param>
        /// <param name="content">Additional content to add to the room creation event.</param>
        /// <param name="initialState">Initial state events to add to the room.</param>
        /// <param name="preset">A preset to set certain room properties automatically.</param>
        /// <param name="isDirect">Whether this is a direct messaging room.</param>
        /// <param name="powerLevelsOverride">Object specifying overrides for power levels in the room.</param>
        [JsonConstructor]
        public CreateRoomRequest(
            RoomVisibility? visibility = null,
            [CanBeNull] string aliasLocalpart = null,
            [CanBeNull] string version = null,
            [CanBeNull] string name = null,
            [CanBeNull] string topic = null,
            [CanBeNull] IReadOnlyCollection<UserId> invites = null,
            [CanBeNull] IReadOnlyCollection<ThirdPartyRoomInvite> thirdPartyInvites = null,
            [CanBeNull] CreationContent content = null,
            [CanBeNull] IReadOnlyCollection<StateEvent> initialState = null,
            RoomPreset? preset = null,
            bool isDirect = false,
            [CanBeNull] PowerLevelsContent powerLevelsOverride = null)
            : this()
        {
            Visibility = visibility;
            AliasLocalpart = aliasLocalpart;
            Version = version;
            Name = name;
            Topic = topic;
            Invites = invites;
            ThirdPartyInvites = thirdPartyInvites;
            Content = content;
            InitialState = initialState;
            Preset = preset;
            IsDirect = isDirect;
            PowerLevelsOverride = powerLevelsOverride;
        }

        /// <summary>
        /// Gets the visibility of the room.
        /// </summary>
        [DefaultValue(RoomVisibility.Private)]
        [JsonProperty("visibility", NullValueHandling = NullValueHandling.Ignore)]
        public RoomVisibility? Visibility { get; }

        /// <summary>
        /// Gets the localpart of an alias to create on the homeserver that is creating the room.
        /// </summary>
        [JsonProperty(
            "room_alias_name",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string AliasLocalpart { get; }

        /// <summary>
        /// Gets the room version to set on the new room.
        /// </summary>
        [JsonProperty(
            "room_version",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Version { get; }

        /// <summary>
        /// Gets a name to set on the room.
        /// </summary>
        [JsonProperty(
            "name",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Name { get; }

        /// <summary>
        /// Gets a topic to set on the room.
        /// </summary>
        [JsonProperty(
            "topic",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public string Topic { get; }

        /// <summary>
        /// Gets a collection of user IDs that should be invited to the room.
        /// </summary>
        [JsonProperty(
            "invite",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<UserId> Invites { get; }

        /// <summary>
        /// Gets a collection of third party invites that should be sent out for the room.
        /// </summary>
        [JsonProperty(
            "invite_3pid",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<ThirdPartyRoomInvite> ThirdPartyInvites { get; }

        /// <summary>
        /// Gets additional data to set in the content of the <c>m.room.create</c> event.
        /// </summary>
        [JsonProperty(
            "creation_content",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public CreationContent Content { get; }

        /// <summary>
        /// Gets a collection of state events that should be set in the new room.
        /// </summary>
        [JsonProperty(
            "initial_state",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public IReadOnlyCollection<StateEvent> InitialState { get; }

        /// <summary>
        /// Gets a preset to use for setting certain room parameters.
        /// </summary>
        [JsonProperty("preset", NullValueHandling = NullValueHandling.Ignore)]
        public RoomPreset? Preset { get; }

        /// <summary>
        /// Gets a value indicating whether this room should use direct messaging.
        /// </summary>
        [JsonProperty("is_direct")]
        public bool IsDirect { get; }

        /// <summary>
        /// Gets an object specifying power level overrides to apply.
        /// </summary>
        [JsonProperty(
            "power_level_content_override",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        [CanBeNull]
        public PowerLevelsContent PowerLevelsOverride { get; }
    }
}
