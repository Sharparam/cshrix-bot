namespace Cshrix.Data
{
    using System;

    using Newtonsoft.Json;

    public readonly struct BaseUriContainer
    {
        [JsonConstructor]
        public BaseUriContainer(Uri baseUri)
            : this()
        {
            BaseUri = baseUri;
        }

        [JsonProperty("base_url")]
        public Uri BaseUri { get; }
    }
}
