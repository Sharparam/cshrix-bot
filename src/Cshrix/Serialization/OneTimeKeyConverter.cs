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

    using Data;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class OneTimeKeyConverter : JsonConverter<OneTimeKey>
    {
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
