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

    public readonly struct EnabledContainer : IEquatable<EnabledContainer>, IEquatable<bool>,
        IComparable<EnabledContainer>, IComparable<bool>
    {
        [JsonConstructor]
        public EnabledContainer(bool enabled)
            : this() =>
            Enabled = enabled;

        public static implicit operator bool(EnabledContainer container) => container.Enabled;

        public static implicit operator EnabledContainer(bool boolean) => new EnabledContainer(boolean);

        public static bool operator ==(EnabledContainer left, EnabledContainer right) => left.Equals(right);

        public static bool operator !=(EnabledContainer left, EnabledContainer right) => !left.Equals(right);

        public static bool operator ==(EnabledContainer left, bool right) => left.Equals(right);

        public static bool operator !=(EnabledContainer left, bool right) => !left.Equals(right);

        public static bool operator ==(bool left, EnabledContainer right) => right.Equals(left);

        public static bool operator !=(bool left, EnabledContainer right) => !right.Equals(left);

        public static bool operator >(EnabledContainer left, EnabledContainer right) => left.CompareTo(right) > 0;

        public static bool operator <(EnabledContainer left, EnabledContainer right) => left.CompareTo(right) < 0;

        public static bool operator >=(EnabledContainer left, EnabledContainer right) => left.CompareTo(right) >= 0;

        public static bool operator <=(EnabledContainer left, EnabledContainer right) => left.CompareTo(right) <= 0;

        public static bool operator >(EnabledContainer left, bool right) => left.CompareTo(right) > 0;

        public static bool operator <(EnabledContainer left, bool right) => left.CompareTo(right) < 0;

        public static bool operator >=(EnabledContainer left, bool right) => left.CompareTo(right) >= 0;

        public static bool operator <=(EnabledContainer left, bool right) => left.CompareTo(right) <= 0;

        public static bool operator >(bool left, EnabledContainer right) => left.CompareTo(right) > 0;

        public static bool operator <(bool left, EnabledContainer right) => left.CompareTo(right) < 0;

        public static bool operator >=(bool left, EnabledContainer right) => left.CompareTo(right) >= 0;

        public static bool operator <=(bool left, EnabledContainer right) => left.CompareTo(right) <= 0;

        public static bool operator true(EnabledContainer container) => container.Enabled;

        public static bool operator false(EnabledContainer container) => !container.Enabled;

        [JsonProperty("enabled")]
        public bool Enabled { get; }

        public bool Equals(EnabledContainer other) => Enabled == other.Enabled;

        public bool Equals(bool other) => Enabled == other;

        public int CompareTo(EnabledContainer other) => Enabled.CompareTo(other.Enabled);

        public int CompareTo(bool other) => Enabled.CompareTo(other);

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

        public override int GetHashCode() => Enabled.GetHashCode();
    }
}
