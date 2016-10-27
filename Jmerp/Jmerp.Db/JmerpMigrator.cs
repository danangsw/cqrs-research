using Jmerp.Db.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db
{
    public class JmerpMigrator
    {
        public static void Migrate()
        {
            var migrator = new DbMigrator(new Configuration());
            migrator.Update();
        }
    }
}
