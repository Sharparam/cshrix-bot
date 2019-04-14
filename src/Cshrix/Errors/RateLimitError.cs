namespace Cshrix.Errors
{
    using System;

    using Newtonsoft.Json;

    using Serialization;

    public sealed class RateLimitError : MatrixError
    {
        [JsonConstructor]
        public RateLimitError(TimeSpan retryAfter, string code, string message)
            : base(code, message) =>
            RetryAfter = retryAfter;

        [JsonProperty("retry_after_ms")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan RetryAfter { get; }
    }
}
