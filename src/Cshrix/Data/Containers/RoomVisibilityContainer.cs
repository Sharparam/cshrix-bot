// <copyright file="RoomVisibilityContainer.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using Newtonsoft.Json;

    /// <summary>
    /// An object containing a room visibility value.
    /// </summary>
    public readonly struct RoomVisibilityContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomVisibilityContainer" /> structure.
        /// </summary>
        /// <param name="visibility">The room visibility.</param>
        [JsonConstructor]
        public RoomVisibilityContainer(RoomVisibility visibility)
            : this() =>
            Visibility = visibility;

        /// <summary>
        /// Gets the room visibility.
        /// </summary>
        [JsonProperty("visibility")]
        public RoomVisibility Visibility { get; }
    }
}
