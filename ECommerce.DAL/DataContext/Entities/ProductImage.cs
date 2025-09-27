using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.DataContext.Entities
{
    public class ProductImage : TimeStample
    {
        public string ImageName { get; set; } = null!;
        public bool IsMain { get; set; } //main pictr
        public bool IsHover { get; set; } //hover pictr
        public string ColorCode { get; set; } = null!; //color code
        public int ProductId { get; set; }

        public Product? Product { get; set; }
    }
}
