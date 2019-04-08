// <copyright file="HashCode.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Helpers
{
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Contains data and methods to aid in generating hashcodes.
    /// </summary>
    /// <remarks>
    /// References:
    ///  * https://stackoverflow.com/a/263416/1104531
    ///  * https://github.com/dotnet/coreclr/pull/14863
    ///  * https://docs.microsoft.com/en-us/visualstudio/ide/reference/generate-equals-gethashcode-methods?view=vs-2017
    /// </remarks>
    internal static class HashCode
    {
        public const int Initializer = 352033288;

        public const int Multiplier = -1521134295;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T>(T value) => value.GetHashCode();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2>(T1 value1, T2 value2)
        {
            var hash = Initializer;
            hash = hash * Multiplier + (value1?.GetHashCode() ?? 0);
            hash = hash * Multiplier + (value2?.GetHashCode() ?? 0);
            return hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            var hash = Initializer;
            hash = hash * Multiplier + (value1?.GetHashCode() ?? 0);
            hash = hash * Multiplier + (value2?.GetHashCode() ?? 0);
            hash = hash * Multiplier + (value3?.GetHashCode() ?? 0);
            return hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            var hash = Initializer;
            hash = hash * Multiplier + (value1?.GetHashCode() ?? 0);
            hash = hash * Multiplier + (value2?.GetHashCode() ?? 0);
            hash = hash * Multiplier + (value3?.GetHashCode() ?? 0);
            hash = hash * Multiplier + (value4?.GetHashCode() ?? 0);
            return hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
        {
            var hash = Initializer;
            hash = hash * Multiplier + (value1?.GetHashCode() ?? 0);
            hash = hash * Multiplier + (value2?.GetHashCode() ?? 0);
            hash = hash * Multiplier + (value3?.GetHashCode() ?? 0);
            hash = hash * Multiplier + (value4?.GetHashCode() ?? 0);
            hash = hash * Multiplier + (value5?.GetHashCode() ?? 0);
            return hash;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6, T7>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8)
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
