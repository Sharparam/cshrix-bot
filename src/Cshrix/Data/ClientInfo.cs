namespace Cshrix.Data
{
    using Newtonsoft.Json;

    public readonly struct ClientInfo
    {
        [JsonConstructor]
        public ClientInfo(BaseUriContainer homeserver, BaseUriContainer identityServer)
            : this()
        {
            Homeserver = homeserver;
            IdentityServer = identityServer;
        }

        [JsonProperty("m.homeserver")]
        public BaseUriContainer Homeserver { get; }

        [JsonProperty("m.identity_server")]
        public BaseUriContainer IdentityServer { get; }
    }
}
