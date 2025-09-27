using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.DataContext.Entities
{
    public class Slider :Entity
    {
        public string? ImageUrl { get; set; }
        public string? Desciption { get; set; }
    }
}
