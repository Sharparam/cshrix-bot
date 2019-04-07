// <copyright file="UserIdSerializationTests.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Tests.Serialization
{
    using Data;

    using Newtonsoft.Json;

    using NUnit.Framework;

    [TestFixture]
    public class UserIdSerializationTests
    {
        [Test]
        public void ShouldSerializeUserId()
        {
            var userId = new UserId("@sharparam:matrix.sharparam.com");
            var serialized = JsonConvert.SerializeObject(userId);

            Assert.AreEqual("\"@sharparam:matrix.sharparam.com\"", serialized);
        }

        [Test]
        public void ShouldDeserializeStringUserId()
        {
            const string userId = "\"@sharparam:matrix.sharparam.com\"";
            var deserialized = JsonConvert.DeserializeObject<UserId>(userId);

            Assert.AreEqual(IdentifierType.User, deserialized.Type);
            Assert.AreEqual('@', deserialized.Sigil);
            Assert.AreEqual("sharparam", deserialized.Localpart);
            Assert.AreEqual("matrix.sharparam.com", deserialized.Domain.Hostname);
        }

        [Test]
        public void ShouldDeserializeObjectUserId()
        {
            const string userId = @"{
                ""Localpart"": ""sharparam"",
                ""Domain"": {
                    ""Hostname"": ""matrix.sharparam.com""
                }
            }";

            var deserialized = JsonConvert.DeserializeObject<UserId>(userId);

            Assert.AreEqual(IdentifierType.User, deserialized.Type);
            Assert.AreEqual('@', deserialized.Sigil);
            Assert.AreEqual("sharparam", deserialized.Localpart);
            Assert.AreEqual("matrix.sharparam.com", deserialized.Domain.Hostname);
        }
    }
}
