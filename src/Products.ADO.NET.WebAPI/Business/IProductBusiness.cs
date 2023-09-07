using Products.ADO.NET.WebAPI.Models;

namespace Products.ADO.NET.WebAPI.Business
{
    public interface IProductBusiness
    {
        List<Product> GetAllProducts();

        Product GetProductById(int id);

        Product CreateProduct(Product product);

        Product UpdateProduct(Product product);

        void DeleteProduct(int id);

        bool Exists(int id);
    }
}
