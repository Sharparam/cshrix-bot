// <copyright file="NotificationRulesets.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Notifications
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    public class NotificationRulesets : ReadOnlyDictionary<string, NotificationRuleset>
    {
        public NotificationRulesets([NotNull] IDictionary<string, NotificationRuleset> dictionary)
            : base(dictionary)
        {
        }

        [JsonIgnore]
        public NotificationRuleset Global => this["global"];
    }
}
