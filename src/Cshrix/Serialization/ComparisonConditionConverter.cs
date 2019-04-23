// <copyright file="ComparisonConditionConverter.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Serialization
{
    using System;

    using Data.Notifications;

    using Newtonsoft.Json;

    public class ComparisonConditionConverter<T> : JsonConverter<ComparisonCondition<T>>
        where T : IEquatable<T>, IComparable<T>
    {
        public override void WriteJson(JsonWriter writer, ComparisonCondition<T> value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override ComparisonCondition<T> ReadJson(
            JsonReader reader,
            Type objectType,
            ComparisonCondition<T> existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                throw new JsonSerializationException($"Cannot deserialize null value to {objectType}");
            }

            if (reader.TokenType != JsonToken.String)
            {
                throw new JsonSerializationException(
                    $"Unexpected token when deserializing ComparisonCondition. Expected String, got {reader.TokenType}");
            }

            var value = (string)reader.Value;
            var operandType = typeof(T);

            string op = null;
            string operandString;

            if (value.StartsWith("=="))
            {
                op = "==";
            }
            else if (value.StartsWith("<"))
            {
                op = "<";
            }
            else if (value.StartsWith(">"))
            {
                op = ">";
            }
            else if (value.StartsWith(">="))
            {
                op = ">=";
            }
            else if (value.StartsWith("<="))
            {
                op = "<=";
            }

            if (op != null)
            {
                operandString = value.Substring(op.Length);
            }
            else
            {
                op = "==";
                operandString = value;
            }

            T operand;

            try
            {
                operand = (T)Convert.ChangeType(operandString, operandType);
            }
            catch (FormatException)
            {
                throw new JsonSerializationException($"Failed to parse '{operandString}' as {operandType}");
            }

            return new ComparisonCondition<T>(operand, op);
        }
    }
}
