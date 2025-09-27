namespace ECommerce.DAL.DataContext.Entities
{
    public class ProductVariant : Entity
    {
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; } 
        public int StockQuantity { get; set; } 

        // Navigation properties
        public Product? Product { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = []; 
    }
}
