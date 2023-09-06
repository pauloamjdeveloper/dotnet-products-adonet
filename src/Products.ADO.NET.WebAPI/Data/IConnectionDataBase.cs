using System.Data;

namespace Products.ADO.NET.WebAPI.Data
{
    public interface IConnectionDataBase
    {
        IDbConnection GetDbConnection();

        void OpenConnection();

        void CloseConnection();
    }
}
