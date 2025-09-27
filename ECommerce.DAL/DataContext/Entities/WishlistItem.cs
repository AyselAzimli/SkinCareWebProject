using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.DataContext.Entities
{
    public class WishlistItem : TimeStample
    {
        public string AppUserId { get; set; }
        public int ProductId { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public AppUser? AppUser { get; set; }
        public Product? Product { get; set; }
    }
}
