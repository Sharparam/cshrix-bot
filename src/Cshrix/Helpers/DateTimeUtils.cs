// <copyright file="DateTimeUtils.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Helpers
{
    using System;

    public static class DateTimeUtils
    {
        /// <summary>
        /// A <see cref="DateTime" /> set to the UNIX epoch (1970-01-01T00:00:00Z).
        /// </summary>
        internal static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts a Unix time expressed as the number of milliseconds that have elapsed since 1970-01-01T00:00:00Z
        /// to a <see cref="DateTime" /> value.
        /// </summary>
        /// <param name="timestamp">
        /// A Unix time, expressed as the number of milliseconds that have elapsed since 1970-01-01T00:00:00Z.
        /// </param>
        /// <returns>A date and time value that represents the same moment in time as the Unix time.</returns>
        public static DateTime FromUnixTimeMilliseconds(long timestamp) => UnixEpoch.AddMilliseconds(timestamp);
    }
}
