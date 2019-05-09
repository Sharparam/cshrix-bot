// <copyright file="DateTimeExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Extensions
{
    using System;

    using Utilities;

    /// <summary>
    /// Contains extension methods for the <see cref="DateTime" /> structure.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns the number of milliseconds that have elapsed since 1970-01-01T00:00:00Z.
        /// </summary>
        /// <param name="dateTime">The point in time to measure to.</param>
        /// <returns>The number of milliseconds that have elapsed since 1970-01-01T00:00:00Z.</returns>
        /// <seealso cref="DateTimeUtils.FromUnixTimeMilliseconds" />
        public static long ToUnixTimeMilliseconds(this DateTime dateTime) =>
            (long)(dateTime.ToUniversalTime() - DateTimeUtils.UnixEpoch).TotalMilliseconds;
    }
}
