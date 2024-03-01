using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List_WIth_SQL.Connection
{
    internal static class ConnectionWithDB
    {
        public static SqlConnection OpenConnect()
        {
            var connectionString = ConfigurationManager
                .ConnectionStrings["DefaulConnection"]
                .ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            
                conn.Open();
                return conn;           
            
        }
    }
}
