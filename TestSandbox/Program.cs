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
using BlueBoxSharp.Data.Translators.Handlers;

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

            Console.WriteLine(NativeMethodHandlers.GetHandler(typeof(string), "Translate").Translate(""));


            Console.WriteLine("Ok it's end");
            Console.ReadKey();
        }
    }
}
