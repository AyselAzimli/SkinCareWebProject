namespace ECommerce.DAL.DataContext.Entities
{
    public class Bio : Entity
    {
        public string LogoUrl { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
    }

}
