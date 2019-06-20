// <copyright file="EventHandlerExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Extensions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Events;

    using JetBrains.Annotations;

    /// <summary>
    /// Contains extensions to <see cref="EventHandler" />, <see cref="EventHandler{TEventArgs}" />,
    /// <see cref="AsyncEventHandler" />, and <see cref="AsyncEventHandler{TEventArgs}" />.
    /// </summary>
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// Converts a synchronous event handler to an asynchronous one.
        /// </summary>
        /// <param name="eventHandler">The event handler to convert.</param>
        /// <returns>The converted handler.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="eventHandler" /> is <c>null</c>.
        /// </exception>
        public static AsyncEventHandler ToAsync([NotNull] this EventHandler eventHandler)
        {
            if (eventHandler == null)
            {
                throw new ArgumentNullException(nameof(eventHandler));
            }

            return (sender, eventArgs) =>
            {
                eventHandler(sender, eventArgs);
                return Task.CompletedTask;
            };
        }

        /// <summary>
        /// Converts a synchronous event handler to an asynchronous one.
        /// </summary>
        /// <param name="eventHandler">The event handler to convert.</param>
        /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
        /// <returns>The converted handler.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="eventHandler" /> is <c>null</c>.
        /// </exception>
        public static AsyncEventHandler<TEventArgs> ToAsync<TEventArgs>([NotNull] this EventHandler<TEventArgs> eventHandler)
        {
            if (eventHandler == null)
            {
                throw new ArgumentNullException(nameof(eventHandler));
            }

            return (sender, eventArgs) =>
            {
                eventHandler(sender, eventArgs);
                return Task.CompletedTask;
            };
        }

        /// <summary>
        /// Invokes an asynchronous event handler asynchronously.
        /// </summary>
        /// <param name="eventHandler">The event handler to invoke.</param>
        /// <param name="sender">The object raising the event.</param>
        /// <param name="eventArgs">Event arguments.</param>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        public static Task InvokeAsync(
            [CanBeNull] this AsyncEventHandler eventHandler,
            object sender,
            EventArgs eventArgs)
        {
            if (eventHandler == null)
            {
                return Task.CompletedTask;
            }

            var delegates = eventHandler.GetInvocationList().Cast<AsyncEventHandler>();
            var tasks = delegates.Select(d => d.Invoke(sender, eventArgs));

            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// Invokes an asynchronous event handler asynchronously.
        /// </summary>
        /// <param name="eventHandler">The event handler to invoke.</param>
        /// <param name="sender">The object raising the event.</param>
        /// <param name="eventArgs">Event arguments.</param>
        /// <typeparam name="TEventArgs">Type of the event arguments.</typeparam>
        /// <returns>A <see cref="Task" /> representing progress.</returns>
        public static Task InvokeAsync<TEventArgs>(
            [CanBeNull] this AsyncEventHandler<TEventArgs> eventHandler,
            object sender,
            TEventArgs eventArgs)
        {
            if (eventHandler == null)
            {
                return Task.CompletedTask;
            }

            var delegates = eventHandler.GetInvocationList().Cast<AsyncEventHandler<TEventArgs>>();
            var tasks = delegates.Select(d => d.Invoke(sender, eventArgs));

            return Task.WhenAll(tasks);
        }
    }
}
