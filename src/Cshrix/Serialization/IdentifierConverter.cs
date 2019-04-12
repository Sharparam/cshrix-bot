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

    public sealed class IdentifierConverter : JsonConverter<Identifier>
    {
        public override void WriteJson(JsonWriter writer, Identifier value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override Identifier ReadJson(
            JsonReader reader,
            Type objectType,
            Identifier existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                return new Identifier((string)reader.Value);
            }

            throw new JsonSerializationException("Cannot deserialize identifier if it is not a string");
        }
    }
}
