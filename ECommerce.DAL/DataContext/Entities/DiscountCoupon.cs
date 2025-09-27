using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.DataContext.Entities
{
    public class DiscountCoupon: TimeStample
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public decimal DiscountPercentage { get; set; }
        public int UsageLimit { get; set; }
        public DateTime ExpiryDate { get; set; }

        // Navigation properties
        public ICollection<Order> Orders { get; set; } = [];
    }
}
