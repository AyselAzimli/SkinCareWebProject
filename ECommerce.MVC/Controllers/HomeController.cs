using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

    }
}
