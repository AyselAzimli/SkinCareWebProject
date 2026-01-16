using ECommerce.BLL.ViewModels;
using ECommerce.DAL.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BLL.Services.Contracts
{
    public interface IOrderItemService
         : ICrudService<OrderItem, OrderItemViewModel, OrderItemCreateViewModel, OrderItemUpdateViewModel>
    {
        Task<List<OrderItemCreateViewModel>> GetOrderItemCreateViewModels();
    }
}
