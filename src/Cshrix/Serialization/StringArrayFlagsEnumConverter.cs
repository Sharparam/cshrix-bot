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

    using Helpers;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class StringArrayFlagsEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
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

            foreach (var kvp in named)
            {
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
