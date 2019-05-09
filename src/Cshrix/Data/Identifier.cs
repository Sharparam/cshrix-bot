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

    using Utilities;

    public abstract class Identifier : IEquatable<Identifier>, IEquatable<string>
    {
        private const char Separator = ':';

        private const string SeparatorString = ":";

        protected static readonly IReadOnlyDictionary<IdentifierType, char> SigilMapping =
            new Dictionary<IdentifierType, char>
            {
                [IdentifierType.User] = '@',
                [IdentifierType.Group] = '+',
                [IdentifierType.RoomAlias] = '#'
            };

        private static readonly IReadOnlyDictionary<char, IdentifierType> TypeMapping =
            new Dictionary<char, IdentifierType>
            {
                ['@'] = IdentifierType.User,
                ['+'] = IdentifierType.Group,
                ['#'] = IdentifierType.RoomAlias
            };

        protected Identifier(IdentifierType type, string localpart, ServerName domain)
            : this(type, SigilMapping[type], localpart, domain)
        {
        }

        protected Identifier(IdentifierType type, char sigil, string localpart, ServerName domain)
        {
            if (localpart == null)
            {
                throw new ArgumentNullException(nameof(localpart));
            }

            if (string.IsNullOrWhiteSpace(localpart))
            {
                throw new ArgumentException("ID cannot have empty localpart", nameof(localpart));
            }

            Type = type;
            Sigil = sigil;
            Localpart = localpart.Trim();
            Domain = domain;
        }

        public static implicit operator string(Identifier identifier) => identifier.ToString();

        public static bool operator ==(Identifier left, Identifier right) =>
            left?.Equals(right) ?? ReferenceEquals(null, right);

        public static bool operator !=(Identifier left, Identifier right) =>
            left?.Equals(right) ?? !ReferenceEquals(null, right);

        public static bool operator ==(Identifier left, string right) =>
            left?.Equals(right) ?? ReferenceEquals(null, right);

        public static bool operator !=(Identifier left, string right) =>
            left?.Equals(right) ?? ReferenceEquals(null, right);

        public static bool operator ==(string left, Identifier right) =>
            right?.Equals(left) ?? ReferenceEquals(null, left);

        public static bool operator !=(string left, Identifier right) =>
            right?.Equals(left) ?? !ReferenceEquals(null, left);

        public IdentifierType Type { get; }

        public char Sigil { get; }

        public string Localpart { get; }

        public ServerName Domain { get; }

        protected static Identifier ParseId(string id)
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

            var sigil = id[0];

            var validSigil = TypeMapping.TryGetValue(sigil, out var type);

            if (!validSigil)
            {
                throw new ArgumentException("ID must start with a valid sigil character", nameof(id));
            }

            if (!id.Contains(SeparatorString))
            {
                throw new ArgumentException("ID cannot have empty domain", nameof(id));
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

            var localpart = id.Substring(1, sepIndex - 1);

            if (string.IsNullOrWhiteSpace(localpart))
            {
                throw new ArgumentException("ID cannot have empty localpart", nameof(id));
            }

            var domain = new ServerName(id.Substring(sepIndex + 1));

            switch (type)
            {
                case IdentifierType.User:
                    return new UserId(localpart, domain);

                case IdentifierType.Group:
                    return new GroupId(localpart, domain);

                case IdentifierType.RoomAlias:
                    return new RoomAlias(localpart, domain);

                default:
                    throw new ArgumentException("ID was not of a supported type", nameof(id));
            }
        }

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

        public bool Equals(string other) => string.Equals(ToString(), other, StringComparison.InvariantCulture);

        public bool Equals(Identifier other) =>
            Type == other?.Type && Sigil == other.Sigil && Localpart == other.Localpart && Domain == other.Domain;

        public override int GetHashCode() => HashCode.Combine(Type, Sigil, Localpart, Domain);

        public override string ToString() => $"{Sigil}{Localpart}{Separator}{Domain}";
    }
}
