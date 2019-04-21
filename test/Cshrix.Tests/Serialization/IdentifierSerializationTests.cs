// <copyright file="UserIdSerializationTests.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Tests.Serialization
{
    using Cshrix.Data;

    using Newtonsoft.Json;

    using NUnit.Framework;

    [TestFixture]
    public class IdentifierSerializationTests
    {
        [Test]
        public void ShouldSerializeUserId()
        {
            var userId = new Identifier("@sharparam:matrix.sharparam.com");
            var serialized = JsonConvert.SerializeObject(userId);

            Assert.AreEqual("\"@sharparam:matrix.sharparam.com\"", serialized);
        }

        [Test]
        public void ShouldDeserializeStringUserId()
        {
            const string UserId = "\"@sharparam:matrix.sharparam.com\"";
            var deserialized = JsonConvert.DeserializeObject<Identifier>(UserId);

            Assert.AreEqual(IdentifierType.User, deserialized.Type);
            Assert.AreEqual('@', deserialized.Sigil);
            Assert.AreEqual("sharparam", deserialized.Localpart);
            Assert.True(deserialized.Domain.HasValue);
            Assert.AreEqual("matrix.sharparam.com", deserialized.Domain.Value.Hostname);
        }

        [Test]
        public void ShouldDeserializeV3EventId()
        {
            const string Localpart = "acR1l0raoZnm60CBwAVgqbZqoO/mYU81xysh1u7XcJk";
            const string Json = "\"$" + Localpart + "\"";
            var deserialized = JsonConvert.DeserializeObject<Identifier>(Json);

            Assert.AreEqual(IdentifierType.Event, deserialized.Type);
            Assert.AreEqual('$', deserialized.Sigil);
            Assert.AreEqual(Localpart, deserialized.Localpart);
            Assert.False(deserialized.Domain.HasValue);
            Assert.Null(deserialized.Domain);
        }
    }
}
