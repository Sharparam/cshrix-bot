// <copyright file="MatrixErrorConverter.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Serialization
{
    using System;
    using System.Collections.Generic;

    using Data.Authentication;

    using Errors;

    using Extensions;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <inheritdoc />
    /// <summary>
    /// Serializes and deserializes <see cref="MatrixError" /> objects.
    /// </summary>
    public class MatrixErrorConverter : JsonConverter<MatrixError>
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether this <see cref="JsonConverter" /> can write JSON.
        /// </summary>
        /// <value><c>true</c> if this <see cref="JsonConverter" /> can write JSON; otherwise, <c>false</c>.</value>
        public override bool CanWrite => false;

        /// <inheritdoc />
        /// <summary>
        /// Throws a <see cref="NotImplementedException" />, due to
        /// serialization being delegated to the default behaviour.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, MatrixError value, JsonSerializer serializer) =>
            throw new NotSupportedException("Writing is handled by the default JSON.NET behaviour");

        /// <inheritdoc />
        /// <summary>Reads the JSON representation of a <see cref="MatrixError" />.</summary>
        /// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">
        /// The existing value of object being read. If there is no existing value then <c>null</c> will be used.
        /// </param>
        /// <param name="hasExistingValue">The existing value has a value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>An instance of a relevant <see cref="MatrixError" /> type.</returns>
        public override MatrixError ReadJson(
            JsonReader reader,
            Type objectType,
            MatrixError existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var obj = JObject.Load(reader);

            if (obj.Type != JTokenType.Object)
            {
                throw new JsonSerializationException(
                    $"Unexpected token when deserializing MatrixError. Expected Object, got {obj.Type}.");
            }

            var code = obj.ValueOrDefault<string>("errcode");
            var message = obj.ValueOrDefault<string>("error");
            var completed = obj.ObjectOrDefault<string[]>("completed");
            var hasRateLimit = obj.TryGetValue<long>("retry_after_ms", out var retryAfterMs);
            var hasFlows = obj.TryGetObject<AuthenticationFlow[]>("flows", out var flows);
            var hasParameters =
                obj.TryGetObject<IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>>>("params", out var parameters);

            var hasSession = obj.TryGetValue<string>("session", out var session);

            if (hasFlows && hasParameters && hasSession)
            {
                return new UnauthorizedError(code, message, completed, flows, parameters, session);
            }

            if (hasRateLimit)
            {
                return new RateLimitError(code, message, TimeSpan.FromMilliseconds(retryAfterMs));
            }

            return new MatrixError(code, message);
        }
    }
}
