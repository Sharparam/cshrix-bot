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

    using Data.Notifications;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <inheritdoc />
    /// <summary>
    /// Converts <see cref="NotificationAction" /> objects to/from their JSON representation.
    /// </summary>
    /// <remarks>
    /// The Matrix API can send/receive notification actions either as a string (for simple actions),
    /// or as an object specifying additional parameters. This custom serializer makes it possible to represent
    /// both as complete objects on the C# side, while the API can send and receive what it expects.
    /// </remarks>
    public class NotificationActionConverter : JsonConverter<NotificationAction>
    {
        /// <inheritdoc />
        /// <summary>Writes the JSON representation of an instance of <see cref="NotificationAction" />.</summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <remarks>
        /// <para>
        /// If <paramref name="value" /> only has <see cref="NotificationAction.Action" /> specified, then the
        /// serializer will product a simple string value, as expected by the API.
        /// </para>
        /// <para>
        /// If, on the other hand, the <paramref name="value" /> has one or both of
        /// <see cref="NotificationAction.Name" /> and/or <see cref="NotificationAction.Value" /> specified,
        /// then it will serialize into a complete object representation, with <c>set_tweak</c> set to the value
        /// of <see cref="NotificationAction.Name" />.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// var action = new NotificationAction("notify");
        /// var json = JsonConvert.SerializeObject(action);
        /// // Value of json: "notify"
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// var action = new NotificationAction("set_tweak", "highlight", true);
        /// var json = JsonConvert.SerializeObject(action);
        /// // Value of json: { "set_tweak": "highlight", "value": true }
        /// </code>
        /// </example>
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

        /// <inheritdoc />
        /// <summary>Reads the JSON representation of a <see cref="NotificationAction" />.</summary>
        /// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">
        /// The existing value of object being read. If there is no existing value then <c>null</c> will be used.
        /// </param>
        /// <param name="hasExistingValue">The existing value has a value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The <see cref="NotificationAction" /> value.</returns>
        /// <remarks>
        /// <para>
        /// If the JSON value is a simple string, it gets deserialized into a <see cref="NotificationAction" />
        /// with only <see cref="NotificationAction.Action" /> set.
        /// </para>
        /// <para>
        /// If, on the other hand, the JSON value is an object, the deserializer will attempt to extract relevant
        /// values from it, as specified by the API.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// var json = "\"notify\"";
        /// var action = JsonConvert.DeserializeObject&lt;NotificationAction&gt;(json);
        /// // action has its Action property set to "notify"
        /// </code>
        /// </example>
        /// <example>
        /// <code>
        /// var json = "{ \"set_tweak\": \"highlight\", \"value\": false }";
        /// var action = JsonConvert.DeserializeObject&lt;NotificationAction&gt;(json);
        /// // action has the following properties:
        /// //   Action = "set_tweak"
        /// //   Name = "highlight"
        /// //   Value = false
        /// </code>
        /// </example>
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

            // ReSharper disable once SwitchStatementMissingSomeCases
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

        /// <summary>
        /// Attempts to read a <see cref="NotificationAction" /> object from a <see cref="JObject" />.
        /// </summary>
        /// <param name="obj">The <see cref="JObject" /> to read properties from.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>An instance of <see cref="NotificationAction" />.</returns>
        /// <exception cref="JsonSerializationException">
        /// Thrown if the provided object doesn't contain the required properties.
        /// </exception>
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
