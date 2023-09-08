using Products.ADO.NET.WebAPI.Data;
using Products.ADO.NET.WebAPI.Models;
using Products.ADO.NET.WebAPI.Exceptions;
using System.Data;
using System.Data.SqlClient;

namespace Products.ADO.NET.WebAPI.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConnectionDataBase _dataBase;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(IConnectionDataBase dataBase, ILogger<ProductRepository> logger)
        {
            _dataBase = dataBase;
            _logger = logger;
        }

        private string _errorMessage = "";

        public List<Product> GetAll()
        {
            IDbConnection connection = null;

            try
            {
                var products = new List<Product>();

                connection = _dataBase.GetDbConnection();
                connection.Open();

                string query = "SELECT * FROM TB_Products";

                using (var command = new SqlCommand(query, (SqlConnection)connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = Convert.ToInt32(reader["Id"]);
                            var name = Convert.ToString(reader["Name"]);
                            var description = Convert.ToString(reader["Description"]);
                            var price = Convert.ToDecimal(reader["Price"]);
                            var quantity = Convert.ToInt32(reader["Quantity"]);

                            var product = new Product(id, name, description, price, quantity);
                            products.Add(product);
                        }
                    }
                }

                return products;
            }
            catch (SqlException exception)
            {
                _errorMessage = $"Error getting all products: {exception.Message}";
                _logger.LogError(exception, _errorMessage);
                throw new Exception(_errorMessage);
            }
            finally
            {
                connection.Close();
            }
        }


        public Product GetById(int id)
        {
            IDbConnection connection = null;

            try
            {
                connection = _dataBase.GetDbConnection();
                connection.Open();

                var query = "SELECT * FROM TB_Products WHERE Id = @Id";

                using (var command = new SqlCommand(query, (SqlConnection)connection)) 
                {
                    command.Parameters.AddWithValue(@"Id", id);

                    using (var reader = command.ExecuteReader()) 
                    {
                        if (reader.Read())
                        {
                            var name = Convert.ToString(reader["Name"]);
                            var description = Convert.ToString(reader["Description"]);
                            var price = Convert.ToDecimal(reader["Price"]);
                            var quantity = Convert.ToInt32(reader["Quantity"]);

                            var product = new Product(id, name, description, price, quantity);
                            return product;
                        }
                        else 
                        {
                            throw new ProductNotFoundException($"Product with ID {id} not found.");
                        }
                    }
                }
            }
            catch (SqlException exception)
            {
                _errorMessage = $"Error getting product by id: {exception.Message}";
                _logger.LogError(exception, _errorMessage);
                throw new Exception(_errorMessage);
            }
            finally 
            {
                connection.Close();
            }
        }

        public Product Create(Product product)
        {
            IDbConnection connection = null;

            try
            {
                connection = _dataBase.GetDbConnection();
                connection.Open();

                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO TB_Products (Name, Description, Price, Quantity) VALUES (@Name, @Description, @Price, @Quantity)";

                    AddParameter(command, "@Name", SqlDbType.NVarChar, product.Name);
                    AddParameter(command, "@Description", SqlDbType.NVarChar, product.Description);
                    AddParameter(command, "@Price", SqlDbType.Decimal, product.Price);
                    AddParameter(command, "@Quantity", SqlDbType.Int, product.Quantity);

                    command.ExecuteNonQuery();

                    return product;
                }
            }
            catch (SqlException exception)
            {
                _errorMessage = $"Error creating product: {exception.Message}";
                _logger.LogError(exception, _errorMessage);
                throw new Exception(_errorMessage);
            }
            finally 
            {
                connection.Close();
            }
        }

        public Product Update(Product product)
        {
            IDbConnection connection = null;

            try
            {
                connection = _dataBase.GetDbConnection();
                connection.Open();

                using (var command = (SqlCommand)connection.CreateCommand()) 
                {
                    command.CommandText = "UPDATE TB_Products SET Name = @Name, Description = @Description, Price = @Price, Quantity = @Quantity WHERE Id = @Id";

                    AddParameter(command, "@Id", SqlDbType.Int, product.Id);
                    AddParameter(command, "@Name", SqlDbType.NVarChar, product.Name);
                    AddParameter(command, "@Description", SqlDbType.NVarChar, product.Description);
                    AddParameter(command, "@Price", SqlDbType.Decimal, product.Price);
                    AddParameter(command, "@Quantity", SqlDbType.Int, product.Quantity);

                    command.ExecuteNonQuery();

                    return product;
                }
            }
            catch (SqlException exception)
            {
                _errorMessage = $"Error updating product: {exception.Message}";
                _logger.LogError(exception, _errorMessage);
                throw new Exception(_errorMessage);
            }
            finally
            {
                connection.Close();
            }
        }

        public void Delete(int id)
        {
            IDbConnection connection = null;

            try
            {
                connection = _dataBase.GetDbConnection();
                connection.Open();

                using (var command = (SqlCommand)connection.CreateCommand()) 
                {
                    command.CommandText = "DELETE FROM TB_Products WHERE Id = @Id";
                    AddParameter(command, "@Id", SqlDbType.Int, id);
                    command.ExecuteNonQuery();
                }

            }
            catch (SqlException exception)
            {
                _errorMessage = $"Error deleting product: {exception.Message}";
                _logger.LogError(exception, _errorMessage);
                throw new Exception(_errorMessage);
            }
            finally
            {
                connection.Close();
            }
        }

        private void AddParameter(SqlCommand command, string paramName, SqlDbType dbType, object value)
        {
            var parameter = new SqlParameter(paramName, dbType) { Value = value };
            command.Parameters.Add(parameter);
        }
    }
}
