// <copyright file="OlmAccount.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Cryptography.Olm
{
    using System;
    using System.Buffers;

    using Microsoft.Extensions.Logging;

    using Utilities;

    using static Olm;

    /// <summary>
    /// Contains <c>OlmAccount</c> functions.
    /// </summary>
    public sealed class OlmAccount : SafeOlmHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OlmAccount" /> class.
        /// </summary>
        public OlmAccount(ILogger<OlmAccount> log)
            : base(log, olm_account_size()) =>
            handle = CreateAccount(log, handle);

        /// <summary>
        /// Pickles this account.
        /// </summary>
        /// <param name="key">The key to encrypt the pickle with.</param>
        /// <returns>The pickled account.</returns>
        public byte[] Pickle(byte[] key)
        {
            var pickledLength = olm_pickle_account_length(handle);
            var pickled = new byte[(int)pickledLength];
            var result = olm_pickle_account(handle, key, (uint)key.Length, pickled, pickledLength);
            return pickled;
        }

        /// <summary>
        /// Unpickles pickled account data into this account.
        /// </summary>
        /// <param name="pickled">The pickled account.</param>
        /// <param name="key">A key to decrypt the pickle with.</param>
        public void Unpickle(byte[] pickled, byte[] key)
        {
            var result = olm_unpickle_account(handle, key, (uint)key.Length, pickled, (uint)pickled.Length);
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes the code required to clean up the <c>OlmAccount</c> resource.
        /// </summary>
        protected override void ClearOlmResource()
        {
            Log.LogDebug("Clearing OlmAccount memory");
            olm_clear_account(handle);
        }

        /// <summary>
        /// Creates an <c>OlmAccount</c> and writes it to this account instance.
        /// </summary>
        /// <param name="log">The logger instance for the class.</param>
        /// <param name="memory">The memory location in which to create the account.</param>
        /// <returns>A pointer to the created account.</returns>
        private static IntPtr CreateAccount(ILogger log, IntPtr memory)
        {
            log.LogDebug("Creating OlmAccount");

            var pool = ArrayPool<byte>.Shared;

            var accountHandle = olm_account(memory);
            log.LogDebug("Got account handle: {Handle}", accountHandle);

            var randomLength = olm_create_account_random_length(accountHandle);
            var randomBuffer = pool.Rent((int)randomLength);

            log.LogDebug("Generating {Count} random bytes", randomLength);
            RandomUtils.SecureBytes(randomBuffer);

            var createResult = olm_create_account(accountHandle, randomBuffer, randomLength);
            log.LogDebug("Account created with result: {Result}", createResult);

            pool.Return(randomBuffer, true);

            log.LogDebug("Returning created account handle {Handle}", accountHandle);
            return accountHandle;
        }
    }
}
