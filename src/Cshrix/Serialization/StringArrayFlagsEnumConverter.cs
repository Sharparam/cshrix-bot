// <copyright file="StringArrayFlagsEnumConverter.cs">
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
    using System.Runtime.Serialization;

    using Helpers;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <inheritdoc />
    /// <summary>
    /// Converts a flags enum to/from a JSON array of strings.
    /// </summary>
    /// <typeparam name="T">The type of the enum to convert.</typeparam>
    /// <remarks>
    /// This converter will only consider fields in the enum marked by an <see cref="EnumMemberAttribute" /> attribute.
    /// </remarks>
    public class StringArrayFlagsEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        /// <inheritdoc />
        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            var named = EnumHelpers.GetNamedFlags<T>();

            writer.WriteStartArray();

            foreach (var kvp in named)
            {
                if (value.HasFlag(kvp.Value))
                {
                    writer.WriteValue(kvp.Key);
                }
            }

            writer.WriteEndArray();
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
        public override T ReadJson(
            JsonReader reader,
            Type objectType,
            T existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                throw new JsonSerializationException($"Cannot convert null value to {objectType}");
            }

            var array = JArray.Load(reader);

            if (array.Type != JTokenType.Array)
            {
                throw new JsonSerializationException(
                    $"Unexpected token when parsing enum array. Expected array, got {array.Type}.");
            }

            var values = array.ToObject<HashSet<string>>();
            var named = EnumHelpers.GetNamedFlags<T>();

            var hasNone = Enum.TryParse("None", out T value);

            if (!hasNone)
            {
                throw new JsonSerializationException($"Target flags enum {objectType} must have 'None' value");
            }

            var underlyingType = Enum.GetUnderlyingType(typeof(T));
            dynamic enumInteger = Convert.ChangeType(value, underlyingType);

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var kvp in named)
            {
                // ReSharper disable once InvertIf
                if (values.Contains(kvp.Key))
                {
                    dynamic namedValue = Convert.ChangeType(kvp.Value, underlyingType);
                    enumInteger |= namedValue;
                }
            }

            return (T)enumInteger;
        }
    }
}
