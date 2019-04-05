namespace Cshrix.Data
{
    using System;

    using Newtonsoft.Json;

    using Serialization;

    [JsonConverter(typeof(UserIdConverter))]
    public readonly struct UserId
    {
        [JsonConstructor]
        public UserId(string id)
            : this()
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "ID string cannot be null");
            }

            id = id.Trim();

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("ID cannot be empty or full of whitespace", nameof(id));
            }

            if (!id.StartsWith("@"))
            {
                throw new ArgumentException("ID must start with '@'", nameof(id));
            }

            if (!id.Contains(":"))
            {
                throw new ArgumentException("ID must contain a ':'", nameof(id));
            }

            Id = id;

            var split = id.Substring(1).Split(':');

            if (split.Length != 2)
            {
                throw new ArgumentException("ID must contain both local-part and server", nameof(id));
            }

            LocalPart = split[0];
            Server = split[1];

            if (string.IsNullOrWhiteSpace(LocalPart))
            {
                throw new ArgumentException("ID cannot contain empty local-part");
            }

            if (string.IsNullOrWhiteSpace(Server))
            {
                throw new ArgumentException("ID must contain a server");
            }
        }

        public string Id { get; }

        public string LocalPart { get; }

        public string Server { get; }

        public static UserId FromString(string id) => new UserId(id);

        public override string ToString() => Id;
    }
}
