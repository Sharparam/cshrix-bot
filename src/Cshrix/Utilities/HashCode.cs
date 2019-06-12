// <copyright file="HashCode.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Utilities
{
    using JetBrains.Annotations;

    /// <summary>
    /// Contains data and methods to aid in generating hashcodes.
    /// </summary>
    /// <remarks>
    /// References:
    ///  * https://stackoverflow.com/a/263416/1104531
    ///  * https://github.com/dotnet/coreclr/pull/14863
    ///  * https://docs.microsoft.com/en-us/visualstudio/ide/reference/generate-equals-gethashcode-methods?view=vs-2017
    /// </remarks>
    [PublicAPI]
    internal static class HashCode
    {
        /// <summary>
        /// Base value for each hash (prevents <c>0</c> being used).
        /// </summary>
        private const int Initializer = 352033288;

        /// <summary>
        /// Value to multiply with for each hashed field.
        /// </summary>
        private const int Multiplier = -1521134295;

        /// <summary>
        /// "Combines" the hash of one value.
        /// </summary>
        /// <param name="value">The value to hash.</param>
        /// <typeparam name="T">The type of <paramref name="value" />.</typeparam>
        /// <returns>The combined hash value of each value.</returns>
        internal static int Combine<T>(T value) => value.GetHashCode();

        /// <summary>
        /// Combines the hash of multiple values.
        /// </summary>
        /// <param name="value1">First value to hash.</param>
        /// <param name="value2">Second value to hash.</param>
        /// <typeparam name="T1">Type of <paramref name="value1" />.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="value2" />.</typeparam>
        /// <returns>The combined hash of all values.</returns>
        internal static int Combine<T1, T2>(T1 value1, T2 value2)
        {
            unchecked
            {
                var hash = Initializer;
                hash = hash * Multiplier + (value1?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value2?.GetHashCode() ?? 0);
                return hash;
            }
        }

        /// <summary>
        /// Combines the hash of multiple values.
        /// </summary>
        /// <param name="value1">First value to hash.</param>
        /// <param name="value2">Second value to hash.</param>
        /// <param name="value3">Third value to hash.</param>
        /// <typeparam name="T1">Type of <paramref name="value1" />.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="value2" />.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="value3" />.</typeparam>
        /// <returns>The combined hash of all values.</returns>
        internal static int Combine<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            unchecked
            {
                var hash = Initializer;
                hash = hash * Multiplier + (value1?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value2?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value3?.GetHashCode() ?? 0);
                return hash;
            }
        }

        /// <summary>
        /// Combines the hash of multiple values.
        /// </summary>
        /// <param name="value1">First value to hash.</param>
        /// <param name="value2">Second value to hash.</param>
        /// <param name="value3">Third value to hash.</param>
        /// <param name="value4">Fourth value to hash.</param>
        /// <typeparam name="T1">Type of <paramref name="value1" />.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="value2" />.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="value3" />.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="value4" />.</typeparam>
        /// <returns>The combined hash of all values.</returns>
        internal static int Combine<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            unchecked
            {
                var hash = Initializer;
                hash = hash * Multiplier + (value1?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value2?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value3?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value4?.GetHashCode() ?? 0);
                return hash;
            }
        }

        /// <summary>
        /// Combines the hash of multiple values.
        /// </summary>
        /// <param name="value1">First value to hash.</param>
        /// <param name="value2">Second value to hash.</param>
        /// <param name="value3">Third value to hash.</param>
        /// <param name="value4">Fourth value to hash.</param>
        /// <param name="value5">Fifth value to hash.</param>
        /// <typeparam name="T1">Type of <paramref name="value1" />.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="value2" />.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="value3" />.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="value4" />.</typeparam>
        /// <typeparam name="T5">Type of <paramref name="value5" />.</typeparam>
        /// <returns>The combined hash of all values.</returns>
        internal static int Combine<T1, T2, T3, T4, T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
        {
            unchecked
            {
                var hash = Initializer;
                hash = hash * Multiplier + (value1?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value2?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value3?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value4?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value5?.GetHashCode() ?? 0);
                return hash;
            }
        }

        /// <summary>
        /// Combines the hash of multiple values.
        /// </summary>
        /// <param name="value1">First value to hash.</param>
        /// <param name="value2">Second value to hash.</param>
        /// <param name="value3">Third value to hash.</param>
        /// <param name="value4">Fourth value to hash.</param>
        /// <param name="value5">Fifth value to hash.</param>
        /// <param name="value6">Sixth value to hash.</param>
        /// <typeparam name="T1">Type of <paramref name="value1" />.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="value2" />.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="value3" />.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="value4" />.</typeparam>
        /// <typeparam name="T5">Type of <paramref name="value5" />.</typeparam>
        /// <typeparam name="T6">Type of <paramref name="value6" />.</typeparam>
        /// <returns>The combined hash of all values.</returns>
        internal static int Combine<T1, T2, T3, T4, T5, T6>(
            T1 value1,
            T2 value2,
            T3 value3,
            T4 value4,
            T5 value5,
            T6 value6)
        {
            unchecked
            {
                var hash = Initializer;
                hash = hash * Multiplier + (value1?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value2?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value3?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value4?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value5?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value6?.GetHashCode() ?? 0);
                return hash;
            }
        }

        /// <summary>
        /// Combines the hash of multiple values.
        /// </summary>
        /// <param name="value1">First value to hash.</param>
        /// <param name="value2">Second value to hash.</param>
        /// <param name="value3">Third value to hash.</param>
        /// <param name="value4">Fourth value to hash.</param>
        /// <param name="value5">Fifth value to hash.</param>
        /// <param name="value6">Sixth value to hash.</param>
        /// <param name="value7">Seventh value to hash.</param>
        /// <typeparam name="T1">Type of <paramref name="value1" />.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="value2" />.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="value3" />.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="value4" />.</typeparam>
        /// <typeparam name="T5">Type of <paramref name="value5" />.</typeparam>
        /// <typeparam name="T6">Type of <paramref name="value6" />.</typeparam>
        /// <typeparam name="T7">Type of <paramref name="value7" />.</typeparam>
        /// <returns>The combined hash of all values.</returns>
        internal static int Combine<T1, T2, T3, T4, T5, T6, T7>(
            T1 value1,
            T2 value2,
            T3 value3,
            T4 value4,
            T5 value5,
            T6 value6,
            T7 value7)
        {
            unchecked
            {
                var hash = Initializer;
                hash = hash * Multiplier + (value1?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value2?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value3?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value4?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value5?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value6?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value7?.GetHashCode() ?? 0);
                return hash;
            }
        }

        /// <summary>
        /// Combines the hash of multiple values.
        /// </summary>
        /// <param name="value1">First value to hash.</param>
        /// <param name="value2">Second value to hash.</param>
        /// <param name="value3">Third value to hash.</param>
        /// <param name="value4">Fourth value to hash.</param>
        /// <param name="value5">Fifth value to hash.</param>
        /// <param name="value6">Sixth value to hash.</param>
        /// <param name="value7">Seventh value to hash.</param>
        /// <param name="value8">Eighth value to hash.</param>
        /// <typeparam name="T1">Type of <paramref name="value1" />.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="value2" />.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="value3" />.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="value4" />.</typeparam>
        /// <typeparam name="T5">Type of <paramref name="value5" />.</typeparam>
        /// <typeparam name="T6">Type of <paramref name="value6" />.</typeparam>
        /// <typeparam name="T7">Type of <paramref name="value7" />.</typeparam>
        /// <typeparam name="T8">Type of <paramref name="value8" />.</typeparam>
        /// <returns>The combined hash of all values.</returns>
        internal static int Combine<T1, T2, T3, T4, T5, T6, T7, T8>(
            T1 value1,
            T2 value2,
            T3 value3,
            T4 value4,
            T5 value5,
            T6 value6,
            T7 value7,
            T8 value8)
        {
            unchecked
            {
                var hash = Initializer;
                hash = hash * Multiplier + (value1?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value2?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value3?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value4?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value5?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value6?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value7?.GetHashCode() ?? 0);
                hash = hash * Multiplier + (value8?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}
