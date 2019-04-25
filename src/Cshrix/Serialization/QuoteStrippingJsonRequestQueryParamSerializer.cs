// <copyright file="QuoteStrippingJsonRequestQueryParamSerializer.cs">
//   Copyright (c) 2019 by Adam Hellberg.
//
//   This Source Code Form is subject to the terms of the Mozilla Public
//   License, v. 2.0. If a copy of the MPL was not distributed with this
//   file, You can obtain one at http://mozilla.org/MPL/2.0/.
// </copyright>

namespace Cshrix.Serialization
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using RestEase;

    public sealed class QuoteStrippingJsonRequestQueryParamSerializer : RequestQueryParamSerializer
    {
        public override IEnumerable<KeyValuePair<string, string>> SerializeQueryParam<T>(
            string name,
            T value,
            RequestQueryParamSerializerInfo info)
        {
            if (value == null)
            {
                yield break;
            }

            var serializedValue = Serialize(value);
            yield return new KeyValuePair<string, string>(name, serializedValue);
        }

        public override IEnumerable<KeyValuePair<string, string>> SerializeQueryCollectionParam<T>(
            string name,
            IEnumerable<T> values,
            RequestQueryParamSerializerInfo info)
        {
            if (values == null)
            {
                yield break;
            }

            foreach (var value in values)
            {
                if (value == null)
                {
                    continue;
                }

                var serializedValue = Serialize(value);
                yield return new KeyValuePair<string, string>(name, serializedValue);
            }
        }

        private static string Serialize<T>(T value)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            var stripped = StripQuotes(serializedValue);
            return stripped;
        }

        private static string StripQuotes(string value) => value.TrimStart('"').TrimEnd('"');
    }
}
