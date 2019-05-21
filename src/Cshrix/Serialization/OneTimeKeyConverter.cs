// <copyright file="OneTimeKeyConverter.cs">
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

    using Cryptography.Data;

    using Data;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <inheritdoc />
    /// <summary>
    /// Converts <see cref="OneTimeKey" /> objects to/from their JSON representation.
    /// </summary>
    public class OneTimeKeyConverter : JsonConverter<OneTimeKey>
    {
        /// <inheritdoc />
        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, OneTimeKey value, JsonSerializer serializer)
        {
            if (value.Signatures == null)
            {
                writer.WriteValue(value.Key);
            }
            else
            {
                writer.WriteStartObject();
                writer.WritePropertyName("key");
                writer.WriteValue(value.Key);
                writer.WritePropertyName("signatures");
                serializer.Serialize(writer, value.Signatures);
                writer.WriteEndObject();
            }
        }

        /// <inheritdoc />
        /// <summary>Reads the JSON representation of the object.</summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">
        /// The existing value of object being read. If there is no existing value then <c>null</c> will be used.
        /// </param>
        /// <param name="hasExistingValue">The existing value has a value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override OneTimeKey ReadJson(
            JsonReader reader,
            Type objectType,
            OneTimeKey existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                throw new JsonSerializationException($"Cannot convert null value to {objectType}");
            }

            var token = JToken.Load(reader);

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (token.Type)
            {
                case JTokenType.String when token is JValue value:
                    return new OneTimeKey((string)value);

                case JTokenType.Object when token is JObject obj:
                    return CreateFromJObject(obj);

                default:
                    throw new JsonSerializationException(
                        $"Unexpected token parsing OneTimeKey. Expected string or object, got {token.Type}");
            }
        }

        /// <summary>
        /// Attempts to create a <see cref="OneTimeKey" /> from an instance of <see cref="JObject" />.
        /// </summary>
        /// <param name="obj">The <see cref="JObject" /> to read properties from.</param>
        /// <returns>An instance of <see cref="OneTimeKey" />.</returns>
        /// <exception cref="JsonSerializationException">
        /// Thrown if the JSON data does not contain the expected properties.
        /// </exception>
        private static OneTimeKey CreateFromJObject(JObject obj)
        {
            var hasKey = obj.TryGetValue("key", out var keyToken);

            if (!hasKey)
            {
                throw new JsonSerializationException("Cannot deserialize OneTimeKey that is missing key");
            }

            if (keyToken.Type != JTokenType.String)
            {
                throw new JsonSerializationException(
                    $"Expected string type for key in OneTimeKey, got {keyToken.Type}");
            }

            var key = keyToken.Value<string>();

            var hasSignatures = obj.TryGetValue("signatures", out var signaturesToken);

            if (!hasSignatures)
            {
                throw new JsonSerializationException("Cannot deserialize OneTimeKey that is missing signatures");
            }

            var signatures = signaturesToken
                .ToObject<IReadOnlyDictionary<UserId, IReadOnlyDictionary<string, string>>>();

            return new OneTimeKey(key, signatures);
        }
    }
}
