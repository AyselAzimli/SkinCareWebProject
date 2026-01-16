using AutoMapper;
using ECommerce.BLL.Services.Contracts;
using ECommerce.BLL.Services;
using ECommerce.BLL.ViewModels;
using ECommerce.DAL.DataContext.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ECommerceProject.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderDetailService;
        private readonly BasketManager _basketManager;


        public OrderController(IOrderService orderService, UserManager<AppUser> userManager, IOrderItemService orderDetailService, BasketManager basketManager)
        {
            _orderService = orderService;
            _userManager = userManager;
            _orderDetailService = orderDetailService;
            _basketManager = basketManager;
        }

        public async Task<IActionResult> Checkout()
        {
            var addressViewModel = new AddressViewModel();

            var model = new OrderCreateViewModel();

            var basketViewModel = await _basketManager.GetBasketAsync();

            model.BasketViewModel = basketViewModel;

            model.OrderDetails = await _orderDetailService.GetOrderItemCreateViewModels();
            model = await _orderService.GetUserAndAddressViewModel(model);
            model.TotalPrice = basketViewModel.TotalPrice;

            return View(model);
        }

        public async Task<IActionResult> Index()
        {
            var username = User.Identity!.Name ?? "";

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
                return BadRequest();

            var models = await _orderService.GetOrderViewModelsAsync(user.Id);

            foreach (var model in models)
            {
                model.TotalCount = model.OrderDetails.Sum(x => x.Quantity);
            }

            return View(models);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _orderService.GetAsync(predicate: x => x.Id == id && !x.IsDeleted,
                include: x => x.Include(od => od.OrderItems).ThenInclude(pv => pv.ProductVariant).ThenInclude(p => p.Product!)
                .Include(od => od.OrderItems).ThenInclude(pv => pv.ProductVariant).ThenInclude(c => c.Price!)
                .Include(a => a.Address));

            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderCreateViewModel model)
        {
            if (model.AddressCreateViewModel == null)
            {
                ModelState.AddModelError("", "Unvan qeyd edilmeyib");

                return View(model);
            }

            if (model.AcceptTermsConditions == false)
            {
                ModelState.AddModelError("", "Terms and conditions must be accepted");

                return View(model);
            }

            var basketViewModel = await _basketManager.GetBasketAsync();
            model.OrderDetails = await _orderDetailService.GetOrderItemCreateViewModels();

            model.BasketViewModel = basketViewModel;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.EndPrice = model.TotalPrice;
           

            await _orderService.CreateAsync(model);
            _basketManager.CleanBasket();
            return RedirectToAction("Index");
        }

      

    }
}