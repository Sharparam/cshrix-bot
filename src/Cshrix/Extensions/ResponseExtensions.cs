// <copyright file="ResponseExtensions.cs">
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

    public static class ResponseExtensions
    {
        public static MatrixError GetError<T>(this Response<T> response)
        {
            var error = JsonConvert.DeserializeObject<MatrixError>(response.StringContent);
            return error;
        }

        public static TError GetError<T, TError>(this Response<T> response) where TError : MatrixError
        {
            var error = JsonConvert.DeserializeObject<TError>(response.StringContent);
            return error;
        }

        public static bool TryGetError<T>(this Response<T> response, out MatrixError error)
        {
            try
            {
                error = response.GetError();
                return true;
            }
            catch (JsonSerializationException)
            {
                error = default;
                return false;
            }
        }

        public static bool TryGetError<T, TError>(this Response<T> response, out TError error)
            where TError : MatrixError
        {
            try
            {
                error = response.GetError<T, TError>();
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
