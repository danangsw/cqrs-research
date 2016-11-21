using Example.Db.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.General.Database;

namespace Example.Db.Infrastructure
{
    public class ExampleMigrator
    {
        public static void Migrate()
        {
            var migrator = new DbMigrator(new Configuration());
            migrator.Update();
        }


        public static void Down()
        {
            using (var dbExample = new ExampleContext())
            {
                var migration = new initial();
                migration.Down();               
                dbExample.RunMigration(migration);
            }
        }
    }
}
