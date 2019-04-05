namespace Cshrix.Serialization
{
    using System;

    using Data;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public sealed class UserIdConverter : JsonConverter<UserId>
    {
        public override UserId ReadJson(
            JsonReader reader,
            Type objectType,
            UserId existingValue,
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
                    return new UserId((string)value);

                case JTokenType.Object when token is JObject obj:
                    return new UserId((string)obj.Property(nameof(UserId.Id)).Value);

                default:
                    throw new JsonSerializationException("Cannot deserialize UserId if it is not a string or object");
            }
        }

        public override void WriteJson(JsonWriter writer, UserId value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
