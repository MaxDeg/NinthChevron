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

namespace BlueBoxSharp.Data
{
    public static class SqlFunctions
    {
        public static bool IsNull(object key) { throw new InvalidOperationException(); }

        public static DateTime GetDate() { throw new InvalidOperationException(); }

        public static DateTime DateAdd(DatePart datePart, int number, DateTime date)
        {
            throw new InvalidOperationException();
        }

        public static int DateDiff(DateTime date, DateTime otherDate, DatePart datePart = DatePart.None)
        {
            throw new InvalidOperationException();
        }

        public enum DatePart 
        { 
            None, Year, Quarter, Month, DayOfYear, Day, Week, DayOfWeek, Hour, Minute, Second, MilliSecond, MicroSecond, NanoSecond 
        }
    }
}
