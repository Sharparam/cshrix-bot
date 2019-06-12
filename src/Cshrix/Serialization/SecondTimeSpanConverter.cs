// <copyright file="SecondTimeSpanConverter.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Serialization
{
    using System;

    using Newtonsoft.Json;

    using Utilities;

    /// <inheritdoc />
    /// <summary>
    /// Converts <see cref="TimeSpan" /> values to/from their second JSON representation.
    /// </summary>
    public class SecondTimeSpanConverter : JsonConverter
    {
        /// <inheritdoc />
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType) =>
            objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);

        /// <inheritdoc />
        /// <summary>Writes the seconds JSON representation of the <see cref="TimeSpan" />.</summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue((long)((TimeSpan)value).TotalSeconds);
            }
        }

        /// <inheritdoc />
        /// <summary>Reads a <see cref="TimeSpan" /> value from seconds represented in JSON.</summary>
        /// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The <see cref="TimeSpan" /> value.</returns>
        /// <exception cref="JsonSerializationException">
        /// Thrown if the JSON being deserialized is not valid.
        /// </exception>
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var nullable = ReflectionUtils.IsNullable(objectType);

            if (reader.TokenType == JsonToken.Null)
            {
                if (!nullable)
                {
                    throw new JsonSerializationException($"Cannot convert null value to {objectType}");
                }

                return null;
            }

            long seconds;

            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (reader.TokenType == JsonToken.Integer)
            {
                seconds = (long)reader.Value;
            }
            else if (reader.TokenType == JsonToken.String)
            {
                if (!long.TryParse((string)reader.Value, out seconds))
                {
                    throw new JsonSerializationException($"Cannot convert invalid value to {objectType}");
                }
            }
            else
            {
                throw new JsonSerializationException(
                    $"Unexpected token parsing timespan. Expected Integer or String, got {reader.TokenType}");
            }

            return TimeSpan.FromSeconds(seconds);
        }
    }
}
