using Microsoft.AspNetCore.Mvc;
using Products.ADO.NET.WebAPI.Business;
using Products.ADO.NET.WebAPI.Models;

namespace Products.ADO.NET.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusiness _productBusiness;

        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _productBusiness.GetAllProducts();

                return Ok(products);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id) 
        {
            try
            {
                var product = _productBusiness.GetProductById(id);

                if (product == null) 
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product) 
        {
            try
            {
                var createdProduct = _productBusiness.CreateProduct(product);

                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product updateProduct) 
        {
            try
            {
                var productExists = _productBusiness.Exists(id);

                if (!productExists) 
                {
                    return NotFound();
                }

                var productToUpdate = new Product(id, updateProduct.Name, updateProduct.Description, updateProduct.Price, updateProduct.Quantity);
                var updateProductResult = _productBusiness.UpdateProduct(productToUpdate);

                return Ok(updateProductResult);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id) 
        {
            try
            {
                if (!_productBusiness.Exists(id)) 
                {
                    return NotFound();
                }

                _productBusiness.DeleteProduct(id);

                return NoContent();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

    }
}
