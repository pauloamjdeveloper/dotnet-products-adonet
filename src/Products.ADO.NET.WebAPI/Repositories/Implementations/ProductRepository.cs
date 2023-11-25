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
        private string _errorMessage = "";

        IDbConnection _connection = null;
        SqlDataReader dataReader = null;

        public ProductRepository(IConnectionDataBase dataBase, ILogger<ProductRepository> logger)
        {
            _dataBase = dataBase;
            _logger = logger;
        }

        public List<Product> GetAll()
        {
            try
            {
                var products = new List<Product>();
                var command = new SqlCommand();

                _connection = _dataBase.GetDbConnection();

                command.CommandText = "SELECT * FROM TB_Products";
                command.Connection = (SqlConnection)_connection;

                _connection.Open();

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    var id = Convert.ToInt32(dataReader["Id"]);
                    var name = Convert.ToString(dataReader["Name"]);
                    var description = Convert.ToString(dataReader["Description"]);
                    var price = Convert.ToDecimal(dataReader["Price"]);
                    var quantity = Convert.ToInt32(dataReader["Quantity"]);

                    var product = new Product(id, name, description, price, quantity);
                    products.Add(product);
                }

                return products;
            }
            catch (Exception exception)
            {
                _errorMessage = $"Error getting all products: {exception.Message}";
                _logger.LogError(exception, _errorMessage);
                throw new Exception(_errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }


        public Product GetById(int id)
        {
            try
            {
                var command = new SqlCommand();

                _connection = _dataBase.GetDbConnection();
                _connection.Open();

                command.CommandText = "SELECT * FROM TB_Products WHERE Id = @Id";
                command.Connection = (SqlConnection)_connection;
                command.Parameters.AddWithValue(@"Id", id);

                dataReader = command.ExecuteReader();

                if (dataReader.Read())
                {
                    var name = Convert.ToString(dataReader["Name"]);
                    var description = Convert.ToString(dataReader["Description"]);
                    var price = Convert.ToDecimal(dataReader["Price"]);
                    var quantity = Convert.ToInt32(dataReader["Quantity"]);

                    var product = new Product(id, name, description, price, quantity);
                    return product;
                }
                else
                {
                    throw new ProductNotFoundException($"Product with ID {id} not found.");
                }
            }
            catch (Exception exception)
            {
                _errorMessage = $"Error getting product by id: {exception.Message}";
                _logger.LogError(exception, _errorMessage);
                throw new Exception(_errorMessage);
            }
            finally
            {
                _connection.Close();
            }
        }

        public Product Create(Product product)
        {
            _connection = _dataBase.GetDbConnection();
            _connection.Open();

            var transaction = (SqlTransaction)_connection.BeginTransaction();

            try
            {
                var command = new SqlCommand();

                command.Transaction = transaction;
                command.Connection = (SqlConnection)_connection;
                command.CommandText = "INSERT INTO TB_Products (Name, Description, Price, Quantity) VALUES (@Name, @Description, @Price, @Quantity)";

                AddParameter(command, "@Name", DbType.String, product.Name);
                AddParameter(command, "@Description", DbType.String, product.Description);
                AddParameter(command, "@Price", DbType.Decimal, product.Price);
                AddParameter(command, "@Quantity", DbType.Int32, product.Quantity);

                command.ExecuteNonQuery();

                transaction.Commit();

                return product;
            }
            catch (Exception exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                _errorMessage = $"Error creating product: {exception.Message}";
                _logger.LogError(exception, _errorMessage);
                throw new Exception(_errorMessage);

            }
            finally
            {
                _connection.Close();
            }
        }

        public Product Update(Product product)
        {
            _connection = _dataBase.GetDbConnection();
            _connection.Open();

            var transaction = (SqlTransaction)_connection.BeginTransaction();

            try
            {
                var command = new SqlCommand();

                command.Transaction = transaction;
                command.Connection = (SqlConnection)_connection;
                command.CommandText = "UPDATE TB_Products SET Name = @Name, Description = @Description, Price = @Price, Quantity = @Quantity WHERE Id = @Id";

                AddParameter(command, "@Id", DbType.Int32, product.Id);
                AddParameter(command, "@Name", DbType.String, product.Name);
                AddParameter(command, "@Description", DbType.String, product.Description);
                AddParameter(command, "@Price", DbType.Decimal, product.Price);
                AddParameter(command, "@Quantity", DbType.Int32, product.Quantity);

                command.ExecuteNonQuery();

                transaction.Commit();

                return product;
            }
            catch (Exception exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                _errorMessage = $"Error updating product: {exception.Message}";
                _logger.LogError(exception, _errorMessage);
                throw new Exception(_errorMessage);

            }
            finally
            {
                _connection.Close();
            }

        }

        public void Delete(int id)
        {

            _connection = _dataBase.GetDbConnection();
            _connection.Open();

            var transaction = (SqlTransaction)_connection.BeginTransaction();

            try
            {
                var command = new SqlCommand();

                command.Transaction = transaction;
                command.Connection = (SqlConnection)_connection;
                command.CommandText = "DELETE FROM TB_Products WHERE Id = @Id";

                AddParameter(command, "@id", DbType.Int32, id);

                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                _errorMessage = $"Error deleting product: {exception.Message}";
                _logger.LogError(exception, _errorMessage);
                throw new Exception(_errorMessage);

            }
            finally
            {
                _connection.Close();
            }
        }

        private void AddParameter(IDbCommand command, string paramName, DbType dbType, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = paramName;
            parameter.DbType = dbType;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }
    }
}
