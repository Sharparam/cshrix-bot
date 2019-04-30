// <copyright file="Profile.cs">
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
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Contains profile information for a user.
    /// </summary>
    [PublicAPI]
    public readonly struct Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Profile" /> structure.
        /// </summary>
        /// <param name="displayName">Display name of the user.</param>
        /// <param name="avatarUri">Avatar URI of the user.</param>
        /// <param name="additionalData">Additional data provided from the API.</param>
        [JsonConstructor]
        public Profile(string displayName, Uri avatarUri, IReadOnlyDictionary<string, JToken> additionalData)
            : this()
        {
            DisplayName = displayName;
            AvatarUri = avatarUri;
            AdditionalData = additionalData;
        }

        /// <summary>
        /// Gets the display name of the user.
        /// </summary>
        [JsonProperty("displayname")]
        public string DisplayName { get; }

        /// <summary>
        /// Gets the avatar URI of the user.
        /// </summary>
        [JsonProperty("avatar_url")]
        public Uri AvatarUri { get; }

        /// <summary>
        /// Gets a dictionary containing any additional data that was provided from the API.
        /// </summary>
        [JsonExtensionData]
        public IReadOnlyDictionary<string, JToken> AdditionalData { get; }
    }
}
