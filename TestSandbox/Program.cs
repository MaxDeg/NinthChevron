using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using BlueBoxSharp.Data.SqlServer;
using BlueBoxSharp.Data;

namespace TestSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            //while (true)
            //{
            //    Console.WriteLine(AppSettings.IsDebugMode);
            //    Console.WriteLine(ConnectionStrings.T4Connection);
            //    Console.WriteLine(ConnectionStrings.LocalSqlServer);

            //    if (Console.ReadKey().Key == ConsoleKey.Escape)
            //        break;
            //}

            SqlServerDataContext dbContext = new SqlServerDataContext(ConnectionStrings.T4Connection);

            int count = CountNotes(dbContext, "BVDEP", "", "d", "").Select(r => r.Get<int>(0)).FirstOrDefault();
            Console.WriteLine(count);


            Console.WriteLine("Ok it's end");
            Console.ReadKey();
        }

        public static IEnumerable<DataRecord> CountNotes(DataContext context, string inUserId, string inCompanyId, string inType, string inReference)
        {
            return context.ExecuteProcedure("[NAS_SonyESI]..[dbo].[CountNotes]", inUserId, inCompanyId, inType, inReference);
        }
    }
}
