namespace Cshrix
{
    using System.Threading.Tasks;

    using Data;

    using RestEase;

    public interface IMatrixApi
    {
        [Header("User-Agent", nameof(Cshrix))]
        string UserAgent { get; set; }

        [Path("apiVersion")]
        string ApiVersion { get; set; }

        [Header("Authorization")]
        string Authorization { get; set; }

        [Get("versions")]
        Task<VersionsResponse> GetVersionsAsync();

        [Post("{apiVersion}/user_directory/search")]
        Task<UserSearchResult> SearchUsersAsync([Body] UserSearchQuery query);
    }
}
