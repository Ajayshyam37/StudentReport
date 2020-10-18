using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentReportFromExceltoDB
{
    class DatabaseConnection
    {
        public  SqlConnection GetDbConnection()
        {
            string strConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TestDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection connection = new SqlConnection(strConnectionString);
            connection.Open();
            return connection;
        }
    }
}
