// <copyright file="SafeOlmHandle.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Cryptography.Olm
{
    using System;
    using System.Runtime.InteropServices;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Contains the handle to an Olm resource.
    /// </summary>
    public abstract class SafeOlmHandle : SafeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeOlmHandle" /> class.
        /// </summary>
        /// <param name="log">A logger instance for this handle.</param>
        /// <param name="size">The size of the memory to allocate.</param>
        protected SafeOlmHandle(ILogger log, uint size)
            : base(IntPtr.Zero, true)
        {
            Log = log;

            try
            {
            }
            finally
            {
                handle = Marshal.AllocHGlobal((int)size);
                Log.LogDebug("Memory allocated at {Handle}", handle);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the handle value is invalid.
        /// </summary>
        /// <returns><c>true</c> if the handle value is invalid; otherwise, <c>false</c>.</returns>
        public override bool IsInvalid => handle == IntPtr.Zero;

        /// <summary>
        /// Gets the logger instance for this class.
        /// </summary>
        protected ILogger Log { get; }

        /// <summary>
        /// Executes the code required to free the handle.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the handle is released successfully;
        /// otherwise, in the event of a catastrophic failure, <c>false</c>.
        /// In this case, it generates a
        /// <see cref="https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/releasehandlefailed-mda">
        /// releaseHandleFailed
        /// </see>
        /// Managed Debugging Assistant.
        /// </returns>
        protected override bool ReleaseHandle()
        {
            if (handle == IntPtr.Zero)
            {
                return false;
            }

            try
            {
            }
            finally
            {
                ClearOlmResource();
                Marshal.FreeHGlobal(handle);
            }

            return true;
        }

        /// <summary>
        /// Executes the code required to clean up the Olm resource (not freeing memory).
        /// </summary>
        protected abstract void ClearOlmResource();
    }
}
