// <copyright file="ServerName.cs">
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

    using Utilities;

    /// <summary>
    /// Represents a homeserver domain.
    /// </summary>
    [JsonConverter(typeof(ServerNameConverter))]
    public readonly struct ServerName : IEquatable<ServerName>, IEquatable<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerName" /> structure.
        /// </summary>
        /// <param name="raw">The domain string to parse.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="raw" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="raw" /> is empty or full of whitespace.
        /// </exception>
        public ServerName([NotNull] string raw)
            : this()
        {
            if (raw == null)
            {
                throw new ArgumentNullException(nameof(raw));
            }

            if (string.IsNullOrWhiteSpace(raw))
            {
                throw new ArgumentException("Input string cannot be empty", nameof(raw));
            }

            var split = raw.Split(':');

            Hostname = split[0];

            Port = split.Length > 1 ? (ushort?)ushort.Parse(split[1]) : null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerName" /> structure.
        /// </summary>
        /// <param name="hostname">The hostname part of the server name.</param>
        /// <param name="port">Optional port for the server name.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="hostname" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="hostname" /> is empty or full of whitespace.
        /// </exception>
        [JsonConstructor]
        public ServerName([NotNull] string hostname, ushort? port)
            : this()
        {
            if (hostname == null)
            {
                throw new ArgumentNullException(nameof(hostname));
            }

            if (string.IsNullOrWhiteSpace(hostname))
            {
                throw new ArgumentException("Hostname cannot be empty", nameof(hostname));
            }

            Hostname = hostname;
            Port = port;
        }

        /// <summary>
        /// Checks if two instance of <see cref="ServerName" /> are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(ServerName left, ServerName right) => left.Equals(right);

        /// <summary>
        /// Checks if two instance of <see cref="ServerName" /> are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> and <paramref name="right" /> are not equal;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(ServerName left, ServerName right) => !left.Equals(right);

        /// <summary>
        /// Checks if an instance of <see cref="ServerName" /> and a <see cref="string" /> are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(ServerName left, string right) => left.Equals(right);

        /// <summary>
        /// Checks if an instance of <see cref="ServerName" /> and a <see cref="string" /> are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> and <paramref name="right" /> are not equal;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(ServerName left, string right) => !left.Equals(right);

        /// <summary>
        /// Checks if a <see cref="string" /> and an instance of <see cref="ServerName" /> are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(string left, ServerName right) => right.Equals(left);

        /// <summary>
        /// Checks if a <see cref="string" /> and an instance of <see cref="ServerName" /> are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> and <paramref name="right" /> are not equal;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(string left, ServerName right) => right.Equals(left);

        /// <summary>
        /// Gets the hostname part of this <see cref="ServerName" />.
        /// </summary>
        [NotNull]
        public string Hostname { get; }

        /// <summary>
        /// Gets the port of this <see cref="ServerName" />.
        /// </summary>
        [PublicAPI]
        public ushort? Port { get; }

        /// <summary>
        /// Gets the port value, or a default value if it's not set.
        /// </summary>
        /// <param name="defaultValue">
        /// The default value to return if a port isn't set on this <see cref="ServerName" />.
        /// </param>
        /// <returns>A port value.</returns>
        [PublicAPI]
        public ushort GetPortOrDefault(ushort defaultValue = default) => Port ?? defaultValue;

        /// <inheritdoc />
        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(ServerName other) => string.Equals(Hostname, other.Hostname) && Port == other.Port;

        /// <inheritdoc />
        /// <summary>Indicates whether the current object is equal to a <see cref="string" />.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(string other) => string.Equals(ToString(), other);

        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="obj" /> and this instance are the same type and represent the same value;
        /// otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case null:
                    return false;

                case string otherString:
                    return Equals(otherString);

                default:
                    return obj is ServerName other && Equals(other);
            }
        }

        /// <inheritdoc />
        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode() => Utilities.HashCode.Combine(Hostname, Port);

        /// <inheritdoc />
        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => Port.HasValue ? $"{Hostname}:{Port}" : Hostname;
    }
}
