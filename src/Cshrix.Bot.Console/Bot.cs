namespace Cshrix.Bot.Console
{
    using Microsoft.Extensions.Logging;

    internal sealed class Bot
    {
        private readonly ILogger _log;

        public Bot(ILogger<Bot> log) => _log = log;
    }
}
