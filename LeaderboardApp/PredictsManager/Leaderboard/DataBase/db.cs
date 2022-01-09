using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbplayers
{
    public static class DB
    {
        public static string DataSource = string.Empty;
        public static string InitialCatalog = string.Empty;
        public static string UserId = string.Empty;
        public static string Password = string.Empty;

        public static string ConnectionString = $"Data Source={DataSource};" +
                $"Initial Catalog={InitialCatalog};" +
                $"User id={UserId};" +
                $"Password={Password};" +
                $"Integrated Security=false;";

        //sql5109.site4now.net
        //db_a7e727_admin
        //db_a7e727_admin_admin
        //EgorPrivet123

        //public static string ConnectionString = $"Data Source=sql5080.site4now.net;" +
        //        $"Initial Catalog=db_a7d303_fet;" +
        //        $"User id=db_a7d303_fet_admin;" +
        //        $"Password=EgorPrivet123;" +
        //        $"Integrated Security=false;";

        public static IDbConnection connection;
        //public static List<Product> products = new List<Product>();

        public static bool Connect(string DataSource, string InitialCatalog, string UserId, string Password)
        {
            ConnectionString = $"Data Source={DataSource};" +
                $"Initial Catalog={InitialCatalog};" +
                $"User id={UserId};" +
                $"Password={Password};" +
                $"Integrated Security=false;";

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void UpdateProducts()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                //products = connection.Query<Product>("SELECT * FROM [PRODUCT_KIRI]").ToList();
            }
        }
        public static void InsertProduct(int CategoryId, string ProductName, int ProductPrice)
        {
            string sql = $"INSERT INTO [PRODUCT_KIRI] ([CATEGORY_ID], [PRODUCT_NAME], [PRODUCT_PRICE]) Values ('{CategoryId}','{ProductName}','{ProductPrice}');";

            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                var affectedRows = connection.Execute(sql);

            }
        }
        public static void DisplayAll()
        {
            //products.ForEach(x => Console.WriteLine(x));
        }
        public static void InsertByProcedure(int CategoryId, string ProductName, int ProductPrice)
        {
            string sql = $"EXECUTE [ADDPRODUCT] @name = {ProductName}, @category = {CategoryId}, @price = {ProductPrice};";

            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                var affectedRows = connection.Execute(sql,
                    new { CATEGORY_ID = CategoryId, PRODUCT_NAME = ProductName, PRODUCT_PRICE = ProductPrice },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
