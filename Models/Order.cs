namespace RepositoryPattern_And_UnitOfWork.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public Product Product { get; set; } = new Product();
    }
}
