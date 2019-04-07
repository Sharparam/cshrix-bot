// <copyright file="Bot.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Bot.Console
{
    using Microsoft.Extensions.Logging;

    internal sealed class Bot
    {
        private readonly ILogger _log;

        public Bot(ILogger<Bot> log) => _log = log;
    }
}
