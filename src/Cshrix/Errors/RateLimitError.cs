namespace Cshrix.Errors
{
    using System;

    using Newtonsoft.Json;

    using Serialization;

    /// <inheritdoc />
    /// <summary>
    /// Represents a rate limit error from the Matrix API.
    /// </summary>
    public sealed class RateLimitError : MatrixError
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Cshrix.Errors.RateLimitError" /> class.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">A description of the error.</param>
        /// <param name="retryAfter">The amount of time to wait before retrying the API call.</param>
        [JsonConstructor]
        public RateLimitError(string code, string message, TimeSpan retryAfter)
            : base(code, message) =>
            RetryAfter = retryAfter;

        /// <summary>
        /// Gets the amount of time to wait before retrying the API call.
        /// </summary>
        [JsonProperty("retry_after_ms")]
        [JsonConverter(typeof(MillisecondTimeSpanConverter))]
        public TimeSpan RetryAfter { get; }
    }
}
