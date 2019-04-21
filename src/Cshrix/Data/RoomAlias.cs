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

    using Newtonsoft.Json;

    using Serialization;

    [JsonConverter(typeof(RoomAliasConverter))]
    public class RoomAlias : Identifier, IEquatable<RoomAlias>
    {
        public RoomAlias(string localpart, string domain)
            : this(localpart, new ServerName(domain))
        {
        }

        public RoomAlias(string localpart, ServerName domain)
            : base(IdentifierType.User, SigilMapping[IdentifierType.User], localpart, domain)
        {
        }

        public static explicit operator RoomAlias(string str) => Parse(str);

        public static RoomAlias Parse(string id)
        {
            var identifier = ParseId(id);

            if (!(identifier is RoomAlias alias))
            {
                throw new ArgumentException("ID must be of room alias type", nameof(id));
            }

            return alias;
        }

        public static bool TryParse(string id, out RoomAlias value)
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

        public bool Equals(RoomAlias other) => Equals((Identifier)other);
    }
}
