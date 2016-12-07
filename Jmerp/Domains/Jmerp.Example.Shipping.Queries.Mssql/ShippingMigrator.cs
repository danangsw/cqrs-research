using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.Mssql
{
    public class ShippingMigrator
    {

        public static void Migrate()
        {
            Jmerp.Db.JmerpMigrator.Migrate();
        }

    }
}
