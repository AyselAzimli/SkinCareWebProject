using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
