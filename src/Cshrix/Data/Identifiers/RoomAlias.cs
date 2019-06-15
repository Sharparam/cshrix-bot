// <copyright file="RoomAlias.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    using Serialization;

    /// <inheritdoc cref="Identifier" />
    /// <inheritdoc cref="IEquatable{RoomAlias}" />
    /// <summary>
    /// Represents an alias for a room.
    /// </summary>
    /// <remarks>
    /// Aliases are in the form of <c>#localpart:domain.tld</c>.
    /// </remarks>
    [JsonConverter(typeof(RoomAliasConverter))]
    public sealed class RoomAlias : Identifier, IEquatable<RoomAlias>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomAlias" /> class.
        /// </summary>
        /// <param name="localpart">The localpart of the alias.</param>
        /// <param name="domain">The domain on which the alias lives.</param>
        [PublicAPI]
        public RoomAlias(string localpart, string domain)
            : this(localpart, new ServerName(domain))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomAlias" /> class.
        /// </summary>
        /// <param name="localpart">The localpart of the alias.</param>
        /// <param name="domain">The domain on which the alias lives.</param>
        public RoomAlias(string localpart, ServerName domain)
            : base(IdentifierType.RoomAlias, localpart, domain)
        {
        }

        /// <summary>
        /// Converts a string into a room alias.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns>The converted <see cref="RoomAlias" />.</returns>
        public static explicit operator RoomAlias(string str) => Parse(str);

        /// <summary>
        /// Parses an identifier string into a room alias.
        /// </summary>
        /// <param name="id">The identifier to parse.</param>
        /// <returns>An instance of <see cref="RoomAlias" />.</returns>
        [PublicAPI]
        public static RoomAlias Parse(string id) => Parse<RoomAlias>(id);

        /// <summary>
        /// Attempts to parse an identifier string into a room alias.
        /// </summary>
        /// <param name="id">The identifier to parse.</param>
        /// <param name="value">
        /// When this method returns, contains the parsed value;
        /// otherwise, the default value for <see cref="RoomAlias" />.
        /// </param>
        /// <returns><c>true</c> if the identifier was parsed successfully; otherwise, <c>false</c>.</returns>
        public static bool TryParse(string id, out RoomAlias value) => Identifier.TryParse(id, out value);

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(RoomAlias other) => Equals((Identifier)other);
    }
}
