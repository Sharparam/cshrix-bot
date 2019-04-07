namespace Cshrix.Serialization
{
    using System;
    using System.Collections.Generic;

    using Data;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public sealed class IdentifierConverter : JsonConverter<IIdentifier>
    {
        private static readonly Dictionary<char, Type> ConverterMapping = new Dictionary<char, Type>
        {
            ['@'] = typeof(UserIdConverter)
        };

        private static readonly Dictionary<char, Type> TypeMapping = new Dictionary<char, Type>
        {
            ['@'] = typeof(UserId)
        };

        public override void WriteJson(JsonWriter writer, IIdentifier value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override IIdentifier ReadJson(
            JsonReader reader,
            Type objectType,
            IIdentifier existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return default;
            }

            var token = JToken.Load(reader);

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (token.Type)
            {
                case JTokenType.String when token is JValue value:
                    return CreateFromJValue(value, reader, serializer);

                default:
                    throw new JsonSerializationException("Cannot deserialize identifier if it is not a string");
            }
        }

        private static IIdentifier CreateFromJValue(JValue value, JsonReader reader, JsonSerializer serializer)
        {
            var stringValue = (string)value;

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                throw new JsonSerializationException("Cannot deserialize identifier from empty string");
            }

            var sigil = stringValue[0];

            var hasConverter = ConverterMapping.TryGetValue(sigil, out var converterType);

            if (!hasConverter)
            {
                throw new JsonSerializationException($"No converter found for sigil '{sigil}'");
            }

            var converter = (JsonConverter)Activator.CreateInstance(converterType);

            if (!converter.CanRead)
            {
                throw new JsonSerializationException($"{converter.GetType()} cannot read");
            }

            var hasTargetType = TypeMapping.TryGetValue(sigil, out var targetType);

            if (!hasTargetType)
            {
                throw new JsonSerializationException($"No target type defined for sigil '{sigil}'");
            }

            if (!converter.CanConvert(targetType))
            {
                throw new JsonSerializationException($"Converter cannot convert {targetType}");
            }

            var deserialized = (IIdentifier)converter.ReadJson(reader, targetType, null, serializer);

            return deserialized;
        }
    }
}
