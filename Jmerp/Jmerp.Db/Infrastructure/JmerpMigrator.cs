using Jmerp.Commons.Database;
using Jmerp.Db;
using Jmerp.Db.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Infrastructure
{
    public class JmerpMigrator
    {
        public static void Migrate()
        {
            var migrator = new DbMigrator(new Configuration());
            migrator.Update();
        }


        public static void Down()
        {
            using (var dbExample = new JmrepContext())
            {
                var migration = new initial();
                migration.Down();
                dbExample.RunMigration(migration);
            }
        }
    }
}
