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

    using Helpers;

    using Newtonsoft.Json;

    public class SecondTimeSpanConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) =>
            objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);

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

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var nullable = ReflectionHelpers.IsNullable(objectType);

            if (reader.TokenType == JsonToken.Null)
            {
                if (!nullable)
                {
                    throw new JsonSerializationException($"Cannot convert null value to {objectType}");
                }

                return null;
            }

            long seconds;

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
