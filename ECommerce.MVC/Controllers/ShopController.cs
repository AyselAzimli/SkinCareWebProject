using ECommerce.BLL.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        public async Task<IActionResult> Index(int? categoryId, int? brandId, string? search, int page = 1)
        {

            var model = await _shopService.GetShopViewModelAsync(
                   categoryId,
                   brandId,
                   search,
                   page
               ); return View(model);
        }


    }
}
