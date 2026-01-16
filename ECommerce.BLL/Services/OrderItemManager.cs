using AutoMapper;
using ECommerce.BLL.Services.Contracts;
using ECommerce.BLL.ViewModels;
using ECommerce.DAL.DataContext.Entities;
using ECommerce.DAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.BLL.Services
{
    public class OrderItemManager
        : CrudManager<OrderItem, OrderItemViewModel, OrderItemCreateViewModel, OrderItemUpdateViewModel>,
          IOrderItemService
    {
        private readonly BasketManager _basketManager;

        public OrderItemManager(
            IRepository<OrderItem> repository,
            IMapper mapper,
            BasketManager basketManager
        ) : base(repository, mapper)
        {
            _basketManager = basketManager;
        }

        // This gets order items from the basket and maps them into OrderItemCreateViewModel
        public async Task<List<OrderItemCreateViewModel>> GetOrderItemCreateViewModels()
        {
            var basket = await _basketManager.GetBasketAsync();

            var orderItems = new List<OrderItemCreateViewModel>();

            foreach (var item in basket.Items)
            {
                orderItems.Add(new OrderItemCreateViewModel
                {
                    ProductVariantId = item.ProductVariantId,
                    Quantity = item.Quantity
                    
                });
            }

            return orderItems;
        }
    }
}

//namespace ECommerce.BLL.Services
//{
//    public class OrderItemManager : CrudManager<OrderItem, OrderItemViewModel, OrderItemCreateViewModel, OrderItemUpdateViewModel>,
//        IOrderItemService
//    {
//        private readonly BasketManager _basketManager;
//        public OrderItemManager(IRepository<OrderItem> repository, IMapper mapper, BasketManager basketManager) : base(repository, mapper)
//        {
//            _basketManager = basketManager;
//        }

//        public async Task<List<OrderItemCreateViewModel>> GetOrderDetailCreateViewModels()
//        {
//            var basketViewModel = await _basketManager.GetBasketAsync();

//            var orderDetailCreateViewModels = new List<OrderItemCreateViewModel>();

//            foreach (var item in basketViewModel.Items)
//            {
//                orderDetailCreateViewModels.Add(new OrderItemCreateViewModel()
//                {
//                    Quantity = item.Quantity,
//                    ProductVariantId = item.ProductVariantId,
//                });
//            }

//            return orderDetailCreateViewModels;
//        }


//    }
//}
