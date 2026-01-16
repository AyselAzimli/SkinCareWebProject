using ECommerce.BLL.Services.Contracts;
using ECommerce.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.MVC.Controllers
{
    public class DetailsController : Controller
    {
        private readonly IDetailsService _detailsService;

        public DetailsController(IDetailsService detailsService)
        {
            _detailsService = detailsService;
        }

        // Accept product id from query string
        public async Task<IActionResult> Index(int id)
        {
            // Get the full details view model (categories + product)
            var viewModel = await _detailsService.GetServiceViewModel();

            // Find the product by id
            var product = viewModel.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            // Pass the product + categories to the view
            var model = new DetailsViewModel
            {
                Categories = viewModel.Categories,
                Products = new List<ProductViewModel> { product }
            };

            return View(model);
        }
    }
}
