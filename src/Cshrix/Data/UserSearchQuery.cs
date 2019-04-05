namespace Cshrix.Data
{
    using Newtonsoft.Json;

    public readonly struct UserSearchQuery
    {
        [JsonConstructor]
        public UserSearchQuery(string searchTerm, int limit = 10)
            : this()
        {
            SearchTerm = searchTerm;
            Limit = limit;
        }

        [JsonProperty("limit")]
        public int Limit { get; }

        [JsonProperty("search_term")]
        public string SearchTerm { get; }
    }
}
