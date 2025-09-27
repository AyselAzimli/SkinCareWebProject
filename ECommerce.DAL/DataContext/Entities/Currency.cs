namespace ECommerce.DAL.DataContext.Entities
{
    public class Currency : Entity
    {
        public string CurrencyIconClass { get; set; } = null!;
        public string CurrencyCode { get; set; } = null!; // USD, EUR, VND
        public string Symbol { get; set; } = null!; // $, €, d
        public string Country { get; set; } = null!; // United States, Germany, Vietnam
    }

}
