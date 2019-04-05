namespace Cshrix
{
    using Configuration;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class MatrixClient : IMatrixClient
    {
        private readonly IOptionsMonitor<MatrixClientConfiguration> _configMonitor;

        public MatrixClient(ILogger<MatrixClient> log, IOptionsMonitor<MatrixClientConfiguration> clientConfig)
        {
            _configMonitor = clientConfig;
            Log = log;
        }

        protected ILogger Log { get; }
    }
}
