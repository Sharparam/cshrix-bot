// <copyright file="RandomHelpers.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Utilities
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// Contains helper methods for working with and generating random numbers and data.
    /// </summary>
    internal static class RandomUtils
    {
        /// <summary>
        /// Global <see cref="Random" /> instance.
        /// </summary>
        internal static readonly Random Rng = new Random();

        /// <summary>
        /// Gets an array of random bytes, generated securely.
        /// </summary>
        /// <param name="count">Number of bytes to generate.</param>
        /// <returns>An array containing <paramref name="count" /> securely generated random bytes.</returns>
        internal static byte[] SecureBytes(int count)
        {
            using (var csprng = new RNGCryptoServiceProvider())
            {
                var data = new byte[count];
                csprng.GetBytes(data);
                return data;
            }
        }
    }
}
