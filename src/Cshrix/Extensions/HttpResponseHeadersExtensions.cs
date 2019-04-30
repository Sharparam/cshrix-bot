// <copyright file="HttpResponseHeadersExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Extensions
{
    using System;
    using System.Linq;
    using System.Net.Http.Headers;

    using JetBrains.Annotations;

    /// <summary>
    /// Contains extension methods for the <see cref="HttpResponseHeaders" /> class.
    /// </summary>
    public static class HttpResponseHeadersExtensions
    {
        /// <summary>
        /// Attempts to get the first instance of a Content-Disposition header from a collection of headers.
        /// </summary>
        /// <param name="headers">The headers to read from.</param>
        /// <param name="value">
        /// When this method returns, contains the first value associated with a Content-Disposition header, if the key
        /// is found; otherwise, the default value for <see cref="String" />.
        /// </param>
        /// <returns><c>true</c> if a Content-Disposition header was found; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="headers" /> is <c>null</c>.
        /// </exception>
        public static bool TryGetFirstContentDisposition([NotNull] this HttpResponseHeaders headers, out string value)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            return headers.TryGetFirstHeaderValue("Content-Disposition", out value);
        }

        /// <summary>
        /// Attempts to get the first instance of a Content-Type header from a collection of headers.
        /// </summary>
        /// <param name="headers">The headers to read from.</param>
        /// <param name="value">
        /// When this method returns, contains the first value associated with a Content-Type header, if the key
        /// is found; otherwise, the default value for <see cref="String" />.
        /// </param>
        /// <returns><c>true</c> if a Content-Type header was found; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="headers" /> is <c>null</c>.
        /// </exception>
        public static bool TryGetFirstContentType([NotNull] this HttpResponseHeaders headers, out string value)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            return headers.TryGetFirstHeaderValue("Content-Type", out value);
        }

        /// <summary>
        /// Attempts to get the first header value belonging to the specified key.
        /// </summary>
        /// <param name="headers">The headers to read from.</param>
        /// <param name="key">The name of the header to retrieve.</param>
        /// <param name="value">
        /// When this method returns, contains the value associated with the specified key, if the key is found;
        /// otherwise, the default value for <see cref="String" />.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns><c>true</c> if the header was found; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="headers" /> and/or <paramref name="key" /> is <c>null</c>.
        /// </exception>
        public static bool TryGetFirstHeaderValue(
            [NotNull] this HttpResponseHeaders headers,
            [NotNull] string key,
            out string value)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var hasValues = headers.TryGetValues(key, out var values);

            if (hasValues)
            {
                value = values.FirstOrDefault();
                return value != default;
            }

            value = default;
            return false;
        }
    }
}
