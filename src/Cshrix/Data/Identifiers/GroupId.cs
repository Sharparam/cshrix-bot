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

    [JsonConverter(typeof(GroupIdConverter))]
    public sealed class GroupId : Identifier, IEquatable<GroupId>
    {
        public GroupId(string localpart, string domain)
            : this(localpart, new ServerName(domain))
        {
        }

        public GroupId(string localpart, ServerName domain)
            : base(IdentifierType.User, SigilMapping[IdentifierType.User], localpart, domain)
        {
        }

        public static explicit operator GroupId(string str) => Parse(str);

        public static GroupId Parse(string id)
        {
            var identifier = ParseId(id);

            if (!(identifier is GroupId groupId))
            {
                throw new ArgumentException("ID must be of group type", nameof(id));
            }

            return groupId;
        }

        public static bool TryParse(string id, out GroupId value)
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

        public bool Equals(GroupId other) => Equals((Identifier)other);
    }
}
