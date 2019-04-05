namespace Cshrix
{
    using System.Threading.Tasks;

    using Data;

    using RestEase;

    public interface IWellKnownApi
    {
        [Header("User-Agent", nameof(Cshrix))]
        string UserAgent { get; set; }

        [Get(".well-known/matrix/client")]
        Task<ClientInfo> GetClientInfoAsync();
    }
}
