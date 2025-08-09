using Microsoft.AspNetCore.Mvc;
using RepositoryPattern_And_UnitOfWork.Models;
using RepositoryPattern_And_UnitOfWork.Repository.Generic;

namespace RepositoryPattern_And_UnitOfWork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductGenericController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;
        public ProductGenericController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product?>>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Any() ? Ok(products) : NotFound();
        }
        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product != null ? Ok(product) : NotFound();
        }
        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateAsync([FromBody] ProductDto product)
        {
            var productEntity = new Product
            {
                ProductName = product.ProductName,
                Price = product.Price,
            };

            return await _productRepository.CreateAsync(productEntity);
        }
        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] ProductDto product)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return NotFound();

            var productEntity = new Product
            {
                ProductName = product.ProductName,
                Price = product.Price,
            };

            var updated = await _productRepository.UpdateAsync(id, productEntity);
            return updated ? NoContent() : StatusCode(500, "Product data is Empty");
        }
        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return NotFound();
            var deleted = await _productRepository.DeleteAsync(id, existingProduct);
            return deleted ? NoContent() : StatusCode(500, "Failed to delete the product");
        }
    }
}
