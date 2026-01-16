namespace ECommerce.BLL.ViewModels
{
    public class BasketViewModel
    {
        public List<BasketItemViewModel> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
        public int TotalCount => Items.Sum(x => x.Quantity);
    }

    public class BasketItemViewModel
    {
        public int ProductVariantId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }

    public class BasketCookieItemViewModel
    {

        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }
}