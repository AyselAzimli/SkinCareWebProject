using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BLL.ViewModels
{
    public class ProductImageViewModel
    {
        public string ImageName { get; set; } = null!;
        public bool IsMain { get; set; }
        public bool IsHover { get; set; }
        public string ColorCode { get; set; } = null!;
    }
}
