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

    [JsonConverter(typeof(UserIdConverter))]
    public readonly struct UserId : IIdentifier
    {
        [JsonConstructor]
        public UserId(string id)
            : this()
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "ID string cannot be null");
            }

            id = id.Trim();

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("ID cannot be empty or full of whitespace", nameof(id));
            }

            if (!id.StartsWith("@"))
            {
                throw new ArgumentException("ID must start with '@'", nameof(id));
            }

            if (!id.Contains(":"))
            {
                throw new ArgumentException("ID must contain a ':'", nameof(id));
            }

            var sepIndex = id.IndexOf(':');

            if (sepIndex == 1)
            {
                throw new ArgumentException("ID cannot have empty localpart", nameof(id));
            }

            if (sepIndex == id.Length - 1)
            {
                throw new ArgumentException("ID cannot have empty domain");
            }

            Localpart = id.Substring(1, sepIndex - 1);

            if (string.IsNullOrWhiteSpace(Localpart))
            {
                throw new ArgumentException("ID cannot have empty localpart", nameof(id));
            }

            Domain = new ServerName(id.Substring(sepIndex + 1));
        }

        public UserId(string localpart, ServerName domain)
        {
            if (localpart == null)
            {
                throw new ArgumentNullException(nameof(localpart));
            }

            if (string.IsNullOrWhiteSpace(localpart))
            {
                throw new ArgumentException("ID cannot have empty localpart", nameof(localpart));
            }

            Localpart = localpart;
            Domain = domain;
        }

        public IdentifierType Type => IdentifierType.User;

        public char Sigil => '@';

        public string Localpart { get; }

        public ServerName Domain { get; }

        public override string ToString() => $"{Sigil}{Localpart}:{Domain}";
    }
}
