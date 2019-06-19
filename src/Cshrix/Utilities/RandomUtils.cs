// <copyright file="RandomUtils.cs">
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
        /// A factory to generate an instance of <see cref="RNGCryptoServiceProvider" />.
        /// </summary>
        /// <remarks>
        /// Generating a default instance with the default constructor and keeping it around without disposing it
        /// is fine as described in https://stackoverflow.com/a/28073796/1104531.
        /// </remarks>
        private static readonly Lazy<RNGCryptoServiceProvider> Csprng =
            new Lazy<RNGCryptoServiceProvider>(() => new RNGCryptoServiceProvider());

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
            var data = new byte[count];
            SecureBytes(data);
            return data;
        }

        /// <summary>
        /// Fills a byte array with random bytes, generated securely.
        /// </summary>
        /// <param name="buffer">The array to write random bytes into.</param>
        internal static void SecureBytes(byte[] buffer)
        {
            Csprng.Value.GetBytes(buffer);
        }
    }
}
