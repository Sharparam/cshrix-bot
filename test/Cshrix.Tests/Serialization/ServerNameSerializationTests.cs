namespace Cshrix.Tests.Serialization
{
    using Data;

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
            const string serverName = "\"matrix.sharparam.com\"";
            var deserialized = JsonConvert.DeserializeObject<ServerName>(serverName);

            Assert.AreEqual("matrix.sharparam.com", deserialized.Hostname);
        }

        [Test]
        public void ShouldDeserializeObjectServerName()
        {
            const string serverName = @"{
                ""Hostname"": ""matrix.sharparam.com"",
            }";

            var deserialized = JsonConvert.DeserializeObject<ServerName>(serverName);

            Assert.AreEqual("matrix.sharparam.com", deserialized.Hostname);
        }
    }
}
