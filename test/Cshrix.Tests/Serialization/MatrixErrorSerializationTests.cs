// <copyright file="MatrixErrorSerializationTests.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Tests.Serialization
{
    using System;

    using Errors;

    using Newtonsoft.Json;

    using NUnit.Framework;

    [TestFixture]
    public class MatrixErrorSerializationTests
    {
        [Test]
        public void ShouldSerializeSimpleError()
        {
            var error = new MatrixError("foobar", "foobar was invalid");
            var json = JsonConvert.SerializeObject(error);
            Assert.AreEqual("{\"errcode\":\"foobar\",\"error\":\"foobar was invalid\"}", json);
        }

        [Test]
        public void ShouldSerializeRateLimitError()
        {
            var error = new RateLimitError("ratelimit", "you were rate limited", TimeSpan.FromSeconds(1));
            var json = JsonConvert.SerializeObject(error);

            Assert.AreEqual(
                "{\"retry_after_ms\":1000,\"errcode\":\"ratelimit\",\"error\":\"you were rate limited\"}",
                json);
        }

        [Test]
        public void ShouldDeserializeSimpleError()
        {
            const string Json = @"{
                ""errcode"": ""foobar"",
                ""error"": ""foobar was invalid""
            }";

            var error = JsonConvert.DeserializeObject<MatrixError>(Json);

            Assert.AreEqual("foobar", error.Code);
            Assert.AreEqual("foobar was invalid", error.Message);
        }

        [Test]
        public void ShouldDeserializeRateLimitError()
        {
            const string Json = @"{
                ""errcode"": ""ratelimit"",
                ""error"": ""you were rate limited"",
                ""retry_after_ms"": 1000
            }";

            var error = JsonConvert.DeserializeObject<RateLimitError>(Json);

            Assert.AreEqual("ratelimit", error.Code);
            Assert.AreEqual("you were rate limited", error.Message);
            Assert.AreEqual(TimeSpan.FromSeconds(1), error.RetryAfter);
        }

        [Test]
        public void ShouldDeserializeRateLimitErrorWhenBaseType()
        {
            const string Json = @"{
                ""errcode"": ""ratelimit"",
                ""error"": ""you were rate limited"",
                ""retry_after_ms"": 1000
            }";

            var error = JsonConvert.DeserializeObject<MatrixError>(Json);

            Assert.That(error, Is.TypeOf<RateLimitError>());
            Assert.AreEqual("ratelimit", error.Code);
            Assert.AreEqual("you were rate limited", error.Message);
            var rateLimitError = (RateLimitError)error;
            Assert.AreEqual(TimeSpan.FromSeconds(1), rateLimitError.RetryAfter);
        }
    }
}
