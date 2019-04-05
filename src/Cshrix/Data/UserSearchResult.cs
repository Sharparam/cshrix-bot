namespace Cshrix.Data
{
    using Newtonsoft.Json;

    public readonly struct UserSearchResult
    {
        [JsonConstructor]
        public UserSearchResult(bool limited, User[] results)
            : this()
        {
            Limited = limited;
            Results = results;
        }

        [JsonProperty("limited")]
        public bool Limited { get; }

        [JsonProperty("results")]
        public User[] Results { get; }
    }
}
