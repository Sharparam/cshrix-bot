// <copyright file="UserIdConverter.cs">
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
    /// A converter to convert user IDs to/from their JSON representation.
    /// </summary>
    public sealed class UserIdConverter : IdentifierConverter<UserId>
    {
        /// <inheritdoc />
        /// <summary>
        /// Parses a string ID into a <see cref="UserId"/>.
        /// </summary>
        /// <param name="id">The ID to parse.</param>
        /// <returns>An instance of <see cref="UserId"/>.</returns>
        protected override UserId Parse(string id)
        {
            var successful = UserId.TryParse(id, out var userId);

            if (successful)
            {
                return userId;
            }

            throw new JsonSerializationException("String was not a valid UserId");
        }
    }
}
