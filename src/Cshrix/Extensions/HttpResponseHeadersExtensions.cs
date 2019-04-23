// <copyright file="HttpResponseHeadersExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Extensions
{
    using System.Linq;
    using System.Net.Http.Headers;

    public static class HttpResponseHeadersExtensions
    {
        public static bool TryGetFirstContentDisposition(this HttpResponseHeaders headers, out string value) =>
            headers.TryGetFirstHeaderValue("Content-Disposition", out value);

        public static bool TryGetFirstContentType(this HttpResponseHeaders headers, out string value) =>
            headers.TryGetFirstHeaderValue("Content-Type", out value);

        public static bool TryGetFirstHeaderValue(this HttpResponseHeaders headers, string key, out string value)
        {
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
