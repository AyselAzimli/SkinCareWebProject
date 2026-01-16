using System.Collections.Generic;

namespace ECommerce.BLL.ViewModels
{
    public class DetailsViewModel
    {
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}
