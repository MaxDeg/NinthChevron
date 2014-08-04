using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TestSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(AppSettings.IsDebugMode);
                Console.WriteLine(ConnectionStrings.T4Connection);
                Console.WriteLine(ConnectionStrings.LocalSqlServer);

                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    break;
            }
            Console.WriteLine("Ok it's end");
            Console.ReadKey();
        }
    }
}
