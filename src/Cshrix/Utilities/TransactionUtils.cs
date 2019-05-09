// <copyright file="TransactionUtils.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Utilities
{
    using System;
    using System.Threading;

    /// <summary>
    /// Contains helper methods to work with transaction IDs for the Matrix API.
    /// </summary>
    internal static class TransactionUtils
    {
        /// <summary>
        /// Base identifier that transaction IDs are created with. Computed once on application start.
        /// </summary>
        private static readonly string BaseIdentifier = Guid.NewGuid().ToString("N");

        /// <summary>
        /// Counter value appended to the <see cref="BaseIdentifier" /> to form a transaction ID.
        /// </summary>
        private static long _counter;

        /// <summary>
        /// Generate a new transaction ID.
        /// </summary>
        /// <returns>A new transaction ID.</returns>
        /// <remarks>
        /// The ID uses a GUID that is computed once at application startup (<see cref="BaseIdentifier" />)
        /// and then appends an integer value that is incremented for every new transaction ID obtained.
        /// </remarks>
        internal static string GenerateId()
        {
            var counter = unchecked((ulong)Interlocked.Increment(ref _counter));
            return $"{BaseIdentifier}.{counter}";
        }
    }
}
