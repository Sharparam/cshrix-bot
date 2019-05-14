// <copyright file="EventContent.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Data.Events.Content
{
    using JetBrains.Annotations;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Contains additional content for an event.
    /// </summary>
    public class EventContent
    {
        /// <summary>
        /// Additional data not otherwise parsed by child classes of <see cref="EventContent" />.
        /// </summary>
        [UsedImplicitly]
        [JsonExtensionData]
        public JObject AdditionalData
        {
            get;

            [UsedImplicitly]
            protected set;
        }
    }
}
