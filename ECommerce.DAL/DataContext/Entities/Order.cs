using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.DataContext.Entities
{
    public class Order: TimeStample
    {
        public string? AppUserId { get; set; }
        public int? CouponId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public AppUser? AppUser { get; set; }
        public DiscountCoupon? Coupon { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }

    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Shipped,
        Delivered,
        Cancelled
    }
}
