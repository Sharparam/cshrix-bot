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

    using Helpers;

    using Newtonsoft.Json;

    public sealed class IdentifierConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) =>
            objectType == typeof(Identifier) || objectType == typeof(Identifier?);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
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

            if (reader.TokenType == JsonToken.String)
            {
                return new Identifier((string)reader.Value);
            }

            throw new JsonSerializationException("Cannot deserialize identifier if it is not a string");
        }
    }
}
