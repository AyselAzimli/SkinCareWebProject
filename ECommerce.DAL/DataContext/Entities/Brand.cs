using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.DataContext.Entities
{
    public class Brand : TimeStample
    {
        public string Name { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = [];
    }
}
