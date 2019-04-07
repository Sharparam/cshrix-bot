namespace Cshrix.Data
{
    using Newtonsoft.Json;

    using Serialization;

    [JsonConverter(typeof(IdentifierConverter))]
    public interface IIdentifier
    {
        IdentifierType Type { get; }

        char Sigil { get; }

        string Localpart { get; }

        ServerName Domain { get; }
    }
}
