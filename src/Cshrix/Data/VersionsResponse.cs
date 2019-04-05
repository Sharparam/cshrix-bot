namespace Cshrix.Data
{
    using Newtonsoft.Json;

    public readonly struct VersionsResponse
    {
        [JsonConstructor]
        public VersionsResponse(string[] versions)
            : this() =>
            Versions = versions;

        [JsonProperty("versions")]
        public string[] Versions { get; }
    }
}
