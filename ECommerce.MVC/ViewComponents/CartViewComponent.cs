using ECommerce.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly BasketManager _basketManager;

        public CartViewComponent(BasketManager basketManager)
        {
            _basketManager = basketManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basket = await _basketManager.GetBasketAsync();
            return View(basket);
        }
    }
}
