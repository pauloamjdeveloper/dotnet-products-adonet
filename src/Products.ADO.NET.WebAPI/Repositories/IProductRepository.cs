using Products.ADO.NET.WebAPI.Models;

namespace Products.ADO.NET.WebAPI.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAll();

        Product GetById(int id);

        Product Create(Product product);

        Product Update(Product product);

        void Delete(int id);
    }
}
