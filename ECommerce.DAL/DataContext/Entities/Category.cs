using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.DataContext.Entities
{
    public class Category :TimeStample
    {
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }

        // Navigation property
        public ICollection<Product> Products { get; set; } = [];
    }
}
