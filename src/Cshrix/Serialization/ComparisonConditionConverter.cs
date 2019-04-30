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

    /// <inheritdoc />
    /// <summary>
    /// Converts typed <see cref="ComparisonCondition{T}" /> instances to/from their JSON representation.
    /// </summary>
    /// <typeparam name="T">The type contained in the <see cref="ComparisonCondition{T}" />.</typeparam>
    public class ComparisonConditionConverter<T> : JsonConverter<ComparisonCondition<T>>
        where T : IEquatable<T>, IComparable<T>
    {
        /// <inheritdoc />
        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, ComparisonCondition<T> value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
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
        /// <returns>The deserialized <see cref="ComparisonCondition{T}" />.</returns>
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

            string operandString;

            var op = ExtractOperator(value);

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

        /// <summary>
        /// Extracts a comparison operator from an input string, if any is present.
        /// </summary>
        /// <param name="value">The string to parse.</param>
        /// <returns>A string containing the operand if one was found; otherwise, <c>null</c>.</returns>
        private static string ExtractOperator(string value)
        {
            if (value.StartsWith("=="))
            {
                return "==";
            }

            if (value.StartsWith("<"))
            {
                return "<";
            }

            if (value.StartsWith(">"))
            {
                return ">";
            }

            if (value.StartsWith(">="))
            {
                return ">=";
            }

            return value.StartsWith("<=") ? "<=" : null;
        }
    }
}
