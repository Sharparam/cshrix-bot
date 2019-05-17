// <copyright file="UserId.cs">
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
    /// <inheritdoc cref="IEquatable{UserId}"/>
    /// <summary>
    /// The MXID of a user on Matrix.
    /// </summary>
    /// <remarks>User IDs are in the form of <c>@localpart:domain.tld</c>.</remarks>
    [JsonConverter(typeof(UserIdConverter))]
    public sealed class UserId : Identifier, IEquatable<UserId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserId" /> class.
        /// </summary>
        /// <param name="localpart">The localpart of the user ID (between initial <c>@</c> and first <c>:</c>).</param>
        /// <param name="domain">The domain of the user ID.</param>
        public UserId(string localpart, string domain)
            : this(localpart, new ServerName(domain))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserId" /> class.
        /// </summary>
        /// <param name="localpart">The localpart of the user ID (between initial <c>@</c> and first <c>:</c>).</param>
        /// <param name="domain">The domain of the user ID.</param>
        public UserId(string localpart, ServerName domain)
            : base(IdentifierType.User, SigilMapping[IdentifierType.User], localpart, domain)
        {
        }

        /// <summary>
        /// Converts a string into a user ID.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns>The converted <see cref="UserId" />.</returns>
        public static explicit operator UserId(string str) => Parse(str);

        /// <summary>
        /// Parses an identifier string into a user ID.
        /// </summary>
        /// <param name="id">The identifier to parse.</param>
        /// <returns>An instance of <see cref="UserId" />.</returns>
        public static UserId Parse(string id) => Parse<UserId>(id);

        /// <summary>
        /// Attempts to parse an identifier string into a user ID.
        /// </summary>
        /// <param name="id">The identifier to parse.</param>
        /// <param name="value">
        /// When this method returns, contains the parsed value;
        /// otherwise, the default value for <see cref="UserId" />.
        /// </param>
        /// <returns><c>true</c> if the identifier was parsed successfully; otherwise, <c>false</c>.</returns>
        public static bool TryParse(string id, out UserId value) => TryParse<UserId>(id, out value);

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(UserId other) => Equals((Identifier)other);
    }
}
