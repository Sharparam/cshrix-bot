// <copyright file="IdentifierConverter.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Serialization
{
    using System;

    using Data;

    using Newtonsoft.Json;

    /// <inheritdoc />
    /// <summary>
    /// An abstract converter for converting <see cref="Identifier" /> objects.
    /// </summary>
    /// <typeparam name="T">The type of identifier this converter converts.</typeparam>
    public abstract class IdentifierConverter<T> : JsonConverter<T> where T : Identifier
    {
        /// <inheritdoc />
        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
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
        /// <returns>The deserialized object of type <typeparamref name="T" />.</returns>
        /// <exception cref="JsonSerializationException">Thrown if deserialization fails.</exception>
        public override T ReadJson(
            JsonReader reader,
            Type objectType,
            T existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    return null;

                case JsonToken.String:
                    return Parse((string)reader.Value);

                default:
                    throw new JsonSerializationException("Cannot deserialize identifier if it is not a string");
            }
        }

        /// <summary>
        /// Parses a string ID into an <see cref="Identifier" /> of type <typeparamref name="T" />.
        /// </summary>
        /// <param name="id">The ID to parse.</param>
        /// <returns>An instance of <typeparamref name="T"/>.</returns>
        protected abstract T Parse(string id);
    }
}
