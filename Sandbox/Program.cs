using BlueBoxSharp.Data;
using BlueBoxSharp.Data.Metadata;
using BlueBoxSharp.Data.SqlServer;
using BlueBoxSharp.Data.Translators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ok it's start");

            //MySqlMetadata meta = new MySqlMetadata("Server=localhost;Database=sakila;Uid=root;Pwd=Secret.123456;");
            MySqlMetadata meta = new MySqlMetadata("Server=localhost;Database=sakila;Uid=root;Pwd=Secret.123456;");

            //foreach (ITableMetadata table in meta.Tables.Where(t => t.Name == "store"))
            //{
            //    Console.WriteLine(table.Name);
            //    foreach (IColumnMetadata c in table.Columns.Values)
            //    {
            //        Console.WriteLine(GeneratorHelper.CreateProperty(c));

            //        if (c is IJoinColumnMetadata)
            //        {
            //            Console.WriteLine(GeneratorHelper.CreateJoinDefinition((IJoinColumnMetadata)c));
            //            Console.WriteLine(GeneratorHelper.CreateJoinProperty((IJoinColumnMetadata)c));
            //        }
            //    }
            //}

            FakeContext context = new FakeContext();

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    User usr = new User { Email = i + "@evoconcept.net", Name = i.ToString(), LastName = i.ToString() };
                    context.Insert(usr);
                }
                catch { }
            }

            try
            {
                context.Query<User>().Select(u => new Profile { UserId = u.Id, PictureUrl = "http://evoconcept.net" }).Insert();
            }
            catch { }

            try
            {
                context.Query<User>().Insert(u => new Profile { UserId = u.Id, PictureUrl = "http://evoconcept.net" });
            }
            catch { }


            try
            {
                context.Query<User>().Where(u => u.Name.Contains("items")).Delete();
            }
            catch { }

            try
            {
                context.Query<User>().Where(u => u.Name.Contains("items")).Update(u => new User { LastAccess = DateTime.Now });
            }
            catch { }

            Console.WriteLine("Ok it's end");
            Console.ReadKey();
            /*
            NamingRulesProvider.TableName = name =>
            {
                IEnumerable<string> parts = name.Split('_').Skip(1);

                if (parts.Count() > 1)
                    parts = parts.Take(parts.Count() - 1);

                return string.Join("_", parts);
            };
            NamingRulesProvider.ColumnName = name =>
            {
                IEnumerable<string> parts = name.Split('_').Skip(1);

                List<string> words = new List<string>();
                foreach (string word in parts)
                {
                    ITableMetadata tblMeta = meta.Tables.FirstOrDefault(t => t.Name.EndsWith("_" + word, StringComparison.OrdinalIgnoreCase));

                    if (tblMeta == null)
                        words.Add(word);
                    else
                        words.Add(GeneratorHelper.ToCSharpName(tblMeta.Name, NameType.Table));
                }

                return string.Join("_", words);
            };
            NamingRulesProvider.JoinColumnName = name =>
            {
                IEnumerable<string> parts = name.Split('_');
             
                if (parts.Count() > 1)
                    parts = parts.Take(parts.Count() - 1);

                List<string> words = new List<string>();
                foreach (string word in parts)
                {
                    ITableMetadata tblMeta = meta.Tables.FirstOrDefault(t => t.Name.EndsWith("_" + word, StringComparison.OrdinalIgnoreCase));

                    if (tblMeta == null)
                        words.Add(word);
                    else
                        words.Add(GeneratorHelper.ToCSharpName(tblMeta.Name, NameType.Table));
                }

                return string.Join("_", words);
            };*/
        }
    }

    public class FakeContext : SqlServerDataContext
    {
        public FakeContext()
            : base("")
        {
        }

        protected override System.Data.Common.DbCommand CreateCommand(System.Data.Common.DbConnection connection, System.Data.Common.DbTransaction transaction, string query)
        {
            Console.WriteLine(query);
            return null;
        }

        protected override System.Data.Common.DbConnection OpenConnection()
        {
            return null;
        }
    }
}
