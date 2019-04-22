namespace Cshrix.Errors
{
    using System;

    using Newtonsoft.Json;

    using Serialization;

    public sealed class RateLimitError : MatrixError
    {
        [JsonConstructor]
        public RateLimitError(string code, string message, TimeSpan retryAfter)
            : base(code, message) =>
            RetryAfter = retryAfter;

        [JsonProperty("retry_after_ms")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan RetryAfter { get; }
    }
}
