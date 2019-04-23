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

    public readonly struct AvailableContainer : IEquatable<AvailableContainer>, IEquatable<bool>,
        IComparable<AvailableContainer>, IComparable<bool>
    {
        [JsonConstructor]
        public AvailableContainer(bool available)
            : this() =>
            Available = available;

        public static implicit operator bool(AvailableContainer container) => container.Available;

        public static implicit operator AvailableContainer(bool boolean) => new AvailableContainer(boolean);

        public static bool operator ==(AvailableContainer left, AvailableContainer right) => left.Equals(right);

        public static bool operator !=(AvailableContainer left, AvailableContainer right) => !left.Equals(right);

        public static bool operator ==(AvailableContainer left, bool right) => left.Equals(right);

        public static bool operator !=(AvailableContainer left, bool right) => !left.Equals(right);

        public static bool operator ==(bool left, AvailableContainer right) => right.Equals(left);

        public static bool operator !=(bool left, AvailableContainer right) => !right.Equals(left);

        public static bool operator >(AvailableContainer left, AvailableContainer right) => left.CompareTo(right) > 0;

        public static bool operator <(AvailableContainer left, AvailableContainer right) => left.CompareTo(right) < 0;

        public static bool operator >=(AvailableContainer left, AvailableContainer right) => left.CompareTo(right) >= 0;

        public static bool operator <=(AvailableContainer left, AvailableContainer right) => left.CompareTo(right) <= 0;

        public static bool operator >(AvailableContainer left, bool right) => left.CompareTo(right) > 0;

        public static bool operator <(AvailableContainer left, bool right) => left.CompareTo(right) < 0;

        public static bool operator >=(AvailableContainer left, bool right) => left.CompareTo(right) >= 0;

        public static bool operator <=(AvailableContainer left, bool right) => left.CompareTo(right) <= 0;

        public static bool operator >(bool left, AvailableContainer right) => left.CompareTo(right) > 0;

        public static bool operator <(bool left, AvailableContainer right) => left.CompareTo(right) < 0;

        public static bool operator >=(bool left, AvailableContainer right) => left.CompareTo(right) >= 0;

        public static bool operator <=(bool left, AvailableContainer right) => left.CompareTo(right) <= 0;

        public static bool operator true(AvailableContainer container) => container.Available;

        public static bool operator false(AvailableContainer container) => !container.Available;

        [JsonProperty("available")]
        public bool Available { get; }

        public bool Equals(AvailableContainer other) => Available == other.Available;

        public bool Equals(bool other) => Available == other;

        public int CompareTo(AvailableContainer other) => Available.CompareTo(other.Available);

        public int CompareTo(bool other) => Available.CompareTo(other);

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

        public override int GetHashCode() => Available.GetHashCode();
    }
}
