using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace OutputClass
{
    public class Mein
    {
        public static SqlConnection CreateConnection(string initialCatalog)
        {
            Console.WriteLine($"Data Source = DESKTOP-5QJFN1Q\\SQLEXPRESS; Initial Catalog = {initialCatalog}; Integrated Security = true;");
            return new SqlConnection($"Data Source = DESKTOP-5QJFN1Q\\SQLEXPRESS; Initial Catalog = {initialCatalog}; Integrated Security = true;");
        }
    }
}
