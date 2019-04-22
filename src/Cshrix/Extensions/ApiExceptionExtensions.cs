// <copyright file="ApiExceptionExtensions.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Extensions
{
    using System;

    using Errors;

    using Newtonsoft.Json;

    using RestEase;

    public static class ApiExceptionExtensions
    {
        public static MatrixError GetError(this ApiException exception)
        {
            var error = JsonConvert.DeserializeObject<MatrixError>(exception.Content);
            return error;
        }

        public static TError GetError<TError>(this ApiException exception) where TError : MatrixError
        {
            var error = JsonConvert.DeserializeObject<TError>(exception.Content);
            return error;
        }

        public static bool TryGetError(this ApiException exception, out MatrixError error)
        {
            try
            {
                error = exception.GetError();
                return true;
            }
            catch (JsonSerializationException)
            {
                error = default;
                return false;
            }
        }

        public static bool TryGetError<TError>(this ApiException exception, out TError error) where TError : MatrixError
        {
            try
            {
                error = exception.GetError<TError>();
                return true;
            }
            catch (JsonSerializationException)
            {
                error = default;
                return false;
            }
            catch (InvalidCastException)
            {
                error = default;
                return false;
            }
        }
    }
}
