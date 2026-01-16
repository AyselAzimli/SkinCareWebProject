using ECommerce.DAL.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BLL.ViewModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductVariantId { get; set; }
        public ProductVariantViewModel? ProductVariant { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderItemCreateViewModel
    {
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int ProductVariantId { get; set; }
        public ProductVariantViewModel ProductVariantViewModel { get; set; } = null!;
    }

    public class OrderItemUpdateViewModel
    {

    }
    
}
