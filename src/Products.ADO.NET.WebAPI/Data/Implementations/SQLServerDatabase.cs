using System.Data;
using System.Data.SqlClient;

namespace Products.ADO.NET.WebAPI.Data.Implementations
{
    public class SQLServerDatabase : IConnectionDataBase
    {
        private readonly string _connectionString;

        public SQLServerDatabase()
        {
            _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=DB_ProductsADONET;User ID=sa;Password=********";
        }
        
        public IDbConnection GetDbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public void OpenConnection()
        {
            using (var connection = GetDbConnection() as SqlConnection) 
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException exception)
                {
                    throw new Exception("Error opening connect to database: " + exception.Message);
                }
            }
        }
        public void CloseConnection()
        {
            using (var connection = GetDbConnection() as SqlConnection)
            {
                try
                {
                    connection.Close();
                }
                catch (SqlException exception)
                {
                    throw new Exception("Error closing database connection: " + exception.Message);
                }
            }
        }
    }
}
