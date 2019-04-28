// <copyright file="StringIpAddressConverter.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Serialization
{
    using System;
    using System.Net;

    using Newtonsoft.Json;

    /// <inheritdoc />
    /// <summary>
    /// Converts <see cref="IPAddress" /> to/from its string representation in JSON.
    /// </summary>
    public class StringIpAddressConverter : JsonConverter<IPAddress>
    {
        /// <inheritdoc />
        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, IPAddress value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value.ToString());
            }
        }

        /// <inheritdoc />
        /// <summary>Reads the JSON representation of the object.</summary>
        /// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">
        /// The existing value of object being read. If there is no existing value then <c>null</c> will be used.
        /// </param>
        /// <param name="hasExistingValue">The existing value has a value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override IPAddress ReadJson(
            JsonReader reader,
            Type objectType,
            IPAddress existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            IPAddress address;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    address = new IPAddress((long)reader.Value);
                    break;

                case JsonToken.String:
                    if (!IPAddress.TryParse((string)reader.Value, out address))
                    {
                        throw new JsonSerializationException("Cannot convert invalid value to IPAddress");
                    }

                    break;

                default:
                    throw new JsonSerializationException(
                        $"Unexpected token parsing IP. Expected Integer or String, got {reader.TokenType}.");
            }

            return address;
        }
    }
}
