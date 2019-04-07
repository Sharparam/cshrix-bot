namespace Cshrix.Serialization
{
    using System;

    using Data;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ServerNameConverter : JsonConverter<ServerName>
    {
        public override ServerName ReadJson(
            JsonReader reader,
            Type objectType,
            ServerName existingValue,
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
                    return new ServerName((string)value);

                case JTokenType.Object when token is JObject obj:
                    return CreateFromJObject(obj);

                default:
                    throw new JsonSerializationException(
                        "Cannot deserialize ServerName if it is not a string or object");
            }
        }

        public override void WriteJson(JsonWriter writer, ServerName value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        private static ServerName CreateFromJObject(JObject obj)
        {
            var hasHostname = obj.TryGetValue(nameof(ServerName.Hostname), out var hostnameToken);

            if (!hasHostname)
            {
                throw new JsonSerializationException("Cannot deserialize server name that is missing hostname");
            }

            if (hostnameToken.Type != JTokenType.String)
            {
                throw new JsonSerializationException(
                    "Cannot deserialize server name that has non-string type hostname");
            }

            var hostname = hostnameToken.Value<string>();

            var hasPort = obj.TryGetValue(nameof(ServerName.Port), out var portToken);

            if (!hasPort)
            {
                return new ServerName(hostname, null);
            }

            if (portToken.Type != JTokenType.Integer)
            {
                throw new JsonSerializationException("Cannot deserialize server name that has non-integer type port");
            }

            var port = portToken.Value<ushort>();

            return new ServerName(hostname, port);
        }
    }
}
