// <copyright file="ICommand.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Bot.Console.Plugins
{
    using System.Collections.Generic;

    /// <summary>
    /// Describes a command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets a collection of verbs that activate this command.
        /// </summary>
        IReadOnlyCollection<string> Verbs { get; }

        /// <summary>
        /// Gets help text for the command.
        /// </summary>
        string Help { get; }

        /// <summary>
        /// Gets the power level required to use the command.
        /// </summary>
        int PowerLevel { get; }
    }
}
