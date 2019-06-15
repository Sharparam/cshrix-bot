// <copyright file="IPowerLevels.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using Data;
    using Data.Events.Content;

    using JetBrains.Annotations;

    /// <summary>
    /// Allows an application to check power levels on a room, and test users against them.
    /// </summary>
    public interface IPowerLevels
    {
        /// <summary>
        /// Gets the power level needed to perform a certain action.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        /// <returns>The power level needed.</returns>
        int this[PowerAction action] { get; }

        /// <summary>
        /// Gets the power level of a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="isCreator">
        /// <c>true</c> if the specified user is the room creator; otherwise, <c>false</c>.
        /// </param>
        /// <returns>The power level of the user.</returns>
        int this[UserId userId, bool isCreator = false] { get; }

        /// <summary>
        /// Gets the power level needed to send an event of a certain type.
        /// </summary>
        /// <param name="eventType">The type of the event.</param>
        /// <param name="isState">
        /// <c>true</c> to treat the event as a state event; otherwise, <c>false</c>.
        /// </param>
        int this[string eventType, bool isState = false] { get; }

        /// <summary>
        /// Gets the underlying event content providing data.
        /// </summary>
        [CanBeNull]
        PowerLevelsContent Content { get; }

        /// <summary>
        /// Checks if a user can perform a certain action.
        /// </summary>
        /// <param name="userId">The user ID to test.</param>
        /// <param name="action">The action they are to perform.</param>
        /// <param name="isCreator">
        /// <c>true</c> if the user is the room creator; otherwise, <c>false</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the user can perform the action; otherwise, <c>false</c>.
        /// </returns>
        bool Can(UserId userId, PowerAction action, bool isCreator = false);

        /// <summary>
        /// Checks if the user can send a certain type of event.
        /// </summary>
        /// <param name="userId">The user ID to test.</param>
        /// <param name="eventType">The type of event they are to send.</param>
        /// <param name="isState"><c>true</c> if it is a state event; otherwise, <c>false</c>.</param>
        /// <param name="isCreator">
        /// <c>true</c> if the user is the room creator; otherwise, <c>false</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the user can send the event; otherwise, <c>false</c>.
        /// </returns>
        bool Can(UserId userId, string eventType, bool isState = false, bool isCreator = false);
    }
}
