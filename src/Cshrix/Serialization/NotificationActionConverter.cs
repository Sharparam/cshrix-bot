// <copyright file="NotificationActionConverter.cs">
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
    using Newtonsoft.Json.Linq;

    public class NotificationActionConverter : JsonConverter<NotificationAction>
    {
        public override void WriteJson(JsonWriter writer, NotificationAction value, JsonSerializer serializer)
        {
            if (value.Name == null && value.Value == null)
            {
                writer.WriteValue(value.Action);
                return;
            }

            writer.WriteStartObject();
            writer.WritePropertyName("set_tweak");
            writer.WriteValue(value.Name);

            if (value.Value != null)
            {
                writer.WritePropertyName("value");
                writer.WriteValue(value.Value);
            }

            writer.WriteEndObject();
        }

        public override NotificationAction ReadJson(
            JsonReader reader,
            Type objectType,
            NotificationAction existingValue,
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
                    return new NotificationAction((string)value);

                case JTokenType.Object when token is JObject obj:
                    return CreateFromJObject(obj, serializer);

                default:
                    throw new JsonSerializationException(
                        $"Unexpected token parsing NotificationAction. Expected String or Object, got {token.Type}.");
            }
        }

        private static NotificationAction CreateFromJObject(JObject obj, JsonSerializer serializer)
        {
            var hasTweak = obj.TryGetValue("set_tweak", out var tweakToken);

            if (!hasTweak)
            {
                throw new JsonSerializationException("Cannot deserialize NotificationAction that is missing set_tweak");
            }

            var name = tweakToken.Value<string>();

            var hasValue = obj.TryGetValue("value", out var valueToken);

            if (!hasValue)
            {
                return new NotificationAction("set_tweak", name);
            }

            var value = serializer.Deserialize(valueToken.CreateReader());

            return new NotificationAction("set_tweak", name, value);
        }
    }
}
