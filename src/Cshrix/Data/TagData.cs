// <copyright file="Struct1.cs">
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
    /// Contains data for a tag.
    /// </summary>
    public readonly struct TagData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagData" /> structure.
        /// </summary>
        /// <param name="order">
        /// A number in the range <c>[0,1]</c> describing a relative position of the room under the tag.
        /// </param>
        [JsonConstructor]
        public TagData(double order)
            : this() =>
            Order = order;

        /// <summary>
        /// Gets a number in the range <c>[0,1]</c> describing a relative position of the room under this tag.
        /// </summary>
        [JsonProperty("order")]
        public double Order { get; }
    }
}
