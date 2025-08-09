namespace RepositoryPattern_And_UnitOfWork.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        
        // Navigation properties can be added if needed
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
