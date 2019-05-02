// <copyright file="MatrixClientConfiguration.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Configuration
{
    using System;

    /// <summary>
    /// Configuration for <see cref="MatrixClient" />.
    /// </summary>
    public class MatrixClientConfiguration
    {
        /// <summary>
        /// The default name used to specify this configuration in an appsettings.json file.
        /// </summary>
        public const string DefaultSectionName = nameof(MatrixClientConfiguration);

        /// <summary>
        /// The base URI of the homeserver.
        /// </summary>
        /// <example>https://matrix.org</example>
        public Uri BaseUri { get; set; }

        /// <summary>
        /// The API version of the API to use.
        /// </summary>
        /// <example>r0</example>
        /// <example>unstable</example>
        public string ApiVersion { get; set; }

        /// <summary>
        /// Access token to use for authenticating the user.
        /// Not required for endpoints that don't require authentication.
        /// </summary>
        public string AccessToken { get; set; }
    }
}
