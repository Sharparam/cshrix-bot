// <copyright file="GroupId.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;

    using Newtonsoft.Json;

    using Serialization;

    /// <inheritdoc cref="Identifier" />
    /// <inheritdoc cref="IEquatable{GroupId}" />
    /// <summary>
    /// Represents the ID of a group on Matrix.
    /// </summary>
    /// <remarks>Group IDs are in the form of <c>+localpart:domain.tld</c>.</remarks>
    [JsonConverter(typeof(GroupIdConverter))]
    public sealed class GroupId : Identifier, IEquatable<GroupId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupId" /> class.
        /// </summary>
        /// <param name="localpart">The localpart of the group ID (between initial <c>+</c> and first <c>:</c>).</param>
        /// <param name="domain">The domain of the group ID.</param>
        public GroupId(string localpart, string domain)
            : this(localpart, new ServerName(domain))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupId" /> class.
        /// </summary>
        /// <param name="localpart">The localpart of the group ID (between initial <c>+</c> and first <c>:</c>).</param>
        /// <param name="domain">The domain of the group ID.</param>
        public GroupId(string localpart, ServerName domain)
            : base(IdentifierType.User, SigilMapping[IdentifierType.User], localpart, domain)
        {
        }

        /// <summary>
        /// Converts a string into a group ID.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns>The converted <see cref="GroupId" />.</returns>
        public static explicit operator GroupId(string str) => Parse(str);

        /// <summary>
        /// Parses an identifier string into a group ID.
        /// </summary>
        /// <param name="id">The identifier to parse.</param>
        /// <returns>An instance of <see cref="GroupId" />.</returns>
        public static GroupId Parse(string id) => Parse<GroupId>(id);

        /// <summary>
        /// Attempts to parse an identifier string into a group ID.
        /// </summary>
        /// <param name="id">The identifier to parse.</param>
        /// <param name="value">
        /// When this method returns, contains the parsed value;
        /// otherwise, the default value for <see cref="GroupId" />.
        /// </param>
        /// <returns><c>true</c> if the identifier was parsed successfully; otherwise, <c>false</c>.</returns>
        public static bool TryParse(string id, out GroupId value) => TryParse<GroupId>(id, out value);

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(GroupId other) => Equals((Identifier)other);
    }
}
