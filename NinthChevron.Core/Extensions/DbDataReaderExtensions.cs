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
using System.Data.Common;

namespace NinthChevron.Extensions
{
    public static class DbDataReaderExtensions
	{
		public static string GetString(this DbDataReader reader , int index, string defaultValue)
		{
			return reader.IsDBNull(index) ? defaultValue : reader.GetString(index);
		}

        public static int GetInt32(this DbDataReader reader, int index, int defaultValue)
		{
			return reader.IsDBNull(index) ? defaultValue : reader.GetInt32(index);
		}

        public static DateTime? GetDateTime(this DbDataReader reader, int index, DateTime? defaultValue)
		{
			return reader.IsDBNull(index) ? defaultValue : reader.GetDateTime(index);
		}

        public static bool GetBoolean(this DbDataReader reader, int index, bool defaultValue)
		{
			return reader.IsDBNull(index) ? defaultValue : reader.GetBoolean(index);
		}

        public static Guid GetGuid(this DbDataReader reader, int index, Guid defaultValue)
		{
			return reader.IsDBNull(index) ? defaultValue : reader.GetGuid(index);
		}
	}
}
