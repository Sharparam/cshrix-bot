// <copyright file="PowerLevels.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using System;

    using Data;
    using Data.Events.Content;

    using Extensions;

    /// <inheritdoc />
    /// <summary>
    /// Contains the power levels for a room and methods to test them.
    /// </summary>
    public sealed class PowerLevels : IPowerLevels
    {
        /// <summary>
        /// Default power level if <see cref="Content" /> is <c>null</c>.
        /// </summary>
        private const int DefaultLevel = 0;

        /// <summary>
        /// Default power level for a room creator if <see cref="Content" /> is <c>null</c>.
        /// </summary>
        private const int DefaultCreatorLevel = 100;

        /// <summary>
        /// Default power level for actions if <see cref="Content" /> is <c>null</c>.
        /// </summary>
        private const int DefaultActionLevel = 50;

        /// <inheritdoc />
        public int this[PowerAction action] => GetActionLevel(action);

        /// <inheritdoc />
        public int this[UserId userId, bool isCreator = false] => GetUserLevel(userId, isCreator);

        /// <inheritdoc />
        public int this[string eventType, bool isState = false] => GetEventTypeLevel(eventType, isState);

        /// <inheritdoc />
        public PowerLevelsContent Content { get; internal set; }

        /// <inheritdoc />
        public bool Can(UserId userId, PowerAction action, bool isCreator = false)
        {
            var userLevel = this[userId, isCreator];
            var actionLevel = this[action];
            return userLevel >= actionLevel;
        }

        /// <inheritdoc />
        public bool Can(UserId userId, string eventType, bool isState = false, bool isCreator = false)
        {
            var userLevel = this[userId, isCreator];
            var eventLevel = this[eventType, isState];
            return userLevel >= eventLevel;
        }

        /// <summary>
        /// Gets the power level required to perform an action.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        /// <returns>The power level required.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the supplied action is not a valid action value.
        /// </exception>
        private int GetActionLevel(PowerAction action)
        {
            if (Content == null)
            {
                return DefaultActionLevel;
            }

            switch (action)
            {
                case PowerAction.Invite:
                    return Content.Invite;

                case PowerAction.Kick:
                    return Content.Kick;

                case PowerAction.Ban:
                    return Content.Ban;

                case PowerAction.Redact:
                    return Content.Redact;

                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        /// <summary>
        /// Gets the power level of a user.
        /// </summary>
        /// <param name="userId">The ID of the user to query.</param>
        /// <param name="isCreator"><c>true</c> if the user is the room creator; otherwise, <c>false</c>.</param>
        /// <returns>The power level of the user.</returns>
        private int GetUserLevel(UserId userId, bool isCreator = false)
        {
            if (Content == null)
            {
                return isCreator ? DefaultCreatorLevel : DefaultLevel;
            }

            var def = Content.UsersDefault;
            return Content.Users?.GetValueOrDefault(userId, def) ?? def;
        }

        /// <summary>
        /// Gets the power level required for a certain event type.
        /// </summary>
        /// <param name="type">The event type to query.</param>
        /// <param name="isState"><c>true</c> if it is a state event; otherwise, <c>false</c>.</param>
        /// <returns>The power level required for the event type.</returns>
        private int GetEventTypeLevel(string type, bool isState)
        {
            if (Content == null)
            {
                return DefaultLevel;
            }

            var def = isState ? Content.StateDefault : Content.EventsDefault;
            return Content.Events?.GetValueOrDefault(type, def) ?? def;
        }
    }
}
