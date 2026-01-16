using AutoMapper;
using ECommerce.BLL.Services.Contracts;
using ECommerce.BLL.ViewModels;
using ECommerce.DAL.DataContext.Entities;
using ECommerce.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.BLL.Services
{
    public class OrderManager : CrudManager<Order, OrderViewModel, OrderCreateViewModel, OrderUpdateViewModel>,
            IOrderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAddressService _addressService;
        private readonly IOrderItemService _orderDetailService;
        private readonly BasketManager _basketManager;

        public OrderManager(
            IRepository<Order> repository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<AppUser> userManager,
            IAddressService addressService,
            IOrderItemService orderDetailService,
            BasketManager basketManager
        ) : base(repository, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _addressService = addressService;
            _orderDetailService = orderDetailService;
            _basketManager = basketManager;
        }

        // Populate order creation model with user info and default address
        public async Task<OrderCreateViewModel> GetUserAndAddressViewModel(OrderCreateViewModel model)
        {
            var currentUser = _httpContextAccessor.HttpContext?.User;

            if (currentUser != null && currentUser.Identity!.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(currentUser.Identity.Name!);

                if (user != null)
                {
                    model.AppUserId = user.Id;
                    model.Email = user.Email!;

                    var addressViewModel = await _addressService.GetAsync(
                        x => x.AppUserId == user.Id && x.IsDefault && !x.IsDeleted
                    );

                    if (addressViewModel != null)
                    {
                        model.AddressCreateViewModel = new AddressCreateViewModel()
                        {
                            Adress = addressViewModel.Adress!,
                            FirstName = addressViewModel.FirstName!,
                            LastName = addressViewModel.LastName!,
                            Country = addressViewModel.Country!,
                            Company = addressViewModel.Company,
                            City = addressViewModel.City!,
                            Phone = addressViewModel.Phone!,
                            PostalCode = addressViewModel.PostalCode!
                        };
                    }
                }
            }

            return model;
        }

        // Create order
        public override async Task CreateAsync(OrderCreateViewModel model)
        {
            // Fill order items from basket
            model.OrderDetails = await _orderDetailService.GetOrderItemCreateViewModels();
            model.OrderStatus = OrderStatus.Pending;

            var order = Mapper.Map<Order>(model);

            var currentUser = _httpContextAccessor.HttpContext?.User;

            if (currentUser != null && currentUser.Identity!.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(currentUser.Identity.Name!);

                if (user != null)
                {
                    order.AppUserId = user.Id;
                    order.Email = user.Email!;

                    var addressViewModel = await _addressService.GetAsync(
                        x => x.AppUserId == user.Id && x.IsDefault && !x.IsDeleted
                    );

                    if (addressViewModel != null)
                        order.AddressId = addressViewModel.Id;
                }
            }
            else if (model.AddressCreateViewModel != null)
            {
                var address = await _addressService.CreateAddressAsync(model.AddressCreateViewModel);
                order.AddressId = address.Id;
            }

            await Repository.CreateAsync(order);
        }

        // Get all orders of a user
        public async Task<List<OrderViewModel>> GetOrderViewModelsAsync(string userId)
        {
            var model = await GetAllAsync(
                x => x.AppUser!.Id == userId && !x.IsDeleted,
                include: x => x.Include(o => o.OrderItems)
                               .ThenInclude(od => od.ProductVariant)
                               .ThenInclude(pv => pv.Product)
            );

            return model.ToList();
        }

        // Get order details by ID
        public async Task<OrderViewModel> GetDetailsOfOrderAsync(int orderId)
        {
            var order = await GetAsync(
                x => x.Id == orderId && !x.IsDeleted,
                include: x => x.Include(o => o.OrderItems)
                               .ThenInclude(od => od.ProductVariant)
                               .ThenInclude(pv => pv.Product)
                               .Include(o => o.Address)
            );

            if (order == null)
                return null!;

            // Calculate total price per order item
            foreach (var detail in order.OrderDetails)
            {
                detail.TotalPrice = detail.ProductVariant!.Price * detail.Quantity;
            }

            return order;
        }
    }
}
