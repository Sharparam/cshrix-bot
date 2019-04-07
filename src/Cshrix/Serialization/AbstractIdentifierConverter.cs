namespace Cshrix.Serialization
{
    using System;

    using Data;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public abstract class AbstractIdentifierConverter<T> : JsonConverter<T> where T : IIdentifier
    {
        private readonly Func<string, T> _stringConstructor;

        private readonly Func<string, ServerName, T> _explicitConstructor;

        protected AbstractIdentifierConverter(
            Func<string, T> stringConstructor,
            Func<string, ServerName, T> explicitConstructor)
        {
            _stringConstructor = stringConstructor;
            _explicitConstructor = explicitConstructor;
        }

        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override T ReadJson(
            JsonReader reader,
            Type objectType,
            T existingValue,
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
                    return _stringConstructor((string)value);

                case JTokenType.Object when token is JObject obj:
                    return CreateFromJObject(obj);

                default:
                    throw new JsonSerializationException("Cannot deserialize UserId if it is not a string or object");
            }
        }

        private T CreateFromJObject(JObject obj)
        {
            var hasLocalpart = obj.TryGetValue(nameof(IIdentifier.Localpart), out var localpartToken);

            if (!hasLocalpart)
            {
                throw new JsonSerializationException("Cannot deserialize identifier object that is missing localpart");
            }

            if (localpartToken.Type != JTokenType.String)
            {
                throw new JsonSerializationException(
                    "Cannot deserialize identifier object that has non-string type localpart");
            }

            var localpart = localpartToken.Value<string>();

            var hasDomain = obj.TryGetValue(nameof(IIdentifier.Domain), out var domainToken);

            if (!hasDomain)
            {
                throw new JsonSerializationException("Cannot deserialize identifier object that is missing domain");
            }

            var domain = domainToken.ToObject<ServerName>();

            return _explicitConstructor(localpart, domain);
        }
    }
}
