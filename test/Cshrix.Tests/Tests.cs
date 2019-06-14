// <copyright file="Tests.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

using NUnit.Framework;

namespace Cshrix.Tests
{
    using System.Text;

    using Cryptography.Olm;

    using Microsoft.Extensions.Logging;

    using Moq;

    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestOlmAccount()
        {
            var account = new OlmAccount(Mock.Of<ILogger<OlmAccount>>());

            var key = Encoding.UTF8.GetBytes("i <3 lonami");
            var pickled = account.Pickle(key);

            var account2 = new OlmAccount(Mock.Of<ILogger<OlmAccount>>());

            account2.Unpickle(pickled, key);

            Assert.Pass();
        }
    }
}
