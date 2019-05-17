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
    [JsonConverter(typeof(UserIdConverter))]
    public sealed class UserId : Identifier, IEquatable<UserId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserId" /> class.
        /// </summary>
        /// <param name="localpart">The localpart of the user ID (between initial <c>@</c> and first <c>:</c>.</param>
        /// <param name="domain">The domain of the user ID.</param>
        public UserId(string localpart, string domain)
            : this(localpart, new ServerName(domain))
        {
        }

        public UserId(string localpart, ServerName domain)
            : base(IdentifierType.User, SigilMapping[IdentifierType.User], localpart, domain)
        {
        }

        public static explicit operator UserId(string str) => Parse(str);

        public static UserId Parse(string id)
        {
            var identifier = ParseId(id);

            if (!(identifier is UserId userId))
            {
                throw new ArgumentException("ID must be of user type", nameof(id));
            }

            return userId;
        }

        public static bool TryParse(string id, out UserId value)
        {
            try
            {
                value = Parse(id);
                return true;
            }
            catch (ArgumentException)
            {
                value = default;
                return false;
            }
        }

        public bool Equals(UserId other) => Equals((Identifier)other);
    }
}
