namespace Cshrix.Data
{
    using System;

    using Newtonsoft.Json;

    public readonly struct User
    {
        [JsonConstructor]
        public User(Uri avatarUri, string displayName, UserId userId)
            : this()
        {
            AvatarUri = avatarUri;
            DisplayName = displayName;
            UserId = userId;
        }

        [JsonProperty("avatar_url")]
        public Uri AvatarUri { get; }

        [JsonProperty("display_name")]
        public string DisplayName { get; }

        [JsonProperty("user_id")]
        public UserId UserId { get; }
    }
}
