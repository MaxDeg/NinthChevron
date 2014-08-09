using NinthChevron.Data.SqlServer.Translators;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinthChevron.Data.SqlServer
{
    public class SqlServer2000DataContext : DataContext
    {
        public SqlServer2000DataContext(string connectionString)
            : base(connectionString)
        {
            this.Translator = new TSqlTranslator(this);
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
