using Products.ADO.NET.WebAPI.Models;
using Products.ADO.NET.WebAPI.Repositories;

namespace Products.ADO.NET.WebAPI.Business.Implementations
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepository _productRepository;

        public ProductBusiness(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetById(id);
        }

        public Product CreateProduct(Product product)
        {
            return _productRepository.Create(product);
        }

        public Product UpdateProduct(Product product)
        {
            return _productRepository.Update(product);
        }

        public void DeleteProduct(int id)
        {
            _productRepository.Delete(id);
        }

        public bool Exists(int id)
        {
            var product = _productRepository.GetById(id);
            return product != null;
        }
    }
}
