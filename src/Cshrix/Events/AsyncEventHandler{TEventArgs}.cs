// <copyright file="AsyncEventHandler{TEventArgs}.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Events
{
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an asynchronous event handler.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="eventArgs">Event arguments.</param>
    /// <typeparam name="TEventArgs">Type of the event arguments.</typeparam>
    /// <returns>A <see cref="Task" /> representing progress.</returns>
    public delegate Task AsyncEventHandler<in TEventArgs>(object sender, TEventArgs eventArgs);
}
