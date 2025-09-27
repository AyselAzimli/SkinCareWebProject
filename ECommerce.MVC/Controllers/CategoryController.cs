using ECommerce.BLL.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync(include: x => x.Include(x => x.Products));

            return Json(categories);
        }
    }
}
