// <copyright file="AvailableContainer.cs">
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

    /// <inheritdoc cref="IEquatable{T}" />
    /// <inheritdoc cref="IComparable{T}" />
    /// <summary>
    /// Contains a boolean value indicating whether a resource or action is available.
    /// </summary>
    public readonly struct AvailableContainer : IEquatable<AvailableContainer>, IEquatable<bool>,
        IComparable<AvailableContainer>, IComparable<bool>
    {
        /// <summary>
        /// Initializes a new instance of this <see cref="AvailableContainer" /> structure.
        /// </summary>
        /// <param name="isAvailable">Whether or not the resource or action is available.</param>
        [JsonConstructor]
        public AvailableContainer(bool isAvailable)
            : this() =>
            IsAvailable = isAvailable;

        /// <summary>
        /// Converts an <see cref="AvailableContainer" /> value to a <see cref="bool" />.
        /// </summary>
        /// <param name="container">The container to convert.</param>
        /// <returns>The converted value.</returns>
        public static implicit operator bool(AvailableContainer container) => container.IsAvailable;

        /// <summary>
        /// Converts a <see cref="bool" /> to an <see cref="AvailableContainer" />.
        /// </summary>
        /// <param name="boolean">The boolean to convert.</param>
        /// <returns>An instance of <see cref="AvailableContainer" />.</returns>
        public static implicit operator AvailableContainer(bool boolean) => new AvailableContainer(boolean);

        /// <summary>
        /// Compares two instance of <see cref="AvailableContainer" /> for equality.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two instances are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(AvailableContainer left, AvailableContainer right) => left.Equals(right);

        /// <summary>
        /// Compares two instance of <see cref="AvailableContainer" /> for inequality.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two instances are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(AvailableContainer left, AvailableContainer right) => !left.Equals(right);

        /// <summary>
        /// Compares an <see cref="AvailableContainer" /> and <see cref="bool" /> for equality.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(AvailableContainer left, bool right) => left.Equals(right);

        /// <summary>
        /// Compares an <see cref="AvailableContainer" /> and <see cref="bool" /> for inequality.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(AvailableContainer left, bool right) => !left.Equals(right);

        /// <summary>
        /// Compares a <see cref="bool" /> and <see cref="AvailableContainer" /> for equality.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(bool left, AvailableContainer right) => right.Equals(left);

        /// <summary>
        /// Compares a <see cref="bool" /> and <see cref="AvailableContainer" /> for inequality.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(bool left, AvailableContainer right) => !right.Equals(left);

        /// <summary>
        /// Checks if an <see cref="AvailableContainer" /> is greater than another.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is greater than <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(AvailableContainer left, AvailableContainer right) => left.CompareTo(right) > 0;

        /// <summary>
        /// Checks if an <see cref="AvailableContainer" /> is less than another.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is less than <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(AvailableContainer left, AvailableContainer right) => left.CompareTo(right) < 0;

        /// <summary>
        /// Checks if an <see cref="AvailableContainer" /> is greater than or equal to another.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is greater than or equal to <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >=(AvailableContainer left, AvailableContainer right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Checks if an <see cref="AvailableContainer" /> is less than or equal to another.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is less than or equal to <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(AvailableContainer left, AvailableContainer right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// Checks if an <see cref="AvailableContainer" /> is greater than a <see cref="bool" />.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is greater than <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(AvailableContainer left, bool right) => left.CompareTo(right) > 0;

        /// <summary>
        /// Checks if an <see cref="AvailableContainer" /> is less than a <see cref="bool" />.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is less than <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(AvailableContainer left, bool right) => left.CompareTo(right) < 0;

        /// <summary>
        /// Checks if an <see cref="AvailableContainer" /> is greater than or equal to a <see cref="bool" />.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is greater than or equal to <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >=(AvailableContainer left, bool right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Checks if an <see cref="AvailableContainer" /> is less than or equal to a <see cref="bool" />.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is less than or equal to <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(AvailableContainer left, bool right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// Checks if a <see cref="bool" /> is greater than an <see cref="AvailableContainer" />.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is greater than <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(bool left, AvailableContainer right) => left.CompareTo(right) > 0;

        /// <summary>
        /// Checks if a <see cref="bool" /> is less than an <see cref="AvailableContainer" />.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is less than <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(bool left, AvailableContainer right) => left.CompareTo(right) < 0;

        /// <summary>
        /// Checks if a <see cref="bool" /> is greater than or equal to an <see cref="AvailableContainer" />.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is greater than or equal to <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >=(bool left, AvailableContainer right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Checks if a <see cref="bool" /> is less than or equal to an <see cref="AvailableContainer" />.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is less than or equal to <paramref name="right" />;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(bool left, AvailableContainer right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// Gets a value indicating whether a given <see cref="AvailableContainer" /> is definitely <c>true</c>.
        /// </summary>
        /// <param name="container">The container to check.</param>
        /// <returns><c>true</c> if the container is definitely <c>true</c>; otherwise, <c>false</c>.</returns>
        public static bool operator true(AvailableContainer container) => container.IsAvailable;

        /// <summary>
        /// Gets a value indicating whether a given <see cref="AvailableContainer" /> is definitely <c>false</c>.
        /// </summary>
        /// <param name="container">The container to check.</param>
        /// <returns><c>true</c> if the container is definitely <c>false</c>; otherwise, <c>false</c>.</returns>
        public static bool operator false(AvailableContainer container) => !container.IsAvailable;

        /// <summary>
        /// Gets a value indicating whether the relevant resource or action is available.
        /// </summary>
        [JsonProperty("available")]
        public bool IsAvailable { get; }

        /// <inheritdoc />
        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other">other</paramref> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(AvailableContainer other) => IsAvailable == other.IsAvailable;

        /// <inheritdoc />
        /// <summary>Indicates whether the current object is equal to a <see cref="bool" />.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other">other</paramref> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(bool other) => IsAvailable == other;

        /// <inheritdoc />
        /// <summary>
        /// Compares this instance to another object of type <see cref="AvailableContainer" />.
        /// </summary>
        /// <param name="other">The value to compare with.</param>
        /// <returns>
        /// <c>-1</c> if this value comes before <paramref name="other" />,
        /// <c>0</c> if this value and <paramref name="other" /> are equal,
        /// <c>1</c> if this value comes after <paramref name="other" />.
        /// </returns>
        public int CompareTo(AvailableContainer other) => IsAvailable.CompareTo(other.IsAvailable);

        /// <inheritdoc />
        /// <summary>
        /// Compares this instance of <see cref="AvailableContainer" /> to a <see cref="bool" /> value.
        /// </summary>
        /// <para>The value to compare with.</para>
        /// <returns>
        /// <c>-1</c> if this value comes before <paramref name="other" />,
        /// <c>0</c> if this value and <paramref name="other" /> are equal,
        /// <c>1</c> if this value comes after <paramref name="other" />.
        /// </returns>
        public int CompareTo(bool other) => IsAvailable.CompareTo(other);

        /// <inheritdoc />
        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="obj">obj</paramref> and this instance are the same type and represent
        /// the same value; otherwise, <c>false</c>.
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
                    return obj is AvailableContainer other && Equals(other);
            }
        }

        /// <inheritdoc />
        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode() => IsAvailable.GetHashCode();
    }
}
