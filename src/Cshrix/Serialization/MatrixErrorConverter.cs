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

    public class MatrixErrorConverter : JsonConverter<MatrixError>
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, MatrixError value, JsonSerializer serializer) =>
            throw new NotImplementedException();

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
