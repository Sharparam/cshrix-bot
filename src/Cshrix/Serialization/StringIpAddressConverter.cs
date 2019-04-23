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

    public class StringIpAddressConverter : JsonConverter<IPAddress>
    {
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
