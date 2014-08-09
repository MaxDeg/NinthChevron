/**
 *   Copyright 2013
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinthChevron.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string value)
        {
            return value.Substring(0, 1).ToUpper() + (value.Length > 1 ? value.Substring(1) : string.Empty);
        }

        public static string ToUpperCamelCase(this string value)
        {
            string result = string.Empty;

            foreach (string word in value.Split('_', ' ', '-'))
            {
                if (string.IsNullOrWhiteSpace(word))
                    result += "_";
                else
                    result += word.Capitalize();
            }

            return result;
        }

        public static string ToLowerCamelCase(this string value)
        {
            string result = string.Empty;

            foreach (string word in value.Split('_', ' ', '-'))
            {
                if (string.IsNullOrWhiteSpace(word))
                    result += "_";
                else
                    result += word.Substring(0, 1).ToLower() + (word.Length > 1 ? word.Substring(1) : string.Empty);
            }

            return result;
        }

        public static string ConvertToCSharpName(this string value)
        {
            return (char.IsDigit(value[0]) ? "_" : "") + value;
        }
    }
}
