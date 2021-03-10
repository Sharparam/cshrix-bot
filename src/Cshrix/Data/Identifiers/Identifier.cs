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

    /// <summary>
    /// Represents an abstract identifier.
    /// </summary>
    public abstract class Identifier : IEquatable<Identifier>, IEquatable<string>
    {
        /// <summary>
        /// A separator character for different parts of the identifier.
        /// </summary>
        private const char Separator = ':';

        /// <summary>
        /// A separator string for different parts of the identifier.
        /// </summary>
        private const string SeparatorString = ":";

        /// <summary>
        /// A dictionary mapping identifier types to their sigil.
        /// </summary>
        protected static readonly IReadOnlyDictionary<IdentifierType, char> SigilMapping =
            new Dictionary<IdentifierType, char>
            {
                [IdentifierType.User] = '@',
                [IdentifierType.Group] = '+',
                [IdentifierType.RoomAlias] = '#'
            };

        /// <summary>
        /// A dictionary mapping sigils to their identifier type.
        /// </summary>
        private static readonly IReadOnlyDictionary<char, IdentifierType> TypeMapping =
            new Dictionary<char, IdentifierType>
            {
                ['@'] = IdentifierType.User,
                ['+'] = IdentifierType.Group,
                ['#'] = IdentifierType.RoomAlias
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="Identifier" /> class.
        /// </summary>
        /// <param name="type">The type of the identifier.</param>
        /// <param name="localpart">The localpart of the identifier.</param>
        /// <param name="domain">The domain of the identifier.</param>
        protected Identifier(IdentifierType type, string localpart, ServerName domain)
            : this(type, SigilMapping[type], localpart, domain)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Identifier" /> class.
        /// </summary>
        /// <param name="type">The type of the identifier.</param>
        /// <param name="sigil">The sigil for the identifier.</param>
        /// <param name="localpart">The localpart of the identifier.</param>
        /// <param name="domain">The domain of the identifier.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="localpart" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="localpart" /> is empty or full of whitespace.
        /// </exception>
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

        /// <summary>
        /// Converts an <see cref="Identifier" /> to a <see cref="string" />.
        /// </summary>
        /// <param name="identifier">The identifier to convert.</param>
        /// <returns>A string representation of the identifier.</returns>
        public static implicit operator string(Identifier identifier) => identifier.ToString();

        /// <summary>
        /// Checks if two instances of <see cref="Identifier" /> are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two instances are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Identifier left, Identifier right) =>
            left?.Equals(right) ?? ReferenceEquals(null, right);

        /// <summary>
        /// Checks if two instances of <see cref="Identifier" /> are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two instances are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Identifier left, Identifier right) =>
            !left?.Equals(right) ?? !ReferenceEquals(null, right);

        /// <summary>
        /// Checks if an instance of <see cref="Identifier" /> and a <see cref="string" /> are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="left" /> and <paramref name="right" /> are equal;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(Identifier left, string right) =>
            left?.Equals(right) ?? ReferenceEquals(null, right);

        /// <summary>
        /// Checks if an instance of <see cref="Identifier" /> and a <see cref="string" /> are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="left" /> and <paramref name="right" /> are not equal;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(Identifier left, string right) =>
            !left?.Equals(right) ?? !ReferenceEquals(null, right);

        /// <summary>
        /// Checks if a <see cref="string" /> and an instance of <see cref="Identifier" /> are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="left" /> and <paramref name="right" /> are equal;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(string left, Identifier right) =>
            right?.Equals(left) ?? ReferenceEquals(null, left);

        /// <summary>
        /// Checks if a <see cref="string" /> and an instance of <see cref="Identifier" /> are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="left" /> and <paramref name="right" /> are not equal;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(string left, Identifier right) =>
            !right?.Equals(left) ?? !ReferenceEquals(null, left);

        /// <summary>
        /// Gets the type of this identifier.
        /// </summary>
        public IdentifierType Type { get; }

        /// <summary>
        /// Gets the sigil prefix for this identifier.
        /// </summary>
        public char Sigil { get; }

        /// <summary>
        /// Gets the localpart of this identifier.
        /// </summary>
        public string Localpart { get; }

        /// <summary>
        /// Gets the domain of this identifier.
        /// </summary>
        public ServerName Domain { get; }

        /// <summary>
        /// Parses an ID to an appropriate identifier type.
        /// </summary>
        /// <param name="id">The raw ID to parse.</param>
        /// <returns>
        /// Depending on the input string, one of the following:
        /// <list type="bullet">
        /// <item><description><see cref="UserId" />, if the input string was a user ID.</description></item>
        /// <item><description><see cref="GroupId" />, if the input string was a group ID.</description></item>
        /// <item><description><see cref="RoomAlias" />, if the input string was a room alias.</description></item>
        /// </list>
        /// If the input string is not a valid ID, an <see cref="ArgumentException" /> will be thrown.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="id" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="id" /> has an incorrect format. Refer to the exception message for
        /// more details.
        /// </exception>
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

        /// <summary>
        /// Parses an identifier string into a specific identifier type.
        /// </summary>
        /// <param name="id">The string to parse.</param>
        /// <typeparam name="T">The type of identifier to parse the string into.</typeparam>
        /// <returns>An instance of <typeparamref name="T" />.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="id" /> is not of the correct type.
        /// </exception>
        protected static T Parse<T>(string id) where T : Identifier
        {
            var identifier = ParseId(id);

            if (!(identifier is T parsed))
            {
                throw new ArgumentException($"ID must be of type {typeof(T)}", nameof(id));
            }

            return parsed;
        }

        /// <summary>
        /// Attempts to parse a string into a specific identifier type.
        /// </summary>
        /// <param name="id">The string to parse.</param>
        /// <param name="parsed">
        /// When this method returns, contains the parsed value, if parsing was successful;
        /// otherwise, the default value for <typeparamref name="T" />.
        /// </param>
        /// <typeparam name="T">The type of identifier to parse into.</typeparam>
        /// <returns><c>true</c> if the identifier was parsed successfully; otherwise, <c>false</c>.</returns>
        protected static bool TryParse<T>(string id, out T parsed) where T : Identifier
        {
            try
            {
                parsed = Parse<T>(id);
                return true;
            }
            catch (ArgumentException)
            {
                parsed = default;
                return false;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.
        /// </returns>
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

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to a <see cref="string" />.
        /// </summary>
        /// <param name="other">A <see cref="string" /> to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(string other) => string.Equals(ToString(), other, StringComparison.InvariantCulture);

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">Another <see cref="Identifier" /> to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Identifier other) =>
            Type == other?.Type && Sigil == other.Sigil && Localpart == other.Localpart && Domain == other.Domain;

        /// <inheritdoc />
        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => Utilities.HashCode.Combine(Type, Sigil, Localpart, Domain);

        /// <inheritdoc />
        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"{Sigil}{Localpart}{Separator}{Domain}";
    }
}
