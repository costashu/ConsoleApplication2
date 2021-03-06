﻿namespace ConsoleApplication2
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Core.Common;
    using System.Data.SQLite;
    using System.Data.SQLite.EF6;
    using System.Linq;

    class Program
    {
        static void Main()
        {
            var connectionString = @"data source = test.db3";
            using (var connection = new SQLiteConnection(connectionString))
            using (var model = new Model1(connection))
            {
                Console.WriteLine("Using EF Code First approach.");

                var dbSetProperty = model.dbSetProperty.ToList();
                foreach (var r in dbSetProperty)
                    Console.WriteLine("{0} -- {1}", r.Key, r.Property);
                Console.ReadKey(true);
            }

            Console.WriteLine();

            using (var connection = new SQLiteConnection(connectionString))
            using (var model = new Entities(connection))
            {
                Console.WriteLine("Using EF Database First approach.");

                var dbSetProperty = model.dbSetProperty.ToList();
                foreach (var r in dbSetProperty)
                    Console.WriteLine("{0} -- {1}", r.Key, r.Property);
                Console.ReadKey(true);
            }
        }
    }

    class SqliteDbConfiguration : DbConfiguration
    {
        public SqliteDbConfiguration()
        {
            string assemblyName = typeof (SQLiteProviderFactory).Assembly.GetName().Name;

            RegisterDbProviderFactories(assemblyName);
            SetProviderFactory(assemblyName, SQLiteFactory.Instance);
            SetProviderFactory(assemblyName, SQLiteProviderFactory.Instance);
            SetProviderServices(assemblyName,
                (DbProviderServices) SQLiteProviderFactory.Instance.GetService(
                    typeof (DbProviderServices)));
        }

        static void RegisterDbProviderFactories(string assemblyName)
        {
            var dataSet = ConfigurationManager.GetSection("system.data") as DataSet;
            if (dataSet != null)
            {
                var dbProviderFactoriesDataTable = dataSet.Tables.OfType<DataTable>()
                    .First(x => x.TableName == typeof (DbProviderFactories).Name);

                var dataRow = dbProviderFactoriesDataTable.Rows.OfType<DataRow>()
                    .FirstOrDefault(x => x.ItemArray[2].ToString() == assemblyName);

                if (dataRow != null)
                    dbProviderFactoriesDataTable.Rows.Remove(dataRow);

                dbProviderFactoriesDataTable.Rows.Add(
                    "SQLite Data Provider (Entity Framework 6)",
                    ".NET Framework Data Provider for SQLite (Entity Framework 6)",
                    assemblyName,
                    typeof (SQLiteProviderFactory).AssemblyQualifiedName
                    );
            }
        }
    }

    public partial class Model1
    {
        public Model1(DbConnection connection)
            : base(connection, true)
        {
        }
    }

    public partial class Entities
    {
        public Entities(DbConnection connection)
            : base(connection, true)
        {
        }
    }
}