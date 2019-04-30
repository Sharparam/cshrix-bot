// <copyright file="RoomAliasConverter.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Serialization
{
    using Data;

    using Newtonsoft.Json;

    /// <inheritdoc />
    /// <summary>
    /// Converts <see cref="RoomAlias" /> to/from its JSON representation.
    /// </summary>
    public class RoomAliasConverter : IdentifierConverter<RoomAlias>
    {
        /// <inheritdoc />
        /// <summary>
        /// Parses a string ID into a <see cref="RoomAlias"/>.
        /// </summary>
        /// <param name="id">The ID to parse.</param>
        /// <returns>An instance of <see cref="RoomAlias" />.</returns>
        /// <exception cref="JsonSerializationException">
        /// Thrown if <paramref name="id" /> is not a valid room alias.
        /// </exception>
        protected override RoomAlias Parse(string id)
        {
            var successful = RoomAlias.TryParse(id, out var userId);

            if (successful)
            {
                return userId;
            }

            throw new JsonSerializationException("String was not a valid RoomAlias");
        }
    }
}
