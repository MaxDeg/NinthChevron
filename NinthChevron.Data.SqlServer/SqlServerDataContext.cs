﻿/**
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
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using NinthChevron.Data.Translators;
using NinthChevron.Data.SqlServer.Translators;

namespace NinthChevron.Data.SqlServer
{
    public class SqlServerDataContext : DataContext
    {
        public SqlServerDataContext(string connectionString) : base(connectionString)
        {
            this.Translator = new TSqlTranslator2005(this);
        }

        protected override DbCommand CreateCommand(DbConnection connection, DbTransaction transaction, string query)
        {
            if (transaction == null)
            {
                return new SqlCommand(query, (SqlConnection)connection);
            }

            return new SqlCommand(query, (SqlConnection)connection, (SqlTransaction)transaction);
        }

        protected override DbConnection GetConnection(bool open = true)
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            if (open)
            {
                connection.Open();
            }

            return connection;
        }
    }
}
