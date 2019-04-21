// <copyright file="Identifier.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Helpers;

    using Newtonsoft.Json;

    using Serialization;

    [JsonConverter(typeof(IdentifierConverter))]
    public readonly struct Identifier : IEquatable<Identifier>, IEquatable<string>
    {
        private const char Separator = ':';

        private const string SeparatorString = ":";

        private static readonly IReadOnlyDictionary<char, IdentifierType> TypeMapping =
            new Dictionary<char, IdentifierType>
            {
                ['@'] = IdentifierType.User,
                ['!'] = IdentifierType.Room,
                ['$'] = IdentifierType.Event,
                ['+'] = IdentifierType.Group,
                ['#'] = IdentifierType.RoomAlias
            };

        private static readonly IReadOnlyDictionary<IdentifierType, char> SigilMapping =
            new Dictionary<IdentifierType, char>
            {
                [IdentifierType.User] = '@',
                [IdentifierType.Room] = '!',
                [IdentifierType.Event] = '$',
                [IdentifierType.Group] = '+',
                [IdentifierType.RoomAlias] = '#'
            };

        public Identifier(string id)
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

            Sigil = id[0];

            var validSigil = TypeMapping.TryGetValue(Sigil, out var type);

            if (!validSigil)
            {
                throw new ArgumentException("ID must start with a valid sigil character", nameof(id));
            }

            Type = type;

            if (!id.Contains(SeparatorString))
            {
                if (Type != IdentifierType.Event && Type != IdentifierType.Room)
                {
                    throw new ArgumentException("Only event and room IDs can omit domain", nameof(id));
                }

                Localpart = id.Substring(1);

                if (string.IsNullOrWhiteSpace(Localpart))
                {
                    throw new ArgumentException("ID cannot have empty localpart", nameof(id));
                }

                Domain = null;
                return;
            }

            var sepIndex = id.IndexOf(Separator);

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

        public Identifier(IdentifierType type, string localpart, ServerName? domain = null)
            : this(type, SigilMapping[type], localpart, domain)
        {
        }

        [JsonConstructor]
        public Identifier(IdentifierType type, char sigil, string localpart, ServerName? domain = null)
        {
            if (localpart == null)
            {
                throw new ArgumentNullException(nameof(localpart));
            }

            if (string.IsNullOrWhiteSpace(localpart))
            {
                throw new ArgumentException("ID cannot have empty localpart", nameof(localpart));
            }

            if (domain == null && type != IdentifierType.Event)
            {
                throw new ArgumentException("Only event IDs can omit the domain", nameof(domain));
            }

            Type = type;
            Sigil = sigil;
            Localpart = localpart;
            Domain = domain;
        }

        public static implicit operator string(Identifier identifier) => identifier.ToString();

        public static explicit operator Identifier(string str) => new Identifier(str);

        public static bool operator ==(Identifier left, Identifier right) => left.Equals(right);

        public static bool operator !=(Identifier left, Identifier right) => !left.Equals(right);

        public static bool operator ==(Identifier left, string right) => left.Equals(right);

        public static bool operator !=(Identifier left, string right) => !left.Equals(right);

        public static bool operator ==(string left, Identifier right) => right.Equals(left);

        public static bool operator !=(string left, Identifier right) => right.Equals(left);

        public IdentifierType Type { get; }

        public char Sigil { get; }

        public string Localpart { get; }

        public ServerName? Domain { get; }

        public static Identifier User(string localpart, string domain) => User(localpart, new ServerName(domain));

        public static Identifier User(string localpart, ServerName domain) =>
            new Identifier(IdentifierType.User, localpart, domain);

        public static Identifier Room(string localpart) => new Identifier(IdentifierType.Room, localpart);

        public static Identifier Room(string localpart, string domain) => Room(localpart, new ServerName(domain));

        public static Identifier Room(string localpart, ServerName domain) =>
            new Identifier(IdentifierType.Room, localpart, domain);

        public static Identifier Event(string localpart) => new Identifier(IdentifierType.Event, localpart);

        public static Identifier Event(string localpart, string domain) => Event(localpart, new ServerName(domain));

        public static Identifier Event(string localpart, ServerName domain) =>
            new Identifier(IdentifierType.Event, localpart, domain);

        public static Identifier Group(string localpart, string domain) => Group(localpart, new ServerName(domain));

        public static Identifier Group(string localpart, ServerName domain) =>
            new Identifier(IdentifierType.Group, localpart, domain);

        public static Identifier RoomAlias(string localpart, string domain) =>
            RoomAlias(localpart, new ServerName(domain));

        public static Identifier RoomAlias(string localpart, ServerName domain) =>
            new Identifier(IdentifierType.RoomAlias, localpart, domain);

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case null:
                    return false;

                case string str:
                    return Equals(str);

                default:
                    return obj is Identifier other && Equals(other);
            }
        }

        public bool Equals(string other) => string.Equals(ToString(), other);

        public bool Equals(Identifier other) =>
            Type == other.Type && Sigil == other.Sigil && Localpart == other.Localpart && Domain == other.Domain;

        public override int GetHashCode() => HashCode.Combine(Type, Sigil, Localpart, Domain);

        public override string ToString()
        {
            var sb = new StringBuilder($"{Sigil}{Localpart}");

            if (Domain == null)
            {
                return sb.ToString();
            }

            sb.Append($":{Domain}");

            return sb.ToString();
        }
    }
}
