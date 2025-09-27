using ECommerce.BLL.ViewModels;
using ECommerce.DAL.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BLL.Services.Contracts
{
    public interface IProductService : ICrudService<Product, ProductViewModel, CreateProductViewModel, UpdateProductViewModel>
    {
        Task<CreateProductViewModel> GetCreateViewModelAsync();
        Task<UpdateProductViewModel> GetUpdateViewModelAsync(int id);
    }


}
