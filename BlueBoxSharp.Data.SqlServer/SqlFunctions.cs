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

namespace BlueBoxSharp.Data.SqlServer
{
    public static class SqlFunctions
    {
        [SqlFunction("{0} IS NULL")]
        public static bool IsNull(object key) { throw new InvalidOperationException(); }

        [SqlFunction("GETDATE()")]
        public static DateTime GetDate() { throw new InvalidOperationException(); }

        [SqlFunction("DATEADD({0}, {1}, {2})")]
        public static DateTime DateAdd(DatePart datePart, int number, DateTime date)
        {
            throw new InvalidOperationException();
        }

        [SqlFunction("DATEDIFF({0}, {1}, {2})")]
        public static int DateDiff(DatePart datePart, DateTime date, DateTime otherDate)
        {
            throw new InvalidOperationException();
        }

        [SqlKeyword]
        public enum DatePart
        {
            Year, Quarter, Month, DayOfYear, Day, Week, DayOfWeek, Hour, Minute, Second, MilliSecond, MicroSecond, NanoSecond
        }
    }
}
