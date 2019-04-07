namespace Cshrix.Serialization
{
    using Data;

    public sealed class UserIdConverter : AbstractIdentifierConverter<UserId>
    {
        public UserIdConverter()
            : base(
                id => new UserId(id),
                (localpart, domain) => new UserId(localpart, domain))
        {
        }
    }
}
