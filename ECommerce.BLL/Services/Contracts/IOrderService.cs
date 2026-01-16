using ECommerce.BLL.ViewModels;
using ECommerce.DAL.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BLL.Services.Contracts
{
    public interface IOrderService
       : ICrudService<Order, OrderViewModel, OrderCreateViewModel, OrderUpdateViewModel>
    {
        Task<OrderCreateViewModel> GetUserAndAddressViewModel(OrderCreateViewModel model);


        Task<List<OrderViewModel>> GetOrderViewModelsAsync(string userId);
        //Task<OrderViewModel> GetItemOfOrderAsync(int orderId);
    }
}
