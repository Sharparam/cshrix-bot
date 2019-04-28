// <copyright file="MatrixApiQueryParamSerializer.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Extensions;

    using Newtonsoft.Json;

    using RestEase;

    /// <inheritdoc />
    /// <summary>
    /// Custom serializer for RestEase that converts query parameters to relevant data for the Matrix API,
    /// depending on their type.
    /// </summary>
    /// <remarks>
    ///  * Instances of <see cref="TimeSpan" /> are turned into their duration in milliseconds.
    ///  * Instances of <see cref="DateTime" /> and <see cref="DateTimeOffset" /> are turned into their UNIX
    ///    timestamp in milliseconds.
    ///  * Other types are serialized using Newtonsoft.Json, stripping any leading and trailing double quotes
    ///    afterwards.
    /// </remarks>
    public sealed class MatrixApiQueryParamSerializer : RequestQueryParamSerializer
    {
        /// <inheritdoc />
        /// <summary>
        /// Serialize a query parameter whose value is scalar (not a collection),
        /// into a collection of <see cref="KeyValuePair{String, String}" />.
        /// </summary>
        /// <typeparam name="T">Type of the value to serialize.</typeparam>
        /// <param name="name">Name of the query parameter.</param>
        /// <param name="value">Value of the query parameter.</param>
        /// <param name="info">Extra info which may be useful to the serializer.</param>
        /// <returns>A collection of <see cref="KeyValuePair{String, String}" /> to use as query parameters.</returns>
        public override IEnumerable<KeyValuePair<string, string>> SerializeQueryParam<T>(
            string name,
            T value,
            RequestQueryParamSerializerInfo info)
        {
            if (value == null)
            {
                yield break;
            }

            var serializedValue = Serialize(value);
            yield return new KeyValuePair<string, string>(name, serializedValue);
        }

        /// <inheritdoc />
        /// <summary>
        /// Serialize a query parameter whose value is a collection,
        /// into a collection of <see cref="KeyValuePair{String, String}" />.
        /// </summary>
        /// <typeparam name="T">Type of the value to serialize.</typeparam>
        /// <param name="name">Name of the query parameter.</param>
        /// <param name="values">Values of the query parameter.</param>
        /// <param name="info">Extra info which may be useful to the serializer.</param>
        /// <returns>A collection of <see cref="KeyValuePair{String, String}" /> to use as query parameters.</returns>
        public override IEnumerable<KeyValuePair<string, string>> SerializeQueryCollectionParam<T>(
            string name,
            IEnumerable<T> values,
            RequestQueryParamSerializerInfo info)
        {
            if (values == null)
            {
                yield break;
            }

            foreach (var value in values)
            {
                if (value == null)
                {
                    continue;
                }

                var serializedValue = Serialize(value);
                yield return new KeyValuePair<string, string>(name, serializedValue);
            }
        }

        /// <summary>
        /// Serializes a value appropriately depending on its type.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <typeparam name="T">The type of the value being serialized.</typeparam>
        /// <returns>An appropriate string representation of <paramref name="value" />.</returns>
        private static string Serialize<T>(T value)
        {
            if (ReferenceEquals(null, value))
            {
                return default;
            }

            switch (value)
            {
                case TimeSpan timeSpan:
                    return SerializeTimeSpan(timeSpan);

                case DateTime dateTime:
                    return SerializeDateTime(dateTime);

                case DateTimeOffset dateTimeOffset:
                    return SerializeDateTimeOffset(dateTimeOffset);

                default:
                    return SerializeToStrippedJson(value);
            }
        }

        /// <summary>
        /// Serializes a <see cref="TimeSpan" /> into its duration in milliseconds.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <returns>The duration of <paramref name="value" /> in milliseconds.</returns>
        private static string SerializeTimeSpan(TimeSpan value) =>
            value.TotalMilliseconds.ToString(CultureInfo.InvariantCulture);

        /// <summary>
        /// Serializes a <see cref="DateTime" /> into its Unix timestamp in milliseconds.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <returns>The Unix timestamp in milliseconds for <paramref name="value" />.</returns>
        private static string SerializeDateTime(DateTime value) =>
            value.ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture);

        /// <summary>
        /// Serializes a <see cref="DateTimeOffset" /> into its Unix timestamp in milliseconds.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <returns>The Unix timestamp in milliseconds for <paramref name="value" />.</returns>
        private static string SerializeDateTimeOffset(DateTimeOffset value) =>
            value.ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture);

        /// <summary>
        /// Serializes a value into JSON using the default Newtonsoft.Json serializer.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <typeparam name="T">The type of <paramref name="value" />.</typeparam>
        /// <returns>The JSON representation of <paramref name="value" />.</returns>
        private static string SerializeToStrippedJson<T>(T value)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            var stripped = StripQuotes(serializedValue);
            return stripped;
        }

        /// <summary>
        /// Strips leading and trailing double quotes (") from <paramref name="value" />.
        /// </summary>
        /// <param name="value">The value to strip quotes from.</param>
        /// <returns><paramref name="value" /> stripped of all leading and trailing double quotes.</returns>
        private static string StripQuotes(string value) => value.TrimStart('"').TrimEnd('"');
    }
}
