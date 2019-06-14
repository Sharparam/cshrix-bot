// <copyright file="OlmAccountTests.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Tests.Cryptography.Olm
{
    using System.Text;

    using Cshrix.Cryptography.Olm;

    using Microsoft.Extensions.Logging;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    [Category("olm")]
    public class OlmAccountTests
    {
        [Test]
        public void ShouldUnpickleAccountWithCorrectKey()
        {
            var account = new OlmAccount(Mock.Of<ILogger<OlmAccount>>());

            var key = Encoding.UTF8.GetBytes("i <3 lonami");
            var pickled = account.Pickle(key);

            var account2 = new OlmAccount(Mock.Of<ILogger<OlmAccount>>());

            account2.Unpickle(pickled, key);

            // The test is successful if no exception was thrown when unpickling the account
            Assert.Pass();
        }
    }
}
