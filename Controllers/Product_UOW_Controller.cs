using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPattern_And_UnitOfWork.Models;
using RepositoryPattern_And_UnitOfWork.Repository.UnitOfWork;

namespace RepositoryPattern_And_UnitOfWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Product_UOW_Controller : ControllerBase
    {
        private readonly IUnitofWork _unitOfWork;
        public Product_UOW_Controller(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        // Example action method
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _unitOfWork.GetRepository<Product>().GetAllAsync();
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto product)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                if (product == null)
                {
                    return BadRequest("Product cannot be null.");
                }
                var newProduct = new Product
                {
                    ProductName = product.ProductName,
                    Price = product.Price,
                };
                await _unitOfWork.GetRepository<Product>().CreateAsync(newProduct);
                await _unitOfWork.SaveChangesAsync();

                var order = new Order
                {
                    ProductId = newProduct.ProductId,
                    OrderNumber = "10"
                };
                await _unitOfWork.GetRepository<Order>().CreateAsync(order);
                
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return CreatedAtAction(nameof(GetProducts), new { id = newProduct.ProductId }, newProduct);
            }
            catch (Exception ex)
            {
                await _unitOfWork.DeleteTransactionAsync();
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

    }
}
