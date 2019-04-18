// <copyright file="NotificationActionSerializationTests.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

using NUnit.Framework;

namespace Cshrix.Tests.Serialization
{
    using Cshrix.Data;

    using Newtonsoft.Json;

    [TestFixture]
    public class NotificationActionSerializationTests
    {
        [Test]
        public void ShouldDeserializeSimpleAction()
        {
            const string Json = "\"notify\"";
            var action = JsonConvert.DeserializeObject<NotificationAction>(Json);

            Assert.AreEqual("notify", action.Action);
            Assert.IsNull(action.Name);
            Assert.IsNull(action.Value);
        }

        [TestCase("sound", "default")]
        [TestCase("highlight", false)]
        public void ShouldDeserializeObjectActionWithValue(string name, object value)
        {
            var json = $@"{{
                ""set_tweak"": ""{name}"",
                ""value"": {JsonConvert.SerializeObject(value)}
            }}";

            var action = JsonConvert.DeserializeObject<NotificationAction>(json);

            Assert.AreEqual("set_tweak", action.Action);
            Assert.AreEqual(name, action.Name);
            Assert.AreEqual(value, action.Value);
        }

        [TestCase("highlight")]
        public void ShouldDeserializeObjectActionWithNoValue(string name)
        {
            var json = $@"{{
                ""set_tweak"": ""{name}""
            }}";

            var action = JsonConvert.DeserializeObject<NotificationAction>(json);

            Assert.AreEqual("set_tweak", action.Action);
            Assert.AreEqual(name, action.Name);
            Assert.IsNull(action.Value);
        }

        [Test]
        public void ShouldSerializeSimpleAction()
        {
            var action = new NotificationAction("notify");
            var json = JsonConvert.SerializeObject(action);

            Assert.AreEqual("\"notify\"", json);
        }

        [TestCase("sound", "default")]
        [TestCase("highlight", false)]
        public void ShouldSerializeObjectActionWithValue(string name, object value)
        {
            var expectedJson = $"{{\"set_tweak\":\"{name}\",\"value\":{JsonConvert.SerializeObject(value)}}}";
            var action = new NotificationAction("set_tweak", name, value);
            var json = JsonConvert.SerializeObject(action);

            Assert.AreEqual(expectedJson, json);
        }

        [TestCase("highlight")]
        public void ShouldSerializeObjectActionWithNoValue(string name)
        {
            var expectedJson = $"{{\"set_tweak\":\"{name}\"}}";
            var action = new NotificationAction("set_tweak", name);
            var json = JsonConvert.SerializeObject(action);

            Assert.AreEqual(expectedJson, json);
        }
    }
}
