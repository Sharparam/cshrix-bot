// <copyright file="EnabledContainer.cs">
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

    /// <summary>
    /// A wrapper containing a value indicating whether something is enabled or not.
    /// </summary>
    public readonly struct EnabledContainer : IEquatable<EnabledContainer>, IEquatable<bool>,
        IComparable<EnabledContainer>, IComparable<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnabledContainer" /> structure.
        /// </summary>
        /// <param name="enabled">Value indicating whether something is enabled.</param>
        [JsonConstructor]
        public EnabledContainer(bool enabled)
            : this() =>
            Enabled = enabled;

        /// <summary>
        /// Converts an instance of <see cref="EnabledContainer" /> into its equivalent <see cref="bool" /> value.
        /// </summary>
        /// <param name="container">The container to convert.</param>
        /// <returns>An equivalent <see cref="bool" /> value.</returns>
        public static implicit operator bool(EnabledContainer container) => container.Enabled;

        /// <summary>
        /// Converts a <see cref="bool" /> value into its equivalent <see cref="EnabledContainer" /> value.
        /// </summary>
        /// <param name="boolean">The boolean to convert.</param>
        /// <returns>An equivalent <see cref="EnabledContainer" /> value.</returns>
        public static implicit operator EnabledContainer(bool boolean) => new EnabledContainer(boolean);

        /// <summary>
        /// Checks if two instances of <see cref="EnabledContainer" /> are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two values are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(EnabledContainer left, EnabledContainer right) => left.Equals(right);

        /// <summary>
        /// Checks if two instances of <see cref="EnabledContainer" /> are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two values are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(EnabledContainer left, EnabledContainer right) => !left.Equals(right);

        /// <summary>
        /// Checks if an instance of <see cref="EnabledContainer" /> and a <see cref="bool" /> are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two values are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(EnabledContainer left, bool right) => left.Equals(right);

        /// <summary>
        /// Checks if an instance of <see cref="EnabledContainer" /> and a <see cref="bool" /> are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two values are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(EnabledContainer left, bool right) => !left.Equals(right);

        /// <summary>
        /// Checks if a <see cref="bool" /> and an instance of <see cref="EnabledContainer" /> are equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two values are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(bool left, EnabledContainer right) => right.Equals(left);

        /// <summary>
        /// Checks if a <see cref="bool" /> and an instance of <see cref="EnabledContainer" /> are not equal.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two values are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(bool left, EnabledContainer right) => !right.Equals(left);

        /// <summary>
        /// Checks if an instance of <see cref="EnabledContainer" /> is greater than another.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(EnabledContainer left, EnabledContainer right) => left.CompareTo(right) > 0;

        /// <summary>
        /// Checks if an instance of <see cref="EnabledContainer" /> is less than another.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is less than <paramref name="right" />; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(EnabledContainer left, EnabledContainer right) => left.CompareTo(right) < 0;

        /// <summary>
        /// Checks if an instance of <see cref="EnabledContainer" /> is greater than or equal to another.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is greater than or equal to <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >=(EnabledContainer left, EnabledContainer right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Checks if an instance of <see cref="EnabledContainer" /> is less than or equal to another.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is less than or equal to <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(EnabledContainer left, EnabledContainer right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// Checks if an instance of <see cref="EnabledContainer" /> is greater than a <see cref="bool" /> value.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(EnabledContainer left, bool right) => left.CompareTo(right) > 0;

        /// <summary>
        /// Checks if an instance of <see cref="EnabledContainer" /> is less than a <see cref="bool" /> value.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is less than <paramref name="right" />; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(EnabledContainer left, bool right) => left.CompareTo(right) < 0;

        /// <summary>
        /// Checks if an instance of <see cref="EnabledContainer" />
        /// is greater than or equal to a <see cref="bool" /> value.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is greater than or equal to <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >=(EnabledContainer left, bool right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Checks if an instance of <see cref="EnabledContainer" /> is less than or equal to a <see cref="bool" /> value.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is less than or equal to <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(EnabledContainer left, bool right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// Checks if a <see cref="bool" /> value is greater than an instance of <see cref="EnabledContainer" />.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(bool left, EnabledContainer right) => left.CompareTo(right) > 0;

        /// <summary>
        /// Checks if a <see cref="bool" /> value is less than an instance of <see cref="EnabledContainer" />.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is less than <paramref name="right" />; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(bool left, EnabledContainer right) => left.CompareTo(right) < 0;

        /// <summary>
        /// Checks if a <see cref="bool" /> value is greater than
        /// or equal to an instance of <see cref="EnabledContainer" />.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is greater than or equal to <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >=(bool left, EnabledContainer right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Checks if a <see cref="bool" /> value is less than
        /// or equal to an instance of <see cref="EnabledContainer" />.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is less than or equal to <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(bool left, EnabledContainer right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// Returns a value indicating whether a given <see cref="EnabledContainer" /> is definitely <c>true</c>.
        /// </summary>
        /// <param name="container">The container to check.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="container" /> is definitely <c>true</c>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator true(EnabledContainer container) => container.Enabled;

        /// <summary>
        /// Returns a value indicating whether a given <see cref="EnabledContainer" /> is definitely <c>false</c>.
        /// </summary>
        /// <param name="container">The container to check.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="container" /> is definitely <c>false</c>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator false(EnabledContainer container) => !container.Enabled;

        /// <summary>
        /// Gets a value indicating whether something is enabled or not.
        /// </summary>
        [JsonProperty("enabled")]
        public bool Enabled { get; }

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(EnabledContainer other) => Enabled == other.Enabled;

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to a boolean value.
        /// </summary>
        /// <param name="other">A <see cref="bool" /> to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(bool other) => Enabled == other;

        /// <inheritdoc />
        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates
        /// whether the current instances precedes, follows, or occurs in the same position in the sort order
        /// as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instances.</param>
        /// <returns>
        /// <para>A value that indicates the relative order of the objects being compared.</para>
        /// <para>
        /// The return values has these meanings:
        ///
        /// | Value   | Meaning                                                                                  |
        /// | ------- | ---------------------------------------------------------------------------------------- |
        /// | `&lt;0` | This instances precedes <paramref name="other" /> in the sort order                      |
        /// | `=0`    | This instance occurs in the same position in the sort order as <paramref name="other" /> |
        /// | `&gt;0` | This instance follows <paramref name="other" /> in the sort order.                       |
        /// </para>
        /// </returns>
        public int CompareTo(EnabledContainer other) => Enabled.CompareTo(other.Enabled);

        /// <inheritdoc />
        /// <summary>
        /// Compares the current instance with a <see cref="bool" /> and returns an integer that indicates
        /// whether the current instances precedes, follows, or occurs in the same position in the sort order
        /// as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instances.</param>
        /// <returns>
        /// <para>A value that indicates the relative order of the objects being compared.</para>
        /// <para>
        /// The return values has these meanings:
        ///
        /// | Value   | Meaning                                                                                  |
        /// | ------- | ---------------------------------------------------------------------------------------- |
        /// | `&lt;0` | This instances precedes <paramref name="other" /> in the sort order                      |
        /// | `=0`    | This instance occurs in the same position in the sort order as <paramref name="other" /> |
        /// | `&gt;0` | This instance follows <paramref name="other" /> in the sort order.                       |
        /// </para>
        /// </returns>
        public int CompareTo(bool other) => Enabled.CompareTo(other);

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="obj" /> and this instance are the same type and represent the same value;
        /// otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            switch (obj)
            {
                case bool otherBool:
                    return Equals(otherBool);

                default:
                    return obj is EnabledContainer other && Equals(other);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode() => Enabled.GetHashCode();
    }
}
