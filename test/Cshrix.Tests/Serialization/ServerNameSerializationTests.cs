// <copyright file="ServerNameSerializationTests.cs">
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
    public class ServerNameSerializationTests
    {
        [Test]
        public void ShouldSerializeUserId()
        {
            var serverName = new ServerName("matrix.sharparam.com");
            var serialized = JsonConvert.SerializeObject(serverName);

            Assert.AreEqual("\"matrix.sharparam.com\"", serialized);
        }

        [Test]
        public void ShouldDeserializeStringServerName()
        {
            const string ServerName = "\"matrix.sharparam.com\"";
            var deserialized = JsonConvert.DeserializeObject<ServerName>(ServerName);

            Assert.AreEqual("matrix.sharparam.com", deserialized.Hostname);
        }

        [Test]
        public void ShouldDeserializeObjectServerName()
        {
            const string ServerName = @"{
                ""Hostname"": ""matrix.sharparam.com"",
            }";

            var deserialized = JsonConvert.DeserializeObject<ServerName>(ServerName);

            Assert.AreEqual("matrix.sharparam.com", deserialized.Hostname);
        }
    }
}
