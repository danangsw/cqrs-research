using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Tests.Helper
{
    public static class ScenarioHelper
    {
        public static void DropTableIfExist(this SqlConnection con, string tableName)
        {
            string sqlCheckTable = @"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES 
                       WHERE TABLE_NAME='" + tableName + "') SELECT 1 ELSE SELECT 0";

            SqlCommand tableCheck = new SqlCommand(sqlCheckTable, con);
            int exist = Convert.ToInt32(tableCheck.ExecuteScalar());
            if (exist == 1)
            {
                string sqlDrop = @"DROP TABLE """ + tableName + @""";";

                SqlCommand cmd = new SqlCommand(sqlDrop, con);
                cmd.ExecuteNonQuery();
            }

        }
    }
}
