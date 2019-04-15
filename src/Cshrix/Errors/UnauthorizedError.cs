// <copyright file="UnauthorizedError.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Errors
{
    using System.Collections.Generic;

    using Data;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public sealed class UnauthorizedError : MatrixError
    {
        public UnauthorizedError(
            [CanBeNull] string code,
            [CanBeNull] string message,
            [CanBeNull] string[] completed,
            AuthenticationFlow[] flows,
            IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> parameters,
            string session)
            : base(code, message)
        {
            Completed = completed;
            Flows = flows;
            Parameters = parameters;
            Session = session;
        }

        [JsonProperty("completed")]
        [CanBeNull]
        public string[] Completed { get; }

        [JsonProperty("flows")]
        public AuthenticationFlow[] Flows { get; }

        [JsonProperty("params")]
        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> Parameters { get; }

        [JsonProperty("session")]
        public string Session { get; }
    }
}
