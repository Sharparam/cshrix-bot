// <copyright file="IMatrixClient.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix
{
    using System;
    using System.Threading.Tasks;

    using Data;
    using Data.Notifications;

    public interface IMatrixClient
    {
        Task<UserId> GetUserIdAsync();

        Task<NotificationRulesets> GetNotificationPushRulesAsync();

        Task<PreviewInfo> GetPreviewInfoAsync(Uri uri, DateTimeOffset? at = null);
    }
}
