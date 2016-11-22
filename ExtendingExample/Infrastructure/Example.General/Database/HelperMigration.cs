using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Example.General.Database
{
    public static class HelperMigration
    {
        public static void RunMigration(this DbContext context, DbMigration migration)
        {
            var prop = migration.GetType().GetProperty("Operations", BindingFlags.NonPublic | BindingFlags.Instance);
            if (prop != null)
            {
                IEnumerable<MigrationOperation> operations = prop.GetValue(migration) as IEnumerable<MigrationOperation>;
                var generator = new SqlServerMigrationSqlGenerator();
                var statements = generator.Generate(operations, "2008");
                foreach (MigrationStatement item in statements)
                    context.Database.ExecuteSqlCommand(item.Sql);
            }
        }
    }
}
