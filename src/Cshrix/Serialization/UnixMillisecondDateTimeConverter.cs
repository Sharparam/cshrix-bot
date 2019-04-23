// Modifications copyright (c) 2019 by Adam Hellberg.
//
// This file is heavily based on the seconds-timestamp converter from
// Newtonsoft.Json available here:
// https://github.com/JamesNK/Newtonsoft.Json/blob/aa4062d/Src/Newtonsoft.Json/Converters/UnixDateTimeConverter.cs
//
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

namespace Cshrix.Serialization
{
    using System;

    using Helpers;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class UnixMillisecondDateTimeConverter : DateTimeConverterBase
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long milliseconds;

            switch (value)
            {
                case DateTime dateTime:
                    milliseconds = (long)(dateTime.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
                    break;

                case DateTimeOffset dateTimeOffset:
                    milliseconds = dateTimeOffset.ToUnixTimeMilliseconds();
                    break;

                default:
                    throw new JsonSerializationException("Expected date object value");
            }

            if (milliseconds < 0)
            {
                throw new JsonSerializationException(
                    "Cannot convert date value that is before Unix epoch of 00:00:00 UTC on 1 January 1970.");
            }

            writer.WriteValue(milliseconds);
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

            long milliseconds;

            if (reader.TokenType == JsonToken.Integer)
            {
                milliseconds = (long)reader.Value;
            }
            else if (reader.TokenType == JsonToken.String)
            {
                if (!long.TryParse((string)reader.Value, out milliseconds))
                {
                    throw new JsonSerializationException($"Cannot convert invalid value to {objectType}");
                }
            }
            else
            {
                throw new JsonSerializationException(
                    $"Unexpected token parsing date. Expected Integer or String, got {reader.TokenType}");
            }

            if (milliseconds < 0)
            {
                throw new JsonSerializationException(
                    "Cannot convert value that is before Unix epoch of 00:00:00 UTC on 1 January.");
            }

            var type = nullable ? Nullable.GetUnderlyingType(objectType) : objectType;

            return type == typeof(DateTimeOffset)
                ? DateTimeOffset.FromUnixTimeMilliseconds(milliseconds)
                : UnixEpoch.AddMilliseconds(milliseconds);
        }
    }
}
