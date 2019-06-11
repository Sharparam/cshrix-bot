// <copyright file="Base64Utils.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using JetBrains.Annotations;

    /// <summary>
    /// Contains utilities for working with Base64 encoding.
    /// </summary>
    internal static class Base64Utils
    {
        /// <summary>
        /// The default encoding.
        /// </summary>
        private static readonly Encoding DefaultEncoding = Encoding.UTF8;

        /// <summary>
        /// Contains chars used for padding in Base64.
        /// </summary>
        private static readonly char[] Padding =
        {
            '='
        };

        /// <summary>
        /// Maps Base64 characters to a URL-safe replacement.
        /// </summary>
        /// <remarks>
        /// Refer to <a href="https://tools.ietf.org/html/rfc4648#section-5">RFC4648</a> for reference.
        /// </remarks>
        private static readonly IReadOnlyDictionary<char, char> UrlSafeEncoders = new Dictionary<char, char>
        {
            ['+'] = '-',
            ['/'] = '_'
        };

        /// <summary>
        /// A reverse dictionary made from <see cref="UrlSafeEncoders" />, used for decoding URL-safe Base64.
        /// </summary>
        private static readonly IReadOnlyDictionary<char, char> UrlSafeDecoders =
            UrlSafeEncoders.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        /// <summary>
        /// Converts an input string to a Base64 string.
        /// </summary>
        /// <param name="input">The input string to convert.</param>
        /// <param name="unpad"><c>true</c> to produce unpadded Base64; otherwise, <c>false</c>.</param>
        /// <param name="urlSafe"><c>true</c> to generate URL-safe Base64; otherwise, <c>false</c>.</param>
        /// <param name="encoding">The encoding to use. If <c>null</c>, will use UTF8.</param>
        /// <returns>The Base64 string.</returns>
        internal static string ToBase64(
            string input,
            bool unpad = false,
            bool urlSafe = false,
            Encoding encoding = null) =>
            ToBase64((encoding ?? DefaultEncoding).GetBytes(input), unpad, urlSafe);

        /// <summary>
        /// Converts an array of bytes to a Base64 string.
        /// </summary>
        /// <param name="input">The bytes to convert.</param>
        /// <param name="unpad"><c>true</c> to produce unpadded Base64; otherwise, <c>false</c>.</param>
        /// <param name="urlSafe"><c>true</c> to generate URL-safe Base64; otherwise, <c>false</c>.</param>
        /// <returns>The Base64 string.</returns>
        internal static string ToBase64(byte[] input, bool unpad = false, bool urlSafe = false)
        {
            var result = Convert.ToBase64String(input);

            if (unpad)
            {
                result = TrimPadding(result);
            }

            if (urlSafe)
            {
                result = UrlSafeEncode(result);
            }

            return result;
        }

        /// <summary>
        /// Decodes a Base64 string to an array of bytes.
        /// </summary>
        /// <param name="input">The Base64 string to decode.</param>
        /// <returns>The decoded bytes.</returns>
        internal static byte[] FromBase64(string input)
        {
            var decoded = UrlSafeDecode(input);
            return Convert.FromBase64String(decoded);
        }

        /// <summary>
        /// Decodes a Base64 string to a regular string.
        /// </summary>
        /// <param name="input">The Base64 string to decode.</param>
        /// <param name="encoding">Encoding to use when converting to string. If <c>null</c>, will use UTF8.</param>
        /// <returns>The decoded string.</returns>
        internal static string FromBase64(string input, [CanBeNull] Encoding encoding)
        {
            var bytes = FromBase64(input);
            return (encoding ?? DefaultEncoding).GetString(bytes);
        }

        /// <summary>
        /// Removes trailing padding characters from the input string.
        /// </summary>
        /// <param name="input">The string to unpad.</param>
        /// <returns>The unpadded string.</returns>
        private static string TrimPadding(string input) => input.TrimEnd(Padding);

        /// <summary>
        /// Adds any necessary padding to the (possibly unpadded) input Base64 string.
        /// </summary>
        /// <param name="input">
        /// The string to add padding to (or do nothing if necessary padding already exists).
        /// </param>
        /// <returns>The padded Base64 string.</returns>
        private static string AddPadding(string input)
        {
            switch (input.Length % 4)
            {
                case 2:
                    return $"{input}==";

                case 3:
                    return $"{input}=";

                default:
                    return input;
            }
        }

        /// <summary>
        /// Encodes a Base64 string to be URL-safe.
        /// </summary>
        /// <param name="input">The Base64 string to encode.</param>
        /// <returns>A URL-safe Base64 string.</returns>
        private static string UrlSafeEncode(string input)
        {
            return UrlSafeEncoders.Aggregate(input, (current, kvp) => current.Replace(kvp.Key, kvp.Value));
        }

        /// <summary>
        /// Decodes a Base64 string to replace URL-safe characters with regular ones.
        /// </summary>
        /// <param name="input">The (potentially URL-safe) Base64 string to decode.</param>
        /// <returns>A regular Base64 string.</returns>
        private static string UrlSafeDecode(string input)
        {
            var result = UrlSafeDecoders.Aggregate(input, (current, kvp) => current.Replace(kvp.Key, kvp.Value));
            return AddPadding(result);
        }
    }
}
