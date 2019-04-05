namespace Cshrix.Extensions
{
    public static class MatrixApiExtensions
    {
        public static void SetBearerToken(this IMatrixApi api, string accessToken) =>
            api.Authorization = $"Bearer {accessToken}";
    }
}
