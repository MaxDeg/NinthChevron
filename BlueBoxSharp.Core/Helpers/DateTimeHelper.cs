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
using System.Globalization;
using System.Linq;
using System.Text;

namespace BlueBoxSharp.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime Parse(int shortDate)
        {
            if (shortDate <= 0) 
                throw new ArgumentException("shortDate must be greather than 0");

            return DateTime.ParseExact(shortDate.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
        }

        public static bool TryParse(int shortDate, out DateTime result)
        {
            result = new DateTime();

            if (shortDate <= 0) 
                return false;

            return DateTime.TryParseExact(shortDate.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        }
    }
}
