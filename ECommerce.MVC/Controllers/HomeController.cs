using ECommerce.BLL.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _homeService.GetHomeViewModel();

            return View(model);
        }
    }
}
